namespace Inspire.Security.Application
{
    public class ReportGroupFilterModel : RecordStatusFilter
    {
        [TableFilter(1, ControlType: ControlType.Hidden, Width: 2)]
        public string ProfileName { get; set; }
        [TableFilter(1, Width: 3, Name: "Report Group")]
        [List(Action: "GetReportGroups")]
        public string ReportGroupId { get; set; }
    }
    public interface IProfileReportRepository : IToggableService<ProfileReport, ProfileReportDto, ReportGroupFilterModel, int>
    {
        List<GenericData<string>> GetAllowableReports(string menuId);
        List<GenericData<string>> GetReportGroups();
        List<GenericData<string>> GetAllowableFormats(int menuId);
    }
    
}