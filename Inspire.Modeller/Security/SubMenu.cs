namespace Inspire.Modeller.Models.Security
{
    [EntityConfiguration("SubMenu","Security")]
    [BreadCrumb(1, "ParentMenu", "Security", "Parent Menus", foreignKey: "ParentMenuId")]
    public class SubMenu : Standard<string>
    {
        [Column(order:3, displayName:"Parent")]
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
        public string Visibility { get=>Visible?"Yes":"No"; }
        
        public ParentMenu ParentMenu { get; set; }
        public bool Creatable { get; set; }
        public bool Readable { get; set; }
        public bool Updatable { get; set; }
        public bool Deletable { get; set; }
        public bool Authorizable { get; set; }
        public bool RetrieveReports { get; set; }
    }
    [FormConfiguration("SubMenu","Security")]
    public class SubMenuDto : StandardDto<string>
    {
        [Field(2,1)]
        [List(Action: "BooleanList")]
        public bool Creatable { get; set; }
        [Field(2, 2)]
        [List(Action: "BooleanList")]
        public bool Readable { get; set; }
        [Field(3, 1)]
        [List(Action: "BooleanList")]
        public bool Updatable { get; set; }
        [Field(3, 2)]
        [List(Action: "BooleanList")]
        public bool Deletable { get; set; }
        [Field(4, 1)]
        [List(Action: "BooleanList")]
        public bool Authorizable { get; set; }
        [Field(4, 2)]
        [List(Action: "BooleanList")]
        public bool RetrieveReports { get; set; }
        public string ParentMenuID { get; set; }
        public string ControllerName { get; set; }
        public string Icon { get; set; }
        [Field(5, 1)]
        public int SortOrder { get; set; }
        [Field(5, 2,displayName:"Enable Trail")]
        [List(Action: "BooleanList")]
        public bool CaptureAuditTrail { get; set; }
        [Field(6, 1)]
        public int Authorisers { get; set; }
        [Field(6, 2)]
        [List(Action:"BooleanList")]
        public bool Visible { get; set; }

    }
}
