namespace Inspire.Security.Application
{
    public class WorkflowFilterModel : RightFilterModel
    {

        [TableFilter(1, Width: 3, Name: "Parent")]
        [List(Action: "GetWorkflowMenus")]
        public override string ParentMenuId { get; set; }
    }
    public class RightFilterModel : RecordStatusFilter
    {
        [TableFilter(1, ControlType: ControlType.Hidden, Width: 2)]
        public virtual string ProfileName { get; set; }
        [TableFilter(1, Width: 3, Name: "Parent menu")]
        [List("ParentMenus")]
        public virtual string ParentMenuId { get; set; }
    }
    public interface IProfileMenuRepository : IToggableService<UserProfileMenu, UserProfileMenuDto, RightFilterModel, string>
    {
        List<GenericData<string>> GetAllowableRights(string menuId);
        List<GenericData<string>> GetAllowableMenus(string menuId);
    }
    
}