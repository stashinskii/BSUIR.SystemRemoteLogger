using System;
using System.Collections.Generic;
using System.Text;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represends interface for entitites which provides configuration for mailing service
    /// </summary>
    public interface IConfigurationProvider
    {
        string MailFrom { get; }
        string Password { get; }
        string MailTo { get; }
        string Directory { get; }
        bool UdpLoggingOn { get; }
        bool EmailLoggingOn { get; }
        int Port { get; }
        string SmtpHost { get; }
        string Signature { get; }
        string MailHeader { get; }
        string MailSubject { get; }
    }
}
