using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represents maling sender based on System.Net.Mail
    /// </summary>
    public class MailSender : IMailSender
    {
        private int _port;
        private string _host;

        public MailSender(int port, string host)
        {
            _port = port;
            _host = host;
        }

        public void SendMail(string mailFrom, string mailTo, string password, string subject, string message, string filePath)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(mailFrom);
                mail.To.Add(mailTo);

                if (!File.Exists(filePath))
                {
                    throw new ArgumentException("Invalid file path", nameof(filePath));
                }

                using (var textFile = new Attachment(filePath))
                {
                    mail.Subject = $"Text file: {filePath}";
                    mail.Body = $"Attachment is text file with name: {filePath}";
                    mail.Attachments.Add(textFile);

                    SmtpClient client = new SmtpClient(_host, _port);
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(mailFrom, password);

                    client.Send(mail);
                }
            }
        }
    }
}
