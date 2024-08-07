namespace Inspire.Security.Models
{
    [EntityConfiguration("SystemUser", "Security", "Users")]
    public class SystemUser : StandardMakerChecker<string>
    {

        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public DateTime PasswordExpiryDate { get; set; }
        public string Profiles { get; set; }
        public string LoginId { get; set; }
        [TableColumn(order: 3)]
        public bool Active { get; set; }
        [Link("SystemUserProfile", Area: "Security")]
        public List<SystemUserProfile> SystemUserProfiles { get; set; }
    }
    [FormConfiguration("SystemUser", "Security", "User")]
    public class SystemUserDto : StandardMakerCheckerDto<string>
    {
        [Field(2, 1, controlType: ControlType.Password)]
        public string Password { get; set; }
        public string LoginId { get; set; }
        public DateTime PasswordExpiryDate { get; set; }
        public string EmailAddress { get; set; }

        [Field(2, 2)]
        [List(Action: "BooleanList")]
        public bool Active { get; set; }
        public string Profiles { get; set; }
    }
    public class RegisterModelView
    {
        public RegisterModelView()
        {
            output = new OutputHandler();
        }
        public string EmployeeCode { get; set; }
        public string Username { get; set; }

        public string EmailAddress { get; set; }
        public bool ActiveDirectoryAuthentication { get; set; }
        public OutputHandler output { get; set; }
    }
}
