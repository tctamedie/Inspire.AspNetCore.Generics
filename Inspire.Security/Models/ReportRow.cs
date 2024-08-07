namespace Inspire.Security.Models
{
    public class ReportRow
    {
        public ReportRow()
        {
            ReportFields = new List<ReportField>();
        }
        public string ID { get; set; }
        public string ReportTabID { get; set; }
        public ReportTab ReportTab { get; set; }
        public int Order { get; set; }
        public List<ReportField> ReportFields { get; set; }
    }

}
