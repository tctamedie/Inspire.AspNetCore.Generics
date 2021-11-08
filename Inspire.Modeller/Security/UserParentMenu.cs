using System.Collections.Generic;

namespace Inspire.Modeller.Models.Security
{
    public class UserParentMenu
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public List<UserSubMenu> SubMenus { get; set; }
    }
}
