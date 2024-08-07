namespace Inspire.Security.Models
{
    public enum ParentMenuType
    {
        Report,
        Menu,
        Workflow
    }
    [EntityConfiguration("ParentMenu", "Security")]
    public class ParentMenu : SortableStandard<string>
    {
        public ParentMenu()
        {
            SubMenus = new HashSet<SubMenu>();
        }

        public string AreaName { get; set; }
        public string Icon { get; set; }
        public int SortOrder { get; set; }
        public ParentMenuType ParentMenuType { get; set; }
        public int SubMenu { get => SubMenus.Count; }
        [Link("SubMenu")]
        public ICollection<SubMenu> SubMenus { get; set; }
    }
    [FormConfiguration("ParentMenu", "Security")]
    public class ParentMenuDto : SortableStandardDto<string>
    {

        public string AreaName { get; set; }
        public string Icon { get; set; }
        [Field(2, 1)]
        public int SortOrder { get; set; }
        [Field(2, 2)]
        [List(Action: "GetParentMenuTypes")]
        public ParentMenuType ParentMenuType { get; set; }

    }
}
