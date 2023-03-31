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

        private ServerPacket packetSent;
        private ServerPacket packetRecv;

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
                        Console.WriteLine($"Received packets from {remoteEndpoint}:");

                        await SendTo(packetSent.ServerSendPacket("Join", "FromServer"), remoteEndpoint);
                        Thread.Sleep(1000);


                        //// Join Resp
                        //if (Convert.ToInt32(bufferSegment) == 1)
                        //{
                        //    byte[] joinResp = ClientJoin();
                        //    await SendTo(joinResp, remoteEndpoint);
                        //};

                        //// Leave Resp
                        //if (Convert.ToInt32(bufferSegment) == 2)
                        //{
                        //    byte[] leaveResp = ClientLeave();
                        //    await SendTo(leaveResp, remoteEndpoint);
                        //};

                        //// Move Resp
                        //if (Convert.ToInt32(bufferSegment) == 3)
                        //{
                        //    byte[] moveResp = ClientMove();
                        //    await SendTo(moveResp, remoteEndpoint);
                        //};

                        //// Place Resp
                        //if (Convert.ToInt32(bufferSegment) == 4)
                        //{
                        //    byte[] placeResp = ClientPlace();
                        //    await SendTo(placeResp, remoteEndpoint);
                        //};


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
        public byte[] ClientJoin()
        {
            if (!conns.ContainsKey(remoteEndpoint.Address))
            {
                conns.Add(remoteEndpoint.Address, conns.Count() + 1);
            }

            return packetSent.ServerSendPacket("Join", "Join Response");
        }

        public byte[] ClientLeave()
        {
            if (conns.ContainsKey(remoteEndpoint.Address))
            {
                conns.Remove(remoteEndpoint.Address);
            }

            return packetSent.ServerSendPacket("Leave", "Leave Response"); ;
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
