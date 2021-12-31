namespace Inspire.Modeller.Models.Security
{
    [EntityConfiguration("UserProfile","Security")]
    public class UserProfile: StandardModifierChecker<string>
    {
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
        [Link("UserProfileMenu")]
        public List<UserProfileMenu> UserProfileMenus { get; set; }
        [Link("UserProfileReport")]
        public List<UserProfileReport> UserProfileReports { get; set; }
    }
    [FormConfiguration("UserProfile","Security")]
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
