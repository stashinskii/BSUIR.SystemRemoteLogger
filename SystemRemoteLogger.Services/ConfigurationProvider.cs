using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represents configuration provider based on App.config file at UI layer
    /// </summary>
    public class ConfigurationProvider : IConfigurationProvider
    {
        public string MailFrom { get => ConfigurationManager.AppSettings["mailFrom"]; }
        public string Password { get => ConfigurationManager.AppSettings["password"]; }
        public string MailTo { get => ConfigurationManager.AppSettings["mailTo"]; }
        public string Directory { get => ConfigurationManager.AppSettings["directory"]; }
    }

}
