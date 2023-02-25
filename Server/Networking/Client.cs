using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class Client : IDisposable
    {
        // default values
        public int PORT = 5000;
        public string IPADRR = "127.0.0.1";

        private UdpClient udpClient;
        private IPEndPoint serverEndPoint;

        private byte[] dataRecv;
        private ArraySegment<byte> dataRecvSegment;

        private bool disposedValue;

        public Client()
        {
            dataRecv = new byte[4096];
            dataRecvSegment = new ArraySegment<byte>(dataRecv);
            udpClient = new UdpClient();
        }


        public void Connect(string ip, int port)
        {
            Console.WriteLine("Initalisaing connection...");

            IPADRR = ip;
            PORT = port;

            IPAddress serverIP = IPAddress.Parse(IPADRR); //localhost
            serverEndPoint = new IPEndPoint(serverIP, PORT);

            try
            {
                udpClient.Connect(serverEndPoint);

                string data = "the packet has been sent oh yea";

                dataRecv = Encoding.ASCII.GetBytes(data);

                udpClient.Send(dataRecv);

            }
            catch (SocketException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("Connection Established");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BasicServer()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}
