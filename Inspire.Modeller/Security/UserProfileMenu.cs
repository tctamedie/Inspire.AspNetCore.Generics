namespace Inspire.Modeller.Models.Security
{
    public class UserProfileMenu : ModifierChecker<string>
    {

        public string UserProfileID { get; set; }
        public string ParentMenuID { get => SubMenu == null ? "" : SubMenu.ParentMenuID; }
        
        public string SubMenuName
        {
            get => SubMenu == null ? "" : SubMenu.Name;
        }
        public string ParentMenuName
        {
            get => SubMenu == null ? "" : SubMenu.ParentMenu == null ? "" : SubMenu.ParentMenu.Name;
        }

        public string SubMenuID { get; set; }
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanAuthorise { get; set; }
        public bool CanRetrieveReports { get; set; }
        public UserProfile UserProfile { get; set; }
        public SubMenu SubMenu { get; set; }
    }
    public class UserProfileMenuDto: ModifierCheckerDto<string>
    {
        public string ID { get; set; }
        public string UserProfileID { get; set; }
        public string ParentMenuID { get; set; }
        public string SubMenuID { get; set; }
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanAuthorise { get; set; }
        public bool CanRetrieveReports { get; set; }
    }
}
