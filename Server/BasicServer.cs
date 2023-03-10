using SharpFont;
using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public class BasicServer
    {

        public const int PORT = 4000;

        private Socket? udpServer;
        private EndPoint? serverEndPoint;
        private EndPoint remoteEndPoint;

        private byte[]? buffer;
        private ArraySegment<byte> bufferSegment;
        private bool disposedValue;
        private ArrayList? conns;

        public void Init()
        {
            Console.WriteLine("Server intialisaing...");

            buffer = new byte[1024];
            bufferSegment = new ArraySegment<byte>(buffer);
            conns = new ArrayList();

            udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpServer.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);

            // IP and PORT
            IPAddress IPADD = IPAddress.Any;
            serverEndPoint = new IPEndPoint(IPADD, PORT);

            udpServer.Bind(serverEndPoint);

            remoteEndPoint = new IPEndPoint(IPAddress.Loopback, 4000);

            Console.WriteLine("Server successfully intialised on: " + IPADD + ":" + PORT);

        }

        // When a client joins the server, it is assigned an ID and added to known connections
        public void MessageLoop(byte[] data)
        {
            _ = Task.Factory.StartNew(async () =>
            {
                //SocketReceiveMessageFromResult res;

                try
                {
                    while (true)
                    {
                        //await RecieveFrom(remoteEndPoint);

                        //string bitString = BitConverter.ToString(data);
                        //Console.WriteLine(bitString);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            });
        }
        public async Task RecieveFrom(EndPoint recipient)
        {
            var s = new ArraySegment<byte>();
            _ = await udpServer.ReceiveFromAsync(s, recipient);

        }

        //public async Task SendTo(EndPoint recipient, byte[] data)
        //{
        //    var s = new ArraySegment<byte>(data);
        //    await udpServer.SendToAsync(s, SocketFlags.None, recipient);
        //}

                //while (conns.Count < 8)
                //{
                //    //res = await udpServer.ReceiveMessageFromAsync(bufferSegment, SocketFlags.None, remoteEndPoint);
                //    //await RecieveFrom(res.RemoteEndPoint);

                //    //string data = res.PacketInformation.ToString();
                //    //Console.WriteLine(data);
                //}
    }
}
