using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SystemRemoteLogger.Services
{
    public class UdpConnectionService
    {
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
        private bool IsConnectionAlive;
        private string userName = "Current OS";
        #endregion
    }
}
