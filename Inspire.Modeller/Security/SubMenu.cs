
namespace Inspire.Modeller.Models.Security
{
    
    public class SubMenu : Standard<string>
    {
        
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
    public class SubMenuDto : StandardDto<string>
    {
        public bool Creatable { get; set; }
        public bool Readable { get; set; }
        public bool Updatable { get; set; }
        public bool Deletable { get; set; }
        public bool Authorizable { get; set; }
        public bool RetrieveReports { get; set; }
        public string ParentMenuID { get; set; }
        public string ControllerName { get; set; }
        public string Icon { get; set; }
        public int SortOrder { get; set; }
        public bool CaptureAuditTrail { get; set; }
        public int Authorisers { get; set; }
        public bool Visible { get; set; }

    }
}
