namespace Inspire.Modeller.Models.Security
{
    public class AuditLog
    {
        public string Table { get; set; }
        public string Action { get; set; }
        public string Username { get; set; }
        public DateTime AuditDate { get; set; }
        public string Value { get; set; }
        public string RecordKey { get; set; }
    }
}
