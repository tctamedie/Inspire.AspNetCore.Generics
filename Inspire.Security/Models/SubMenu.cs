namespace Inspire.Security.Models
{
    [EntityConfiguration("SubMenu", "Security")]
    [BreadCrumb(1, "ParentMenu", "Security", "Parent Menus", foreignKey: "ParentMenuId")]
    public class SubMenu : SortableStandard<string>
    {
        [TableColumn(order: 3, displayName: "Parent")]
        public string ParentMenuName
        {
            get => ParentMenu == null ? "" : ParentMenu.Name;
        }
        public string ParentMenuID { get; set; }
        public string ControllerName { get; set; }
        public string Icon { get; set; }
        public int SortOrder { get; set; }
        public bool CaptureAuditTrail { get; set; }
        public int Authorisers { get; set; }
        public bool Visible { get; set; }
        public bool IsReport { get => ParentMenu == null ? false : ParentMenu.IsReport; }
        public string Visibility { get => Visible ? "Yes" : "No"; }
        public ParentMenu ParentMenu { get; set; }
        public string ActionType { get; set; }
        public string LinkedPageId { get; set; }
        public SubMenu LinkedPage { get; set; }
        public List<SubMenu> PageLinks { get; set; }
    }
    [FormConfiguration("SubMenu", "Security", foreignKey: "parentMenuId", foreignKeyDesc: "Parent Menus")]
    public class SubMenuDto : SortableStandardDto<string>
    {
        [Field(1, 1, displayName: "Allowable Rights")]
        [List(Action: "GetMenus")]
        public string ActionType { get; set; }
        public string ParentMenuID { get; set; }
        public string ControllerName { get; set; }
        public string Icon { get; set; }
        [Field(5, 1)]
        public int SortOrder { get; set; }
        [Field(5, 2, displayName: "Enable Trail")]
        [List(Action: "BooleanList")]
        public bool CaptureAuditTrail { get; set; }
        [Field(6, 1)]
        public int Authorisers { get; set; }
        [Field(6, 2)]
        [List(Action: "BooleanList")]
        public bool Visible { get; set; }
        public string LinkedPageId { get; set; }

    }
    public class MenuRight
    {
        public string RightCode { get; set; }
        public string RightDescription { get; set; }
        public int Order { get; set; }
        public int Rank { get; set; }
    }
}
