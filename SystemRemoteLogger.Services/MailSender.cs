using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
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
        IConfigurationProvider _provider;


        public MailSender(int port, string host, IConfigurationProvider provider)
        {
            _port = port;
            _host = host;
            _provider = provider;
        }

        public void SendMessage(object sender, EncodingEventArgs e)
        {
            SendMail(_provider.MailFrom, _provider.MailTo, _provider.Password, "Logging", e.data, "");   
        }

        public void SendMail(string mailFrom, string mailTo, string password, string subject, string message, string filePath)
        {
            using (MailMessage mail = new MailMessage())
            {
               
                mail.From = new MailAddress(mailFrom);
                mail.To.Add(mailTo);
                mail.Subject = subject;
                mail.Body = message;

                SmtpClient client = new SmtpClient(_host, _port);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mailFrom, password);

                client.Send(mail);
            }
        }
    }
}
