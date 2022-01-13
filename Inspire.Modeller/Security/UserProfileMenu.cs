namespace Inspire.Modeller.Models.Security
{
    [EntityConfiguration("UserProfileMenu","Security", "User Access Rights")]
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
    [FormConfiguration("UserProfileMenu", "Security",header: "Role Access Rights",modal:"Role Access Right", foreignKey:"userProfileId", "User Profiles")]
    public class UserProfileMenuDto: ModifierCheckerDto<string>
    {
        public string ID { get; set; }        
        public string UserProfileID { get; set; }
        [Field(1, 1, displayName: "Parent Menu")]
        [List(Controller:"ParentMenu", Area:"Security", OnSelectChange:"LoadSubMenu")]
        public string ParentMenuID { get; set; }
        [List(Controller:"SubMenu", Area:"Security")]
        [Field(1, 2, displayName: "Sub Menu")]
        public string SubMenuID { get; set; }
        [Field(2, 1)]
        [List(Action: "BooleanList")]
        public bool CanCreate { get; set; }
        [Field(2, 2)]
        [List(Action: "BooleanList")]
        public bool CanRead { get; set; }
        [Field(3, 1)]
        [List(Action: "BooleanList")]
        public bool CanUpdate { get; set; }
        [Field(3, 2)]
        [List(Action: "BooleanList")]
        public bool CanDelete { get; set; }
        [Field(4, 1)]
        [List(Action: "BooleanList")]
        public bool CanAuthorise { get; set; }
        [Field(4, 2)]
        [List(Action: "BooleanList")]
        public bool CanRetrieveReports { get; set; }
    }
}
