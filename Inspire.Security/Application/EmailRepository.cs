


namespace Inspire.Security.Application
{
    public interface IEmailRepository
    {
        OutputHandler SendEmail(int QueueID, bool passwordRecovery = false);
        OutputHandler SendEmail(EmailQueue row);
        OutputHandler SendEmail(int id);
        List<OutputHandler> SendEmail(List<EmailQueue> rows);
        int QueueEmail(EmailQueue row);
    }

    public class MoreOptions
    {
        public bool CanSendEmails { get; set; }
      
    }

}