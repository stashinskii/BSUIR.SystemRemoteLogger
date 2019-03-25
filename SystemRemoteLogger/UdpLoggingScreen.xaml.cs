using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SystemRemoteLogger.Services;
using SystemRemoteLogger.Services.Helpers;

namespace SystemRemoteLogger.WPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UdpLoggingScreen : Window
    {
        private string userName = "Herman Stashynski";
        UdpConnectionService udpService;

        public UdpLoggingScreen()
        {
            InitializeComponent();
            udpService = new UdpConnectionService(false, userName);
            udpService.NewMessageOn += AddNewMessageLine;
            buttonSend.IsEnabled = false;
            Closing += this.OnWindowClosing;
            LocalIpBox.Text = udpService.GetLocalIPAddress();
            buttonDisconnect.IsEnabled = false;

        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            udpService.Disconnect();
        }

        private void ConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (userNameTextBox.Text == "" || userNameTextBox.Text == null)
                {
                    userNameTextBox.Text = userName;
                }
                userName = userNameTextBox.Text;
                udpService.Connect();
                udpService.IsConnectionAlive = true;
                Thread ListenStart = new Thread(new ThreadStart(udpService.Listen));
                ListenStart.Start();

                buttonConnect.IsEnabled = false;
                userNameTextBox.IsReadOnly = true;
                buttonSend.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while setting connection");
            }

        }

        public void AddNewMessageLine(object sender, EncodingEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                string time = DateTime.Now.ToShortTimeString();
                lisbox.Items.Add("🙍" + time + " " + EncryptionHelper.Decode(e.dataToDecode));
            });
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (messageTextBox.Text == "" || messageTextBox.Text == null)
                {
                    throw new Exception("Empty message");
                }
                udpService.SendMessage(messageTextBox.Text);
                messageTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            udpService.Disconnect();
            buttonDisconnect.IsEnabled = false;
            buttonConnect.IsEnabled = true;
        }
    }
}
