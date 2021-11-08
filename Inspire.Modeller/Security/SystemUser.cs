using System.Collections.Generic;

namespace Inspire.Modeller.Models.Security
{
    
    public class SystemUser: StandardModifierChecker<string>
    {   
        public string Password { get; set; }
        public bool Active { get; set; }
        public List<SystemUserProfile> SystemUserProfiles { get; set; }
    }
    public class SystemUserDto: StandardModifierCheckerDto<string>
    {
              
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}
