using DocumentFormat.OpenXml.Spreadsheet;
using Inspire.Services.Infrastructure.Common;

namespace Services.SystemSecurity
{
    //public class ReportFilterModel: FilterModel
    //{
    //    [TableFilter(1, ControlType: ControlType.Hidden, Width: 2)]
    //    public int ReportId { get; set; }
    //}
    public interface IParameterRepository : IToggableService<SystemReportParameter, SystemReportParameterDto, ReportFilterModel, int>
    {
        Task<int> DeleteReportParameters(string reportId);
    }
    
}