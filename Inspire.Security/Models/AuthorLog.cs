namespace Inspire.Security.Models
{
    public class AuthorLog
    {
        public string Table { get; set; }
        public string Username { get; set; }
        public DateTime AuthorDate { get; set; }
        public DateTime? RecordCaptureDate { get; set; }
        public string Value { get; set; }
        public int AuthorCount { get; set; }
        public string RecordKey { get; set; }
    }
}
