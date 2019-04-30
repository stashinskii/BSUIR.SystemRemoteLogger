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
        IConfigurationProvider _provider;


        public MailSender(int port, string host, IConfigurationProvider provider)
        {
            _port = port;
            _host = host;
            _provider = provider;
        }

        public async Task SendMessage(object sender, EncodingEventArgs e)
        {
            await SendMail(_provider.MailFrom, _provider.MailTo, _provider.Password, $"Logging - {DateTime.Now.ToString()}", e.data, "");   
        }

        public async Task SendMail(string mailFrom, string mailTo, string password, string subject, string message, string filePath)
        {
            using (MailMessage mail = new MailMessage())
            {

                string signature = GlobalConstants.Signature.Replace(GlobalConstants.UsernameTokenPattern, ConfigurationProvider.UserName);
                mail.From = new MailAddress(mailFrom);
                mail.To.Add(mailTo);
                mail.Subject = subject;
                mail.Body = $"Dear administrator, <br><br>You have recieved this OS interaction: {message} <br> <br> Best regards, {signature}<br>"; 
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
                catch(Exception e)
                {
                    throw new MailLoggingException("Error while sending email. Please, check your mail credentials ot SMTP configuration");
                }
            }
        }
    }
}
