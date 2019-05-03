using Microsoft.Extensions.Configuration;
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
        static ConfigurationProvider()
        {
            BuildConfig();
        }

        public string MailFrom { get => ConfigurationManager.AppSettings["mailFrom"]; }
        public string Password { get => ConfigurationManager.AppSettings["password"]; }
        public string MailTo { get => ConfigurationManager.AppSettings["mailTo"]; }
        public string Directory { get => ConfigurationManager.AppSettings["directory"]; }
        public bool UdpLoggingOn { get => bool.Parse(ConfigurationManager.AppSettings["udpLoggingOn"]); }
        public bool EmailLoggingOn { get => bool.Parse(ConfigurationManager.AppSettings["emailLoggingOn"]); }
        public int Port { get => int.Parse(ConfigurationManager.AppSettings["port"]); }
        public string SmtpHost { get => ConfigurationManager.AppSettings["smtpHost"]; }
        public static string UserName { get => ConfigurationManager.AppSettings["userName"]; }
        public string Signature { get => config["signature"]; }
        public string MailHeader { get => config["mailHeader"]; }
        public string MailSubject { get => config["mailSubject"]; }

        private static IConfiguration config;

        private static void BuildConfig()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json", optional: true, reloadOnChange: true).Build();
            config = configuration;
        }
    }
}
