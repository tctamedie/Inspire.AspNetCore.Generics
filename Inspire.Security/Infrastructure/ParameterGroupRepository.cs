using Inspire.Services;

namespace Services.SystemSecurity
{

    public class ParameterGroupRepository(SecurityContext db) : StandardMakerCheckerService<SysParameter, SysParameterDto, string, SecurityContext, StandardStatusFilter>(db), IParameterGroupRepository
    {
        
        public List<GenericData<ReportParameterDataType>> GetDataTypes()
        {
            
            return ConvertEnumToList<ReportParameterDataType>();

        }
        protected override OutputHandler Validate(SysParameterDto row, int a = 0, bool c = true, string capturer = "", [CallerMemberName] string caller = "")
        {
            if (string.IsNullOrEmpty(row.Name))
                throw new ValidationException( "Name cannot be blank");
            if (Any(s => s.Name.ToLower().Contains(row.Name.ToLower()) && s.Id.ToLower()!=row.Id.ToLower()))
                throw new ValidationException( "Record Already Exists");
            return new OutputHandler();
        }
        public override Task<OutputHandler> AddInScopeAsync(List<SysParameterDto> rows, string createBy, int authorisers = 0, bool captureTrail = true, string authoriser = "")
        {
            List<SysParameterDto> newParams = new List<SysParameterDto>();
            foreach (var row in rows)
            {
                if (Any(s => s.Id.ToLower() == row.Id.ToLower()))
                    newParams.Add(row);
            }
            if (newParams.Count == 0)
                return Task.FromResult(new OutputHandler());

            return base.AddInScopeAsync(newParams, createBy, authorisers, captureTrail);
        }
        protected override SysParameterDto AlignDataTransferObject(SysParameterDto row, [CallerMemberName] string caller = "")
        {
            if (row.Id.StartsWith("@"))
            {
                row.Id = row.Id.Substring(1);
                row.Name = row.Name.Substring(1).CamelSplit();
            }
            return base.AlignDataTransferObject(row, caller);
        }
    }
}