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

        private Dictionary<IPAddress, int> conns;
        private int clientId = 0;

        private ServerPacket packetSent;
        private ServerPacket packetRecv;

        // Tick rate
        private TimeSpan deltaTime;

        public void Init()
        {
            buffer = new byte[512];
            conns = new Dictionary<IPAddress, int>();

            udpServer = new UdpClient(PORT);
            udpServer.EnableBroadcast = true;
            udpServer.DontFragment = true;

            remoteEndpoint = new IPEndPoint(IPAddress.Broadcast, PORT);

            packetSent = new ServerPacket();
            packetRecv = new ServerPacket();

            deltaTime = TimeSpan.FromMilliseconds( 1000.0f/ 60 );

            Console.WriteLine("Server successfully intialised...");
        }

        public void StartLoop()
        {
            _ = Task.Factory.StartNew(async () =>
            {
                try
                {
                    while (true)
                    {

                        buffer = udpServer.Receive(ref remoteEndpoint);
                        packetRecv.ServerRecvPacket(buffer);

                        //Join Resp
                        if (packetRecv.cmd == 1)
                        {
                            byte[] joinResp = ClientJoin();
                            await SendTo(joinResp, remoteEndpoint);
                        };

                        // Leave Resp
                        if (packetRecv.cmd == 2)
                        {
                            byte[] leaveResp = ClientLeave();
                            await SendTo(leaveResp, remoteEndpoint);
                        };

                        // Move Resp
                        if (packetRecv.cmd == 3)
                        {
                            byte[] moveResp = ClientMove();
                            await SendTo(moveResp, remoteEndpoint);
                        };

                        // Place Resp
                        if (packetRecv.cmd == 4)
                        {
                            byte[] placeResp = ClientPlace();
                            await SendTo(placeResp, remoteEndpoint);
                        };

                        Thread.Sleep(1000);
                    }
                }
                catch (SocketException err)
                {
                    Console.WriteLine(err);
                }
                finally
                {
                    udpServer.Close();
                }
            });

        }
        public byte[] ClientJoin()
        {
            if (!conns.ContainsKey(remoteEndpoint.Address))
            {
                conns.Add(remoteEndpoint.Address, conns.Count() + 1);
                clientId = conns.GetValueOrDefault(remoteEndpoint.Address);
            }

            return packetSent.ServerSendPacket("Join", $"Client ID: {clientId}");
        }

        public byte[] ClientLeave()
        {
            if (conns.ContainsKey(remoteEndpoint.Address))
            {
                conns.Remove(remoteEndpoint.Address);
            }

            return packetSent.ServerSendPacket("Leave", $"Client ID: {clientId}"); ;
        }

        public byte[] ClientMove()
        {
            return packetSent.ServerSendPacket("Move", "Move Response");
        }

        public byte[] ClientPlace()
        {
            return packetSent.ServerSendPacket("Place", "Place Response");
        }

        public async Task SendTo(byte[] data, IPEndPoint recipient)
        {
            var s = new ArraySegment<byte>(data);
            await udpServer.SendAsync(data, recipient);
        }
    }
}
