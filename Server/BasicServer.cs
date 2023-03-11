using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class BasicServer
    {

        public const int PORT = 4000;

        private Socket udpServer;
        private EndPoint serverEndpoint;
        private EndPoint remoteEndpoint;

        private byte[] buffer;
        private ArraySegment<byte> bufferSegment;
        private bool disposedValue;
        private ArrayList conns;

        public void Init()
        {
            Console.WriteLine("Server intialisaing...");

            buffer = new byte[4096];
            bufferSegment = new ArraySegment<byte>(buffer);
            conns = new ArrayList();

            //server ep
            serverEndpoint = new IPEndPoint(IPAddress.Any, PORT);

            // Remote ep
            remoteEndpoint = new IPEndPoint(IPAddress.Any, 0);

            udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpServer.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
            udpServer.Bind(serverEndpoint);

            Console.WriteLine("Server successfully intialised on: " + IPAddress.Any + ":" + PORT);

        }

        public void StartLoop(byte[] data)
        {

            _ = Task.Run(async () =>
            {
                SocketReceiveMessageFromResult res;

                while (true)
                {
                    res = await udpServer.ReceiveMessageFromAsync(bufferSegment, SocketFlags.None, remoteEndpoint);
                    await SendTo(res.RemoteEndPoint, data);

                    Console.WriteLine(res);

                }
            });

        }

        public async Task SendTo(EndPoint recipient, byte[] data)
        {
            var s = new ArraySegment<byte>(data);
            await udpServer.SendToAsync(s, SocketFlags.None, recipient);
        }
    }
}
