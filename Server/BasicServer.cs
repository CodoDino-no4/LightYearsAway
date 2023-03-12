using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class BasicServer
    {

        public const int PORT = 11000;

        private UdpClient udpServer;
        private IPEndPoint remoteEndpoint;

        private byte[] buffer;
        private ArraySegment<byte> bufferSegment;
        private bool disposedValue;
        private ArrayList conns;

        public void Init()
        {
            Console.WriteLine("Server intialisaing...");

            buffer = new byte[1024];
            bufferSegment = new ArraySegment<byte>(buffer);
            conns = new ArrayList();

            udpServer = new UdpClient(PORT);
            remoteEndpoint = new IPEndPoint(IPAddress.Any, PORT);

            Console.WriteLine("Server successfully intialised");

        }

        public void StartLoop(byte[] data)
        {
            _ = Task.Factory.StartNew(async () =>
            {
                try
                {
                    while (true)
                    {
                        Console.WriteLine("Waiting to recieve packets");
                        bufferSegment = udpServer.Receive(ref remoteEndpoint);

                        Console.WriteLine($"Received packets from {remoteEndpoint}: {Encoding.UTF8.GetString(bufferSegment)}");

                        await SendTo(data, remoteEndpoint);
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    udpServer.Close();
                }
            });

        }

        public ArraySegment<byte> RecievedBytes()
        {
            return bufferSegment;
        }

        public async Task SendTo(byte[] data, IPEndPoint recipient)
        {
            var s = new ArraySegment<byte>(data);
            await udpServer.SendAsync(data, recipient);
        }
    }
}
