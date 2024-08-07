namespace Inspire.Security.Models
{
    [EntityConfiguration("ProfileReports", "SystemSecurity", "Profile Reports", "Profile Report")]
    [BreadCrumb(1, "UserProfiles", "SystemSecurity", "User Profiles", "ProfileID")]
    public class ProfileReport : ToggableMakerChecker<int>
    {
        //[Column("ID")]
        [TableColumn(order: 1, width: 50, isKey: true)]
        public override int Id { get; set; }
        public string ProfileID { get; set; }
        [TableColumn(order: 2)]
        public string ReportName { get => Report == null ? "" : Report.Name; }
        public int ReportID { get; set; }
        public string ProfileFormats { get; set; }
        [TableColumn(order: 3, displayName: "Allowed Formats")]
        public string FormatDescription
        {
            get
            {
                return ReportFormatConfiguration.GetDescription(ProfileFormats);
            }
        }
        [ForeignKey("ReportID")]
        public virtual Report Report { get; set; }
        [ForeignKey("ProfileID")]
        public virtual UserProfile UserProfile { get; set; }
    }
    [FormConfiguration("ProfileReports", "SystemSecurity", "Profile Reports", "Profile Report")]
    public class ProfileReportDto : ToggableMakerCheckerDto<int>
    {
        [Field(0, 0, isKey: true, controlType: ControlType.Hidden)]
        public override int Id { get; set; }
        [Field(0, 0, isKey: true, controlType: ControlType.Hidden)]
        public string ProfileID { get; set; }
        [Field(1, 1, displayName: "Report")]
        [List(Action: "GetMenus", OnSelectChange: "LoadRights")]
        public int ReportID { get; set; }
        [Field(1, 1, displayName: "Format")]
        [List(Action: "GetRights")]
        public string ProfileFormats { get; set; }

    }
}
