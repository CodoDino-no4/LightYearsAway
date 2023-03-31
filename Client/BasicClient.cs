using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class BasicClient
    {
        public const int PORT = 11001;

        private UdpClient udpClient;
        private IPEndPoint serverEndPoint;

        private byte[] buffer;

        private ClientPacket packetSent;
        private ClientPacket packetRecv;

        public void Init(IPAddress ip, int port)
        {
            buffer = new byte[512];

            serverEndPoint = new IPEndPoint(ip, port);

            udpClient = new UdpClient(PORT);
            udpClient.EnableBroadcast = true;
            udpClient.DontFragment = true;

            packetSent = new ClientPacket();
            packetRecv = new ClientPacket();
        }

        public void StartLoop()
        {
            _ = Task.Factory.StartNew(async () =>
            {
                try
                {
                    while (true)
                    {
                        await Send(packetSent.ClientSendPacket("Join", "8", "FromClient"));
                        Thread.Sleep(1000);

                        buffer = udpClient.Receive(ref serverEndPoint);
                        packetRecv.ClientRecvPacket(buffer);
                        Console.WriteLine($"Recieved packets from {serverEndPoint}:");

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
