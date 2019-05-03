using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using SystemRemoteLogger.Services.Helpers;

namespace SystemRemoteLogger.Services
{
    /// <summary>
    /// Represents service for managing UDP Connection and sending log messages
    /// </summary>
    public class UdpConnectionService
    {
        public delegate void EncryptedDataEventHandler(object sender, EncodingEventArgs e);
        public event EncryptedDataEventHandler NewMessageLineOn;

        #region UDP clients
        private UdpClient senderUdpClient;
        private UdpClient recievingUdpClient;
        #endregion

        #region Addresses
        private IPAddress multiCastAddress;
        private IPEndPoint remoteEndPoint;
        #endregion

        #region Configuration
        private const int port = 65000;
        public bool IsConnectionAlive { get; set; }
        private string userName = "Current OS";
        #endregion

        public UdpConnectionService(bool connectionStatus, string userName)
        {
            this.IsConnectionAlive = connectionStatus;
            this.userName = userName;
        }

        /// <summary>
        /// Disconnect user from this session 
        /// </summary>
        public void Disconnect()
        {
            if (IsConnectionAlive)
            {
                IsConnectionAlive = false;
                recievingUdpClient.DropMulticastGroup(multiCastAddress);
                recievingUdpClient.Close();
            }
        }

        /// <summary>
        /// Connect user to this session 
        /// </summary>
        public void Connect()
        {
            // 224.0.0.0 to 239.255.255.255.
            multiCastAddress = IPAddress.Parse("239.0.0.222");
            senderUdpClient = new UdpClient();
            senderUdpClient.JoinMulticastGroup(multiCastAddress);
            remoteEndPoint = new IPEndPoint(multiCastAddress, port);
        }

        /// <summary>
        /// Event hander for watchers
        /// </summary>
        /// <param name="sender">Specify sender data</param>
        /// <param name="e">Specify event arguments</param>
        public async Task SendMessage(object sender, EncodingEventArgs e)
        {
            NLog.ILogger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info(e.data);
            try
            {
                if (e.data.Length > 33)
                    e.data = e.data.Substring(0, 33) + "...";
                string formattedMesage = string.Format($"{userName} : { e.data }");
                byte[] buffer = EncryptionHelper.Encode(formattedMesage);
                senderUdpClient.Send(buffer, buffer.Length, remoteEndPoint);
            }
            catch (Exception ex)
            {
                byte[] buffer = EncryptionHelper.Encode("Exception on client device");
                senderUdpClient.Send(buffer, buffer.Length, remoteEndPoint);
            }
        }   

        /// <summary>
        /// Starts infinite listener of Udp Multicast
        /// </summary>
        public void Listen()
        {
            recievingUdpClient = new UdpClient();
            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, port);

            recievingUdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            recievingUdpClient.ExclusiveAddressUse = false;

            recievingUdpClient.Client.Bind(localEp);

            recievingUdpClient.JoinMulticastGroup(multiCastAddress);

            while (IsConnectionAlive)
            {
                byte[] data = recievingUdpClient.Receive(ref localEp);
                string stringData = EncryptionHelper.Decode(data);
                NewMessageLineOn(this, new EncodingEventArgs { data = stringData }); 
            }
        }

        /// <summary>
        /// Returns local IP address of local network
        /// </summary>
        /// <returns>Local IP address</returns>
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
