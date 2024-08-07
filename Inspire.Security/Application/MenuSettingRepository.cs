namespace Inspire.Security.Application
{
    public class ParentFilterModel : RecordStatusFilter
    {
        [TableFilter(Order: 1, ControlType: ControlType.Hidden)]
        public string ParentMenuId { get; set; }
    }
    public interface IMenuSettingRepository : ISortableStandardService<SubMenu, SubMenuDto, ParentFilterModel, string>
    {
        List<GenericData<string>> GetAllowableRights(string menuId);
        List<GenericData<string>> GetAllowableMenus(string parentMenuId);
        List<GenericData<int>> GetAuditPrinciples();
        List<GenericData<string>> GetReportGroups();
        List<GenericData<string>> GetWorkflowGroups();
    }
    
}