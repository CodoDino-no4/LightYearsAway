using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
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

        private List<string> tiles;
        private List<ClientInfo> clients;

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

            clients = new List<ClientInfo>();
            tiles = new List<string>();

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
                        byte[] moveResp = ClientMove(res.RemoteEndPoint);
                        await sendToAll(moveResp);
                    };

                    // Place Resp
                    if (packetRecv.cmd == 4)
                    {
                        byte[] placeResp = ClientPlace(res.RemoteEndPoint);
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

        public string Decode(ServerPacket packet)
        {
            string position = "";

            if (packet.cmd == 3 || packet.cmd == 4)
            {
                if (packet.payload != null)
                {
                    string remCurlys = packet.payload.Substring(1, packet.payload.Length - 2);
                    string xPair = remCurlys.Split(' ').First();
                    string yPair = remCurlys.Split(' ').Last();

                    string xValue = xPair.Split(":").Last();
                    string yValue = yPair.Split(":").Last();

                    int x = Int32.Parse(xValue);
                    int y = Int32.Parse(yValue);

                    position = "{X:" + x + " Y:" + y + "}";
                }
            }
            return position;
        }

        public async Task sendToAll(byte[] data)
        {
            foreach (var client in clients)
            {
                _ = await udpServer.SendAsync(data, client.ep);
            }
        }

        public byte[] ClientJoin(IPEndPoint ep)
        {
            var client = clients.Find(c => c.ep.Equals(ep));
            var clientId = 0;

            if (client == null)
            {
                clientId = clients.Count() + 1;
                clients.Add(new ClientInfo(ep, clientId));

                return packetSent.ServerSendPacket("Join", clientId, clients.Count().ToString());
            }
            else {

                return packetSent.ServerSendPacket("Error", clientId, "Error on joining server");
            }

        }

        public byte[] ClientLeave(IPEndPoint ep)
        {
            var client = clients.Find(c => c.ep.Equals(ep));
            var clientId = 0;

            if (client != null)
            {
                clientId = client.id;
                clients.Remove(client);

                return packetSent.ServerSendPacket("Leave", clientId, clients.Count().ToString());
            }
            else {

                return packetSent.ServerSendPacket("Error", clientId, "Error on leaving server");
            }
        }

        public byte[] ClientMove(IPEndPoint ep)
        {
            ClientInfo client = clients.Find(c => c.ep.Equals(ep));
            var clientId = 0;

            if (client != null)
            {
                clientId = client.id;
                var position = Decode(packetRecv);
                client.position = position;

                return packetSent.ServerSendPacket("Move", clientId, position);
            }
            else {

                return packetSent.ServerSendPacket("Error", clientId, "Error on move");
            }

        }

        public byte[] ClientPlace(IPEndPoint ep)
        {
            var client = clients.Find(c => c.ep.Equals(ep));
            var clientId = 0;

            var position = Decode(packetRecv);

            if (client != null)
            {
                if (!tiles.Contains(position))
                {
                    clientId = client.id;
                    tiles.Add(position);
                    return packetSent.ServerSendPacket("Place", clientId, position);
                }
                else
                {
                    return packetSent.ServerSendPacket("Error", clientId, "Tile already placed here");
                }
            }
            else {

                return packetSent.ServerSendPacket("Error", clientId, "Error on place");
            }

        }
    }
}
