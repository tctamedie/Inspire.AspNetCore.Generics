namespace Inspire.Modeller.Models.Security
{
    
    public class UserProfileReport: ModifierChecker<string>
    {
        public string UserProfileID { get; set; }
        public string SubMenuName
        {
            get => Report == null ? "" : Report.SubMenu.Name;
        }
        
        public string ReportName
        {
            get => Report == null ? "" : Report.Name;
        }
        public string ReportID { get; set; }        
        public string SubMenuID { get=>Report==null?"":Report.SubMenuID; }
        public UserProfile UserProfile { get; set; }
        public Report Report { get; set; }
    }
    public class UserProfileReportDto : ModifierCheckerDto<string> 
    {
        public string ID { get; set; }
        public string UserProfileID { get; set; }
        public string SubMenuID { get; set; }
        public string ReportID { get; set; }
    }
}
