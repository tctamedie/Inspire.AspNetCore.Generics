namespace Inspire.Security
{
    [BreadCrumb(1, "SystwmUser", "Security", "Users", foreignKey: "UserID")]
    [EntityConfiguration("SystemUserProfile","Security", "System User Profiles")]
    public class SystemUserProfile: MakerChecker<string>
    {
        [TableColumn(order:1, displayName:"User Profile")]
        public string UserProfileID { get; set; }
        public string UserID { get; set; }
        public string ProfileName
        {
            get => UserProfile == null ? "" : UserProfile.Name;
        }
        
        public bool Active { get; set; }
        [TableColumn(order:1, displayName:"Is Active")]
        public string ActiveProfile { get => Active ? "Yes" : "No"; }       
        public UserProfile UserProfile { get; set; }
        public SystemUser SystemUser { get; set; }
    }
    [FormConfiguration(controller:"SystemUserProfile","Security", "System User Profiles")]
    public class SystemUserProfileDto  : MakerCheckerDto<string>
    {
        [Field(1,1,displayName:"User Profile")]
        [List(Controller:"UserProfile", Area:"Securiy")]
        public string UserProfileID { get; set; }
        [Field(1,2,displayName:"Is Active")]
        [List(Action:"BooleanList")]
        public bool Active { get; set; }
        [Field(0,1, controlType: ControlType.Hidden)]
        public string UserID { get; set; }
        
    }
}
