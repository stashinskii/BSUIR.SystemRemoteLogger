using System;
using System.Windows;
using System.DirectoryServices.AccountManagement;
using System.Windows.Navigation;
using System.Diagnostics;
using SystemRemoteLogger.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SystemRemoteLogger.WPF
{
    /// <summary>
    /// Represents Home page of application
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Represents fild for configuring SRL Client app
        /// </summary>
        private IConfigurationProvider config;
        private bool EmailLogging, UdpLogging;

        public MainWindow()
        {
            InitializeComponent();
            SetUserCard();
            SetPreviousInteractions();
            SetPCInfo();

            try
            {
                EmailLogging = config.EmailLoggingOn;
                UdpLogging = config.UdpLoggingOn;
                RemoteLoggingService loggingService = new RemoteLoggingService(config);
                UdpConnectionService udpService = new UdpConnectionService(true, UserPrincipal.Current.DisplayName);
                int port = config.Port;
                string host = config.SmtpHost;
                MailSender smtpService = new MailSender(port, host, config);
                try
                {
                    udpService.Connect();
                }
                catch
                {
                    MessageBox.Show("Something went wrong while setting UDP connection");
                    UdpLogging = false;
                }
                this.Subscribe(loggingService, udpService, smtpService);
            }
            catch (MailLoggingException e)
            {
                MessageBox.Show(e.Message);
            }
            
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong");
            }
        }

        private void Subscribe(RemoteLoggingService loggingService, UdpConnectionService udpService, MailSender smtpService)
        {
            if (UdpLogging)
                loggingService.NewMessageOn += udpService.SendMessage;
            if (EmailLogging)
                loggingService.NewMessageOn += smtpService.SendMessage;
            loggingService.Start();
        }

        private void SetUserCard()
        {
            string name = UserPrincipal.Current.DisplayName;
            UserCardText.Text = name;
            config = new ConfigurationProvider();
            EmailDestinationText.Text = config.MailTo;
            UdpLoggingCheck.IsChecked = config.UdpLoggingOn;
            EmailLoggingCheck.IsChecked = config.EmailLoggingOn;
        }

        private void SetPCInfo()
        {
            SystemMonitor.BLL.Service.Service monitorService = new SystemMonitor.BLL.Service.Service();
            var computerInfo = monitorService.GetComputerInfo();

            OSName.Text = computerInfo.OSName;
            MachineName.Text = computerInfo.MachineName;
        }


        private void OpenUdpScreen(object sender, RoutedEventArgs e)
        {
            UdpLoggingScreen udpScreen = new UdpLoggingScreen();
            udpScreen.Show();
        }


        private void HyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void OpenSettingsScreen(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.Show();
        }

        private void SetPreviousInteractions()
        {
            List<string> lines = File.ReadAllLines("logs/data.log").ToList();
            lines = Enumerable.Reverse(lines).Take(10).Reverse().ToList();
            foreach (var line in lines)
            {
                lastInteractionsView.Items.Add(line);
            }
        }
    }
}
