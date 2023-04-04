using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class BasicServer
    {
        public const int PORT = 11000;

        private UdpClient udpServer;
        public struct Client
        {
            public UdpClient socket;
            public IPEndPoint ep;
        }

        private UdpClient remoteClient;
        private IPEndPoint remoteEndpoint;
        private Client client;

        private byte[] sendBuff;
        private byte[] recvBuff;

        private Dictionary<IPAddress, int> conns;
        private int clientId = 0;

        private ServerPacket packetSent;
        private ServerPacket packetRecv;

        public bool messageRec = false;

        // Tick rate
        private TimeSpan deltaTime;
        private Timer tickTimer;

        public void Init()
        {
            sendBuff = new byte[512];
            recvBuff = new byte[512];

            conns = new Dictionary<IPAddress, int>();

            udpServer = new UdpClient(PORT);
            udpServer.EnableBroadcast = true;
            udpServer.DontFragment = true;

            remoteEndpoint = new IPEndPoint(IPAddress.Any, PORT);
            //remoteClient = new UdpClient(remoteEndpoint);  

            packetSent = new ServerPacket();
            packetRecv = new ServerPacket();

            deltaTime = TimeSpan.FromMilliseconds(1000.0f / 60);

            Console.WriteLine("Server successfully intialised...");
        }

        public void TickRate(object stateInfo) 
        {
            //if tickTimer hits delta time interval 
            tickTimer.Change(0, deltaTime.Milliseconds);
        }

        public void StartLoop()
        {
            _ = Task.Factory.StartNew(async () =>
            {
                try
                {
                    tickTimer = new Timer(new TimerCallback(TickRate));

                    while (true)
                    {

                        var res = await udpServer.ReceiveAsync();
                        recvBuff = res.Buffer;
                        packetRecv.ServerRecvPacket(recvBuff);

                        //Join Resp
                        if (packetRecv.cmd == 1)
                        {
                            byte[] joinResp = ClientJoin(res.RemoteEndPoint);
                            await udpServer.SendAsync(joinResp, res.RemoteEndPoint);
                        };

                        // Leave Resp
                        if (packetRecv.cmd == 2)
                        {
                            byte[] leaveResp = ClientLeave();
                            await udpServer.SendAsync(leaveResp, res.RemoteEndPoint);
                        };

                        // Move Resp
                        if (packetRecv.cmd == 3)
                        {
                            byte[] moveResp = ClientMove();
                            await udpServer.SendAsync(moveResp, res.RemoteEndPoint);
                        };

                        // Place Resp
                        if (packetRecv.cmd == 4)
                        {
                            byte[] placeResp = ClientPlace();
                            await udpServer.SendAsync(placeResp, res.RemoteEndPoint);
                        };
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

        public void IncomingData(IAsyncResult res)
        {
            messageRec = true;
        }

        public byte[] ClientJoin(IPEndPoint ep)
        {
            if (!conns.ContainsKey(ep.Address))
            {
                conns.Add(ep.Address, conns.Count() + 1);
                clientId = conns.GetValueOrDefault(ep.Address);
            }

            return packetSent.ServerSendPacket("Join", $"{clientId}:{conns.Count()}");
        }

        public byte[] ClientLeave()
        {
            if (conns.ContainsKey(remoteEndpoint.Address))
            {
                conns.Remove(remoteEndpoint.Address);
            }

            return packetSent.ServerSendPacket("Leave", $"{clientId}:{conns.Count()}");
        }

        public byte[] ClientMove()
        {
            return packetSent.ServerSendPacket("Move", "Move Response");
        }

        public byte[] ClientPlace()
        {
            return packetSent.ServerSendPacket("Place", "Place Response");
        }
    }
}
