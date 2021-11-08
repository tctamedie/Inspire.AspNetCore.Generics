namespace Inspire.Modeller.Models.Security
{
    public class SystemUserProfile: MakerChecker<string>
    {
        public string UserProfileID { get; set; }
        public string UserID { get; set; }
        public string ProfileName
        {
            get => UserProfile == null ? "" : UserProfile.Name;
        }
        
        public bool Active { get; set; }
        public string ActiveProfile { get => Active ? "Yes" : "No"; }       
        public UserProfile UserProfile { get; set; }
        public SystemUser SystemUser { get; set; }
    }
    public class SystemUserProfileDto  : MakerCheckerDto<string>
    {
        
        public string UserProfileID { get; set; }
        public bool Active { get; set; }
        public string UserID { get; set; }
        
    }
}
