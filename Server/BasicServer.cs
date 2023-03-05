using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public class BasicServer
    {

        public const int PORT = 4000;

        private Socket? udpServer;
        private EndPoint? endPoint;

        private byte[]? buffer;
        private ArraySegment<byte> bufferSegment;
        private bool disposedValue;
        private ArrayList? conns;

        public void Init()
        {
            Console.WriteLine("Server intialisaing...");

            buffer = new byte[4096];
            bufferSegment = new ArraySegment<byte>(buffer);
            conns = new ArrayList();

            // IP and PORT
            IPAddress IPADD = IPAddress.Loopback;
            endPoint = new IPEndPoint(IPADD, PORT);

            udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpServer.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
            udpServer.Bind(endPoint);

            Console.WriteLine("Server successfully intialised on: " + IPADD + ":" + PORT);

        }

        public void StartLoop(byte[] data)
        {

            _ = Task.Factory.StartNew(async () =>
            {
                SocketReceiveMessageFromResult res;
                try
                {
                    while (true)
                    {
                        res = await udpServer.ReceiveMessageFromAsync(bufferSegment, SocketFlags.None, endPoint);
                        await SendTo(res.RemoteEndPoint, data);

                        //await RecieveFrom(res.RemoteEndPoint);

                        string bitString = BitConverter.ToString(data);
                        Console.WriteLine(bitString);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            });

        }

        public async Task SendTo(EndPoint recipient, byte[] data)
        {
            var s = new ArraySegment<byte>(data);
            await udpServer.SendToAsync(s, SocketFlags.None, recipient);
        }

        public async Task RecieveFrom(EndPoint recipient)
        {
            var s = new ArraySegment<byte>();
            _ = await udpServer.ReceiveFromAsync(s, SocketFlags.None, recipient);
        }
    }
}
