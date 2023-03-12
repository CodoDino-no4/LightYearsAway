using System.Net;
using System.Net.Sockets;
using System.Text;

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
            buffer = new byte[4096];
            bufferSegment = new ArraySegment<byte>(buffer);

            serverEndPoint = new IPEndPoint(ip, port);

            udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpClient.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
        }

        public void StartLoop(byte[] data)
        {
            _ = Task.Factory.StartNew(async () =>
            {
                try
                {
                    while (true)
                    {
                        await Send(data);
                        Console.WriteLine("Message sent to the broadcast address");

                        await Recieve(buffer);
                        Console.WriteLine($"{serverEndPoint}: {Encoding.UTF8.GetString(buffer)}");

                    }

                }
                catch (SocketException e)
                {
                    Console.WriteLine(e);

                }
                finally
                {
                    udpClient.Close();
                }
            });
        }

        public async Task Send(byte[] data)
        {
            var s = new ArraySegment<byte>(data);
            _ = await udpClient.SendToAsync(s, serverEndPoint);
        }

        public async Task Recieve(byte[] data)
        {
            _ = await udpClient.ReceiveAsync(data);
        }
    }
}
