


namespace Inspire.Security.Infrastructure
{
    

    public class EmailRepository : IEmailRepository
    {
        public EmailQueue EmailQueue { get; set; }
        public EmailSetting EmailSetting { get; set; }
        SecurityContext db;
        IEncryptionService encryptionService { get; set; }
        public MoreOptions MoreOptions { get; set; }
        public EmailRepository(SecurityContext db, IOptions<EmailSetting> options, IOptions<MoreOptions> moreOptions)
        {
            EmailQueue = new EmailQueue();
            EmailSetting = options.Value;
            MoreOptions = moreOptions.Value;
            this.db = db;
        }
        public OutputHandler SendEmail(int QueueID, bool passwordRecovery = false)
        {
            OutputHandler output = new();
            try
            {


                var row = db.EmailQueues.Where(s => s.Id == QueueID).Include(s => s.MailingGroup).ThenInclude(s => s.EmailConfiguration).FirstOrDefault();
                if (row != null)
                {
                    MailMessage mail = new();
                    SmtpClient stmpServer = new(row.MailingGroup.EmailConfiguration.SmtpServer);
                    mail.From = new MailAddress(row.MailingGroup.SenderEmail, row.MailingGroup.SenderName);
                    if (row.SentCount != row.Recurrence)
                    {
                        string[] mainRecipients = row.To.Split(',');


                        foreach (var address in mainRecipients)
                            mail.To.Add(address);
                        if (!string.IsNullOrEmpty(row.CarbonCopy))
                        {
                            string[] ccRecipients = row.CarbonCopy.Split(',');
                            foreach (var address in ccRecipients)
                                mail.CC.Add(address);
                        }
                        if (!string.IsNullOrEmpty(row.BlindCarbonCopy))
                        {
                            string[] bccRecipients = row.BlindCarbonCopy.Split(',');
                            foreach (var address in bccRecipients)
                                mail.Bcc.Add(address);
                        }
                        mail.Subject = row.Subject;
                        mail.IsBodyHtml = true;
                        mail.Body = row.EmailBody;
                        stmpServer.Port = row.MailingGroup.EmailConfiguration.ClientPortNo;
                        stmpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                        stmpServer.Credentials = new System.Net.NetworkCredential(row.MailingGroup.SenderEmail, encryptionService.Decrypt(row.MailingGroup.SenderPassword,""));
                        stmpServer.EnableSsl = row.MailingGroup.EmailConfiguration.EnableSsl;
                        stmpServer.Send(mail);
                        output = string.Format("Successfully Sent Email to Address {0}", row.To).Formator();
                        row.SentCount = row.SentCount + 1;
                        db.SaveChanges();


                    }
                    else
                    {
                        output = "The Email was sent already".Formator(true);
                    }
                }
            }
            catch (Exception e)
            {

                output = e.Catch("EmailRepository", "SendEmail");
            }

            return output;
        }
        OutputHandler SendEmail(EmailQueue row, MailingGroup group)
        {
            OutputHandler output = new();
            try
            {
                MailMessage mail = new();
                SmtpClient stmpServer = new(group.EmailConfiguration.SmtpServer);
                mail.From = new MailAddress(group.SenderEmail, group.SenderName);

                string[] mainRecipients = row.To.Split(',');

                foreach (var address in mainRecipients)
                    mail.To.Add(address);
                if (!string.IsNullOrEmpty(row.CarbonCopy))
                {
                    string[] ccRecipients = row.CarbonCopy.Split(',');
                    foreach (var address in ccRecipients)
                        mail.CC.Add(address);
                }
                if (!string.IsNullOrEmpty(row.BlindCarbonCopy))
                {
                    string[] bccRecipients = row.BlindCarbonCopy.Split(',');
                    foreach (var address in bccRecipients)
                        mail.Bcc.Add(address);
                }

                //mail.Bcc.Add(new MailAddress("lkaonga.rbm.mw"));
                mail.Subject = row.Subject;
                mail.IsBodyHtml = true;
                mail.Body = row.EmailBody;
                stmpServer.Port = group.EmailConfiguration.ClientPortNo;
                stmpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                stmpServer.Credentials = new System.Net.NetworkCredential(group.SenderEmail, encryptionService.Decrypt(group.SenderPassword,""));
                stmpServer.EnableSsl = EmailSetting.EnableSSL;
                stmpServer.Send(mail);
                output = string.Format("Successfully Sent Email to Address {0}", row.To).Formator();
            }
            catch (Exception ex)
            {
                output = ex.Catch("EmailRepository", "SendEmail");
            }
            return output;
        }
        public OutputHandler SendEmail(EmailQueue row)
        {
            OutputHandler output = new();

            try
            {
                var groupId = row.MailingGroupId ?? "REGISTRATION";
                var group = db.Set<MailingGroup>().Where(s => s.Id == groupId).Include(s => s.EmailConfiguration).FirstOrDefault();
                output = SendEmail(row, group);
            }
            catch (Exception e)
            {

                output = e.Catch("EmailRepository", "SendEmail");
            }

            return output;
        }
        public OutputHandler SendEmail(int id)
        {
            OutputHandler output = new();
            EmailSetting EmailSetting = new();
            try
            {
                var row = db.EmailQueues.Where(s => s.Id == id).Include(s => s.MailingGroup).FirstOrDefault();
                if (row == null)
                    return "The Email Queue ID Does not Exist".Formator(true);
                SendEmail(row, row.MailingGroup);
            }
            catch (Exception e)
            {

                output = e.Catch("EmailRepository", "SendEmail");
            }

            return output;
        }
        public List<OutputHandler> SendEmail(List<EmailQueue> rows)
        {
            List<OutputHandler> output = new();
            EmailSetting EmailSetting = new();
            try
            {

                MailMessage mail = new();
                SmtpClient stmpServer = new(EmailSetting.SMTPServer);
                mail.From = new MailAddress(EmailSetting.SenderEmail, EmailSetting.SenderUsername);
                foreach (var row in rows)
                {
                    output.Add(SendEmail(row, row.MailingGroup));
                }
            }
            catch (Exception e)
            {

                output.Add(e.Catch("EmailRepository", "SendEmail"));
            }

            return output;
        }
        public int QueueEmail(EmailQueue row)
        {
            db.EmailQueues.Add(row);
            db.SaveChanges();

            //remove this when scheduler start running
            if(MoreOptions.CanSendEmails) 
                SendEmail(row);

            return row.Id;

        }
    }
}