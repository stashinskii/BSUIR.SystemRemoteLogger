using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SystemRemoteLogger.WPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class UdpLoggingScreen : Window
    {
        private bool IsConnectionAlive;
        private string userName = "Herman Stashynski";

        public UdpLoggingScreen()
        {
            InitializeComponent();
            buttonSend.IsEnabled = false;
            IsConnectionAlive = false;
            Closing += this.OnWindowClosing;
            LocalIpBox.Text = GetLocalIPAddress();
            buttonDisconnect.IsEnabled = false;
        }
    }
}
