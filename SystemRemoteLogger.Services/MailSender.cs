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
            SendMail(_provider.MailFrom, _provider.MailTo, _provider.Password, $"Logging - {DateTime.Now.ToString()}", e.data, "");   
        }

        public void SendMail(string mailFrom, string mailTo, string password, string subject, string message, string filePath)
        {
            using (MailMessage mail = new MailMessage())
            {
               
                string signature = @"<p><strong><span style=""font-family: Arial, sans-serif; font-size: 11pt; color: #39c2d7; text-transform: uppercase;"" data-bind=""text: name"">Herman Stashynski</span></strong> <br /> <strong><span style=""font-family: Arial, sans-serif; font-size: 10pt; color: #464547;"" data-bind=""text: jobTitle"">Computer Administrator</span></strong> <br /> <br /> <span style=""font-family: Arial, sans-serif; font-size: 8pt; color: #999999;""> CONFIDENTIALITY CAUTION AND DISCLAIMER<br /> This message was created and send automatically by System Remote Logger (SRL) API. Please, do not reply. </span></p>";
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
                    client.Send(mail);
                }
                catch(Exception e)
                {
                    throw new MailLoggingException("Error while sending email. Please, check your mail credentials ot SMTP configuration");
                }
            }
        }
    }
}
