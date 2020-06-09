using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace guiApp
{

    /// <summary>
    /// The sending socket.
    /// </summary>
    public class SendingSocket
    {
        private NetworkStream ns {get; set;}
        private System.Net.Sockets.TcpClient clientSocket { get; set; }
        private String ipAddress { get; set; }
        private int socketNumber { get; set; }

        public SendingSocket(String ipAddress, int socketNumber)
        {
            this.ipAddress = ipAddress;
            this.socketNumber = socketNumber;
        }

        public void EstablishConnection()
        {
            clientSocket = new System.Net.Sockets.TcpClient();
            clientSocket.Connect(ipAddress, socketNumber);

            ns = clientSocket.GetStream();
        }

        public void SendDataOverSocket(byte[] outgoingBuffer)
        {
            ns.Write(outgoingBuffer, 0, outgoingBuffer.Length);
        }

        public void CleanSocket()
        {
            ns.Flush();
            ns.Close();
            clientSocket.Close();
        }

    }
}
