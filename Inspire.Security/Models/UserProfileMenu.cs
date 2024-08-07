using Inspire.Security.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Inspire.Security.Models
{
    [EntityConfiguration("UserProfileMenu", "Security", "User Access Rights")]
    public class UserProfileMenu : ToggableMakerChecker<string>
    {

        public override string Id { get; set; }
        public string UserProfileId { get; set; }
        public string SubMenuId { get; set; }
        [TableColumn(order: 2)]
        public string Menu { get => SubMenu == null ? "" : SubMenu.Name; }
        [StringLength(6)]
        public string AccessCode { get; set; }
        [TableColumn(order: 3, displayName: "Profile Rights")]
        public string AccessDescription { get { return RightConfiguration.GetDescription(AccessCode); } }
        [ForeignKey("UserProfileId")]
        public virtual UserProfile UserProfile { get; set; }
        [ForeignKey("SubMenuId")]
        public virtual SubMenu SubMenu { get; set; }
    }
    [FormConfiguration("UserProfileMenu", "Security", header: "Role Access Rights", modal: "Role Access Right", foreignKey: "userProfileId", "User Profiles")]
    public class UserProfileMenuDto : ToggableMakerCheckerDto<string>
    {
        [Field(0, 0, controlType: ControlType.Hidden, isKey: true)]
        public override string Id { get; set; }
        [Field(0, 0, controlType: ControlType.Hidden)]
        public string UserProfileId { get; set; }
        [Field(1, 1, displayName: "Menu")]
        [List(Action: "GetMenus", OnSelectChange: "LoadRights")]
        public string SubMenuId { get; set; }
        [Field(1, 1, displayName: "Rights")]
        [List(Action: "GetRights")]
        public string AccessCode { get; set; }
    }
}
