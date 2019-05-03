using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SystemRemoteLogger.Services.Helpers;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represents maling sender based on System.Net.Mail
    /// </summary>
    public class MailSender : IMailSender
    {
        private int _port;
        private string _host;
        private IConfigurationProvider _provider;

        public MailSender(int port, string host, IConfigurationProvider provider)
        {
            _port = port;
            _host = host;
            _provider = provider;
        }

        /// <summary>
        /// Event hander for watchers
        /// </summary>
        /// <param name="sender">Specify sender data</param>
        /// <param name="e">Specify event arguments</param>
        public async Task SendMessage(object sender, EncodingEventArgs e)
        {
            await SendMail(_provider.MailFrom, _provider.MailTo, _provider.Password, $"{_provider.MailSubject} - {DateTime.Now.ToString()}", e.data, "");   
        }

        /// <summary>
        /// Represents method for sending async message through SMTP protocol
        /// </summary>
        /// <param name="mailFrom">Author email</param>
        /// <param name="mailTo">Reciever email</param>
        /// <param name="password">Author password</param>
        /// <param name="subject">Mail subject</param>
        /// <param name="message">Message content (excluding signature and header)</param>
        /// <param name="filePath">Path to file</param>
        public async Task SendMail(string mailFrom, string mailTo, string password, string subject, string message, string filePath)
        {
            using (MailMessage mail = new MailMessage())
            {

                string signature = _provider.Signature.Replace(GlobalConstants.UsernameTokenPattern, ConfigurationProvider.UserName);
                mail.From = new MailAddress(mailFrom);
                mail.To.Add(mailTo);
                mail.Subject = subject;
                mail.Body = _provider.MailHeader.Replace(GlobalConstants.MessageTokenPattern, message).Replace(GlobalConstants.SignatureTokenPattern, signature);
                mail.IsBodyHtml = true;

                SmtpClient client = new SmtpClient(_host, _port);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(mailFrom, password);

                try
                {
                    await client.SendMailAsync(mail);
                }
                catch
                {
                    throw new MailLoggingException("Error while sending email. Please, check your mail credentials ot SMTP configuration");
                }
            }
        }
    }
}
