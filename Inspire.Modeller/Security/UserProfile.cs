using System.Collections.Generic;
namespace Inspire.Modeller.Models.Security
{
    public class UserProfile: StandardModifierChecker<string>
    {
        
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanAuthorise { get; set; }
        public bool CanRetrieveReports { get; set; }
        public List<UserProfileMenu> UserProfileMenus { get; set; }
        public List<UserProfileReport> UserProfileReports { get; set; }
    }
    public class UserProfileDto : StandardModifierCheckerDto<string>
    {
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanAuthorise { get; set; }
        public bool CanRetrieveReports { get; set; }
    }
}
