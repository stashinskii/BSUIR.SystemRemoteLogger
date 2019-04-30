using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represents interface for managing mail sending
    /// </summary>
    public interface IMailSender
    {
        Task SendMail(string mailFrom, string mailTo, string password, string subject, string message, string filePath);
    }
}
