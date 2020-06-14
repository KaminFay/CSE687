using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows;
using System.Diagnostics;
using Windows.Networking;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
namespace guiApp
{

    // State object for reading client data asynchronously  
    public class StateObject
    {
        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousSocketListener
    {
        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        StreamSocket clientSocket = new StreamSocket();
        HostName hostName = new HostName("127.0.0.1");
        static string PortNumber = "8090";

        public AsynchronousSocketListener()
        {
        }

        public async void StartListener()
        {
            var streamSocketListener = new Windows.Networking.Sockets.StreamSocketListener();
            var currentSetting = streamSocketListener.Control.QualityOfService;
            streamSocketListener.Control.QualityOfService = SocketQualityOfService.LowLatency;
            streamSocketListener.Control.KeepAlive = true;
            streamSocketListener.ConnectionReceived += this.StreamSocketListener_ConnectionReceived;

            await streamSocketListener.BindEndpointAsync(hostName,"8080");
            
        }

        public async void StreamSocketListener_ConnectionReceived(Windows.Networking.Sockets.StreamSocketListener sender, Windows.Networking.Sockets.StreamSocketListenerConnectionReceivedEventArgs args)
        {
            string request;
            using (var streamReader = new StreamReader(args.Socket.InputStream.AsStreamForRead()))
            {
                Debug.WriteLine("Reading in the request");
                request = await streamReader.ReadLineAsync();
            }

            Debug.WriteLine(request);

            //Echo the request back as the response.
            using (Stream outputStream = args.Socket.OutputStream.AsStreamForWrite())
            {
                using (var streamWriter = new StreamWriter(outputStream))
                {
                    await streamWriter.WriteLineAsync(request);
                    await streamWriter.FlushAsync();
                }
            }

        }
    }
}
