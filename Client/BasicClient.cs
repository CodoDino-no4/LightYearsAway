using System.Net;
using System.Net.Sockets;

namespace Client
{
    public class BasicClient
    {
        private Socket udpClient;
        private IPEndPoint serverEndPoint;
        private byte[] buffer;
        private ArraySegment<byte> bufferSegment;

        public void Init(IPAddress ip, int port)
        {
            buffer = new byte[1048];
            bufferSegment = new ArraySegment<byte>(buffer);

            serverEndPoint = new IPEndPoint(ip, port);

            udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpClient.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
            udpClient.Connect(serverEndPoint);

        }

        // Sends a packet to connect to the server
        public void JoinServer(byte[] joinData)
        {
            _ = Task.Factory.StartNew(async () =>
            {
                //SocketReceiveMessageFromResult res;

                try
                {
                     await Send(joinData);
                     //res = await udpClient.ReceiveMessageFromAsync(bufferSegment, SocketFlags.None, serverEndPoint);
                     //await Recieve();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }


            });
        }

        public async Task Send(byte[] data)
        {
            if (serverEndPoint != null)
            { 
                bufferSegment = new ArraySegment<byte>(data);
                _ = await udpClient.SendToAsync(bufferSegment, serverEndPoint);
            }
        }

        public async Task<ArraySegment<byte>> Recieve()
        {
            var s = new ArraySegment<byte>();
            _ = await udpClient.ReceiveFromAsync(s, serverEndPoint);

            return s;
        }
    }
}
