namespace Inspire.Security.Models
{
    [EntityConfiguration("UserProfileReport", "Security")]
    public class UserProfileReport : ModifierChecker<string>
    {

        public string UserProfileID { get; set; }
        [TableColumn(order: 1, displayName: "Sub Menu")]
        public string SubMenuName
        {
            get => Report == null ? "" : Report.SubMenu.Name;
        }
        [TableColumn(order: 2, displayName: "Report")]
        public string ReportName
        {
            get => Report == null ? "" : Report.Name;
        }

        public string ReportID { get; set; }
        public string SubMenuID { get => Report == null ? "" : Report.SubMenuID; }
        public UserProfile UserProfile { get; set; }
        public Report Report { get; set; }
    }
    [FormConfiguration("UserProfileReport", "Security", header: "Role Reports", modal: "Role Report", foreignKey: "userProfileId", foreignKeyDesc: "User Profiles")]
    public class UserProfileReportDto : ModifierCheckerDto<string>
    {
        [Field(0, 1, controlType: ControlType.Hidden)]
        public string UserProfileID { get; set; }
        [Field(1, 1, displayName: "Sub Menu")]
        [List(Controller: "SubMenu", Area: "Security")]
        public string SubMenuID { get; set; }
        [Field(1, 2, displayName: "Report")]
        [List(Controller: "Report", Area: "Security")]
        public string ReportID { get; set; }

    }
}
