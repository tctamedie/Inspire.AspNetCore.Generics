using DocumentFormat.OpenXml.Spreadsheet;

namespace Services.SystemSecurity
{
    
    public class ParameterRepository(SecurityContext db) : ToggableService<SystemReportParameter, SystemReportParameterDto, ReportFilterModel, int, SecurityContext>(db), IParameterRepository
    {
        

        protected override SystemReportParameterDto AlignDataTransferObject(SystemReportParameterDto row, [CallerMemberName] string caller = "")
        {
            var count = Count() + 1;
            row.Id = count;
            while (Any(s => s.Id == row.Id))
            {
                count++;
                row.Id = count;
            }

            return base.AlignDataTransferObject(row, caller);
        }

        protected override List<SystemReportParameterDto> AlignDataTransferObject(List<SystemReportParameterDto> rows, [CallerMemberName] string caller = "")
        {

            //var data = GetList().ToList();
            //List<int> keys = new();
            //rows.ForEach(row =>
            //{
            //    var count = data.Count() + 1;
            //    row.Id = count;
            //    while (data.Any(s => s.Id == row.Id) || keys.Any(s => s == row.Id))
            //    {
            //        count++;
            //        row.Id = count;
            //    }
            //    keys.Add(row.Id);

            //});

            return base.AlignDataTransferObject(rows, caller);
        }
        protected override OutputHandler Validate(SystemReportParameterDto row, int authorisers = 0, bool captureTrail = true, string capturer = "", [CallerMemberName] string caller = "")
        {
            if (Any(s => s.ReportID == row.ReportID && s.ParameterID == row.ParameterID && s.Id != row.Id))
                throw new ValidationException("Report Parameter Already Exists");
            return base.Validate(row, authorisers, captureTrail, caller);
        }
        public override IQueryable<SystemReportParameter> SearchByFilterModel(ReportFilterModel model, IQueryable<SystemReportParameter> data = null)
        {
            string reportId =  model.ReportId;
            return base.SearchByFilterModel(model, data).Where(s => s.ReportID == reportId).Include(s => s.SysParameter);
        }
        public override string GetPrependedHeader(string foreignKey)
        {
            int.TryParse(foreignKey, out int id);
            var record = db.Set<Report>().FirstOrDefault(s => s.Id == foreignKey);
            if (record != null)
                return record.Name;
            return base.GetPrependedHeader(foreignKey);
        }

        public Task<int> DeleteReportParameters(string reportId)
        {
            var records = db.SystemReportParameters.Where(s => s.ReportID == reportId);
            db.RemoveRange(records);
            return db.SaveChangesAsync();
        }
    }
}