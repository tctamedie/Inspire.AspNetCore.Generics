using System.Collections.Generic;

namespace Inspire.Modeller.Models.Security
{
    
    public class ParentMenu: Standard<string>
    {
        public ParentMenu()
        {
            SubMenus = new HashSet<SubMenu>();
        }
        
        public string AreaName { get; set; }
        public string Icon { get; set; }
        public int SortOrder { get; set; }
        public bool IsReport { get; set; }
        public int SubMenu { get => SubMenus.Count; }
        public ICollection<SubMenu> SubMenus { get; set; }
    }
    public class ParentMenuDto : StandardDto<string>
    {
        
        public string AreaName { get; set; }
        public string Icon { get; set; }
        
        public int SortOrder { get; set; }
        public bool IsReport { get; set; }
    }
}
