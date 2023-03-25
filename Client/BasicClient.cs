using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class BasicClient
    {
        private UdpClient udpClient;
        private IPEndPoint serverEndPoint;
        private byte[] buffer;
        private ArraySegment<byte> bufferSegment;

        private Packet packet;

        public void Init(IPAddress ip, int port)
        {
            buffer = new byte[1024];

            serverEndPoint = new IPEndPoint(ip, port);

            udpClient = new UdpClient(Int32.Parse("11001"));
            udpClient.EnableBroadcast = true;
            udpClient.DontFragment = true;

            packet = new Packet();

        }

        public void StartLoop()
        {
            _ = Task.Factory.StartNew(async () =>
            {
                try
                {
                    while (true)
                    {
                        await Send(packet.MakeBytes("Join", "1", "FromClient"));
                        Console.WriteLine("Message sent to the broadcast address");

                        bufferSegment = udpClient.Receive(ref serverEndPoint);
                        Console.WriteLine($"Recieved packets from {serverEndPoint}: {Encoding.UTF8.GetString(bufferSegment)}");

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
            _ = await udpClient.SendAsync(data, serverEndPoint);
        }
    }
}
