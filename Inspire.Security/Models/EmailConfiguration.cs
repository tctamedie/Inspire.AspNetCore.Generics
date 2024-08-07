namespace Inspire.Security.Models
{
    public class EmailConfiguration : Standard<string>
    {
        public string SmtpServer { get; set; }
        public int ClientPortNo { get; set; }
        public bool EnableSsl { get; set; }
    }
}
