using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SystemRemoteLogger.Services.Helpers;

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

        public void Disconnect()
        {
            if (IsConnectionAlive)
            {
                IsConnectionAlive = false;
               // SendMessage("disconnected from chat");
                recievingUdpClient.DropMulticastGroup(multiCastAddress);
                recievingUdpClient.Close();
            }
        }

        public void Connect()
        {
            // 224.0.0.0 to 239.255.255.255.
            multiCastAddress = IPAddress.Parse("239.0.0.222");
            senderUdpClient = new UdpClient();
            senderUdpClient.JoinMulticastGroup(multiCastAddress);
            remoteEndPoint = new IPEndPoint(multiCastAddress, port);
            //SendMessage("connected to chat");
        }

        private void SendMessage(string message)
        {
            try
            {
                string formattedMesage = string.Format($"{userName} : {message}");
                byte[] buffer = EncryptionHelper.Encode(formattedMesage);
                senderUdpClient.Send(buffer, buffer.Length, remoteEndPoint);
            }
            catch (Exception ex)
            {
                //TODO: Handle exception
                //MessageBox.Show(ex.Message, "Error while sending message");
            }
        }

        private void Listen()
        {
            try
            {
                recievingUdpClient = new UdpClient();
                IPEndPoint localEp = new IPEndPoint(IPAddress.Any, port);

                //SO_REUSEADDR 
                recievingUdpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                recievingUdpClient.ExclusiveAddressUse = false;

                recievingUdpClient.Client.Bind(localEp);

                recievingUdpClient.JoinMulticastGroup(multiCastAddress);

                while (IsConnectionAlive)
                {
                    byte[] data = recievingUdpClient.Receive(ref localEp);
                    string message = EncryptionHelper.Decode(data);
                    //Do smth!!!
                }
            }
            catch (Exception ex)
            {
                //catch
                //MessageBox.Show(ex.Message, "Error while listeting port");
            }
        }
    }
}
