﻿namespace Inspire.Modeller.Models.Security
{
    [EntityConfiguration("ParentMenu","Security")]
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
        [Link("SubMenu")]
        public ICollection<SubMenu> SubMenus { get; set; }
    }
    [FormConfiguration("ParentMenu","Security")]
    public class ParentMenuDto : StandardDto<string>
    {
        
        public string AreaName { get; set; }
        public string Icon { get; set; }
        [Field(2,1)]
        public int SortOrder { get; set; }
        [Field(2,2)]
        [List(Action: "BooleanList")]
        public bool IsReport { get; set; }
    }
}
