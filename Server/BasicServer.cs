using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Server
{
    public class BasicServer
    {
        public const int PORT = 11000;

        private UdpClient udpServer;

        private UdpClient remoteClient;
        private IPEndPoint remoteEndpoint;

        private byte[] sendBuff;
        private byte[] recvBuff;

        private Dictionary<IPEndPoint, int> conns;
        private int clientId = 0;

        private ServerPacket packetSent;
        private ServerPacket packetRecv;

        public bool messageRec = false;

        // Tick rate
        private Timer tickTimer;
        private TimeSpan deltaTime;

        string path = "C:/Users/Alz/Documents/log.txt";

        public void Init()
        {
            sendBuff = new byte[512];
            recvBuff = new byte[512];

            conns = new Dictionary<IPEndPoint, int>();

            udpServer = new UdpClient(PORT);
            udpServer.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpServer.EnableBroadcast = true;
            udpServer.DontFragment = true;

            remoteEndpoint = new IPEndPoint(IPAddress.Any, PORT);

            packetSent = new ServerPacket();
            packetRecv = new ServerPacket();

            deltaTime = TimeSpan.FromMilliseconds(1000.0f / 60);

            Console.WriteLine("Server successfully intialised...");
        }

        public async void StartLoop()
        {
            tickTimer = new Timer();
            tickTimer.Interval = deltaTime.TotalMilliseconds;
            tickTimer.Enabled = true;
            tickTimer.Elapsed += async (object source, ElapsedEventArgs e) =>
            {
                try
                {
                    var res = await udpServer.ReceiveAsync();
                    recvBuff = res.Buffer;
                    packetRecv.ServerRecvPacket(recvBuff);

                    //Join Resp
                    if (packetRecv.cmd == 1) // for second player either unreachable or no response given
                    {
                        byte[] joinResp = ClientJoin(res.RemoteEndPoint);
                        await sendToAll(joinResp);
                    };

                    // Leave Resp
                    if (packetRecv.cmd == 2)
                    {
                        byte[] leaveResp = ClientLeave(res.RemoteEndPoint);
                        await sendToAll(leaveResp);
                    };

                    // Move Resp
                    if (packetRecv.cmd == 3)
                    {
                        byte[] moveResp = ClientMove();
                        await sendToAll(moveResp);
                    };

                    // Place Resp
                    if (packetRecv.cmd == 4)
                    {
                        byte[] placeResp = ClientPlace();
                        await sendToAll(placeResp);
                    };

                } catch (SocketException ex)
                {
                    if (!File.Exists(path))
                    {
                        FileStream fs = File.Create(path);
                    }

                    File.AppendAllText(path, ex.Message);

                    Console.Write("Press Enter to close window ...");
                    Console.Read();

                    if (ex.SocketErrorCode.ToString() == "ConnectionReset")
                    {
                        Debug.WriteLine("The client is unreachable");
                    }
                }
            };       
        }

        public async Task sendToAll(byte[] data)
        {
            foreach (var client in conns)
            {
                _ = await udpServer.SendAsync(data, client.Key);        
            }
        }

        public byte[] ClientJoin(IPEndPoint ep)
        {
            if (!conns.ContainsKey(ep))
            {
                conns.Add(ep, conns.Count() + 1);
                clientId = conns.GetValueOrDefault(ep);
            }

            return packetSent.ServerSendPacket("Join", clientId, conns.Count().ToString());
        }

        public byte[] ClientLeave(IPEndPoint ep)
        {
            if (conns.ContainsKey(ep))
            {
                conns.Remove(ep);
            }

            return packetSent.ServerSendPacket("Leave", packetRecv.clientId, conns.Count().ToString());
        }

        public byte[] ClientMove()
        {
            return packetSent.ServerSendPacket("Move", packetRecv.clientId, packetRecv.payload);
        }

        public byte[] ClientPlace()
        {
            return packetSent.ServerSendPacket("Place", packetRecv.clientId, packetRecv.payload);
        }
    }
}
