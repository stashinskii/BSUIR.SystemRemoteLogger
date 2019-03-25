﻿using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.DirectoryServices.AccountManagement;
using System.Windows.Navigation;
using System.Diagnostics;
using SystemRemoteLogger.Services;

namespace SystemRemoteLogger.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IConfigurationProvider config;
        bool? EmailLogging, UdpLogging;

        public MainWindow()
        {
            InitializeComponent();
            SetUserCard();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UdpLoggingScreen udpScreen = new UdpLoggingScreen();
            udpScreen.Show();
        }


        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

      
    }
}
