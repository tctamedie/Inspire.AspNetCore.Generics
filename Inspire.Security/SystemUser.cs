namespace Inspire.Security
{
    [EntityConfiguration("SystemUser","Security","Users")]
    public class SystemUser: StandardModifierChecker<string>
    {   
        
        public string Password { get; set; }
        [TableColumn(order:3)]
        public bool Active { get; set; }
        [Link("SystemUserProfile", Area:"Security")]
        public List<SystemUserProfile> SystemUserProfiles { get; set; }
    }
    [FormConfiguration("SystemUser", "Security", "User")]
    public class SystemUserDto: StandardModifierCheckerDto<string>
    {    
        [Field(2, 1, controlType: ControlType.Password)]
        public string Password { get; set; }
        [Field(2, 2)]
        [List(Action:"BooleanList")]
        public bool Active { get; set; }
    }
}
