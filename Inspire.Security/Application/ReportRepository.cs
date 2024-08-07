

using Inspire.Security.Models;

namespace Inspire.Security.Application;

public class ReportFilter: StandardStatusFilter
{
    [TableFilter(1)]
    public string SubMenuID { get; set; }
}
public interface IReportService : IStandardMakerCheckerService<Report, ReportDto, string, ReportFilter>
{
    //Task<List<ReportTab>> GetReportTabs(string reportID);
    Task DeleteReportTabs(string reportID);
    //Task AddReportTabs(List<ReportTab> data);
    Task<List<InputField>> GetInputFields();
    Task<OutputHandler> CreateReport(ReportDto report, List<ReportParamater> parameters, string createBy, int authorisers = 0, bool captureTrail = true);
    Task<OutputHandler> UpdateReport(ReportDto report, List<ReportParamater> parameters, string createBy, int authorisers = 0, bool captureTrail = true);
    List<GenericData<string>> GetReportGroups();
    List<GenericData<string>> GetReportFormats(string formatCode);
    List<GenericData<string>> GetAllowableFormats(int reportId);
    List<GenericData<string>> GetReports(string reportGroupId);
}

