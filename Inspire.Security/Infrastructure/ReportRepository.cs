
using Services.SystemSecurity;

namespace Inspire.Security.Application
{
    
    public class ReportService(SecurityContext _context, 
        IParameterRepository parameterRepository,
        IMenuSettingRepository menuSettingRepository,
        IParameterGroupRepository parameterGroupRepository) : StandardMakerCheckerService<Report, ReportDto, string, SecurityContext, ReportFilter>(_context), IReportService

    {
        public override Report Find(params object[] id)
        {
            var reportID = id[0].ToString();
            return _context.Set<Report>().Where(s => s.Id == reportID).Include(s => s.SubMenu).FirstOrDefault();
        }
        
        
        public Task<List<ReportTab>> GetReportTabs(string reportID)
        {
            return _context.Set<ReportTab>().Where(s => s.ReportID == reportID).Include(s => s.ReportRows).ThenInclude(s => s.ReportFields).ToListAsync();
        }
        public async Task DeleteReportTabs(string reportID)
        {
            var data = await GetReportTabs(reportID);
            _context.RemoveRange(data);
            await _context.SaveChangesAsync();
        }
        public async Task AddReportTabs(List<ReportTab> data)
        { 
            await _context.AddRangeAsync(data);
            await _context.SaveChangesAsync();
        }
        public Task<List<InputField>> GetInputFields()
        {
            return _context.Set<InputField>().ToListAsync();
        }

        protected override OutputHandler Validate(ReportDto row, int a = 0, bool c = true, string capturer = "", [CallerMemberName] string caller = "")
        {
            if (caller.ToLower().StartsWith("add") || caller.ToLower().StartsWith("edit"))
            {
                if (Any(s => !(s.Id.Equals(row.Id)) && s.SubMenuID == row.SubMenuID && s.Name.ToUpper() == row.Name.ToUpper()))
                    throw new ValidationException($" Name {row.Name}for {_modelHeader} already exist");
            }
            return base.Validate(row, a, c, capturer, caller);
        }
        
        public List<GenericData<string>> GetReportGroups()
        {
            return menuSettingRepository.GetReportGroups();
        }
        public List<GenericData<string>> GetReportFormats(string formatCode = "")
        {
            return ReportFormatConfiguration.GetAllowableFormats(formatCode);
        }

        protected override ReportDto AlignDataTransferObject(ReportDto row, [CallerMemberName] string caller = "")
        {
            int count = Count(s=>s.SubMenuID==row.SubMenuID) + 1;

            row.Id = $"{row.SubMenuID}{count:000}";
            while (Any(s => s.Id == row.Id))
            {
                count++;
                row.Id = $"{row.SubMenuID}{count:000}" ;
            }

            return row;

        }

        public override IQueryable<Report> SearchByFilterModel(ReportFilter model, IQueryable<Report> data = null)
        {
            var search = GetSearchString(model);
            return base.SearchByFilterModel(model, data)
                .Where(s => (s.SubMenu.ParentMenu.Name + s.Name + s.SubMenu.Name).ToLower().Contains(search))
                .Include(s => s.SubMenu).ThenInclude(s => s.ParentMenu);
        }

        public List<GenericData<string>> GetReports(string reportGroupId)
        {
            return _context.Set<Report>().Where(s => s.SubMenuID == reportGroupId && s.AuthStatus == "A").Select(s => new GenericData<string>
            {
                ID = s.Id,
                Name = s.Name
            }).ToList();
        }

        public List<GenericData<string>> GetAllowableFormats(int reportId)
        {
            var report = Find(reportId);
            string format = report == null ? "" : report.ReportUIModel;
            return GetReportFormats(format);
        }
        public async Task<OutputHandler> UpdateReport(ReportDto report, List<ReportParamater> parameters, string createBy, int authorisers = 0, bool captureTrail = true)
        {
            var isScoped = _context.Database.CurrentTransaction != null;
            using var scope = _context.Database.CurrentTransaction ?? _context.Database.BeginTransaction();
            var record = await UpdateInScopeAsync(report, createBy, authorisers, captureTrail);
            if (!record.ErrorOccured)
            {
                await parameterRepository.DeleteReportParameters(report.Id);
                List<SystemReportParameterDto> parameterList = parameters
                    .Select(s => new SystemReportParameterDto
                    {
                        ReportID = report.Id,
                        ParameterID = s.Name,
                        Column = 1,
                        Row = s.Order
                    })
                    .ToList();
                record = await parameterRepository.AddInScopeAsync(parameterList, createBy, authorisers, captureTrail);

            }
            if (!record.ErrorOccured && !isScoped)
                scope.Commit();
            return record;
        }
        public async Task<OutputHandler> CreateReport(ReportDto report, List<ReportParamater> parameters, string createBy, int authorisers = 0, bool captureTrail = true)
        {
            var isScoped = _context.Database.CurrentTransaction != null;
            using var scope = _context.Database.CurrentTransaction ?? _context.Database.BeginTransaction();

            OutputHandler record = new OutputHandler();

            if (report.UserAction == "Edit")
            {
                record = await UpdateInScopeAsync(report, createBy, authorisers, captureTrail);
            }

            if (report.UserAction == "Create")
            {
                record = await AddInScopeAsync(report, createBy, authorisers, captureTrail);
            }




            if (!record.ErrorOccured && parameters.Count > 0)
            {
                var row = (Report)record.Data;
                List<SystemReportParameterDto> parameterList = parameters
                    .Select(s => new SystemReportParameterDto
                    {
                        ReportID = row.Id,
                        ParameterID = s.Name,
                        Column = 1,
                        Row = s.Order
                    })
                    .ToList();

                var sysParams = _context.Parameters.ToList();

                var paramDtos = new List<SysParameterDto>();
                foreach (var param in parameters)
                {

                    if (!sysParams.Any(s => s.Id == param.Name))
                    {
                        paramDtos.Add(new SysParameterDto()
                        {
                            Id = param.Name,
                            ReportParameterDataType = GetDataType(param.ParameterType),
                            MultipleSelect = false,
                            Name = param.Name.CamelSplit(),
                            Area = "",
                            Controller = ""
                        });
                    }
                }

                record = await parameterGroupRepository.AddInScopeAsync(paramDtos, createBy, 0, false);

                if (!record.ErrorOccured)
                {
                    var dt = _context.SystemReportParameters.Where(s => s.ReportID == row.Id).ToList();

                    _context.RemoveRange(dt);
                    // parameterRepository.DeleteInScopeAsync();

                    record = await parameterRepository.AddInScopeAsync(parameterList, createBy, 0, false);
                }


            }

            if (!record.ErrorOccured && !isScoped)
                scope.Commit();
            return record;
        }

        private ReportParameterDataType GetDataType(FieldValueType type)
        {
            switch (type)
            {
                case FieldValueType.Int8sField:
                case FieldValueType.Int8uField:
                case FieldValueType.Int16sField:
                case FieldValueType.Int16uField:
                case FieldValueType.Int32sField:
                case FieldValueType.Int32uField:
                case FieldValueType.NumberField:
                case FieldValueType.CurrencyField:
                    return ReportParameterDataType.Number;

                case FieldValueType.BooleanField:
                    return ReportParameterDataType.Boolean;

                case FieldValueType.DateField:
                case FieldValueType.TimeField:
                case FieldValueType.DateTimeField:
                    return ReportParameterDataType.Date;


                case FieldValueType.TransientMemoField:
                case FieldValueType.BlobField:
                case FieldValueType.StringField:
                case FieldValueType.PersistentMemoField:
                case FieldValueType.BitmapField:
                    return ReportParameterDataType.String;


                //
                //    return ReportParameterDataType.Number;
                //case FieldValueType.IconField:
                //    return ReportParameterDataType.Number;
                //case FieldValueType.PictureField:
                //    return ReportParameterDataType.Number;
                //case FieldValueType.OleField:
                //    return ReportParameterDataType.Number;
                //case FieldValueType.ChartField:
                //    return ReportParameterDataType.Number;
                //case FieldValueType.SameAsInputField:
                //    return ReportParameterDataType.Number;
                default:
                    throw new ValidationException("unknown report parameter type");
            }
        }

    }
}
