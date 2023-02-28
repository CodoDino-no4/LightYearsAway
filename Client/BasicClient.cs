using System.Net;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Client
{
    public class BasicClient
    {
        private Socket? udpClient;
        private IPEndPoint? serverEndPoint;
        private byte[]? buffer;
        private ArraySegment<byte> bufferSegment;

        public void Init(IPAddress ip, int port)
        {
            buffer = new byte[1048];
            bufferSegment = new ArraySegment<byte>(buffer);

            serverEndPoint = new IPEndPoint(ip, port);

            udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpClient.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);

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
                        await Send(data);

                        await Recieve();

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

        public async Task Send(byte[] data)
        {
            var s = new ArraySegment<byte>(data);
            _ = await udpClient.SendToAsync(s, SocketFlags.None, serverEndPoint);
        }

        public async Task Recieve()
        {
            var s = new ArraySegment<byte>();
            _ = await udpClient.ReceiveFromAsync(s, SocketFlags.None, serverEndPoint);

        }
    }
}
