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

        private List<Vector2> tiles;
        private List<ClientInfo> clients;

        private ServerPacket packetSent;
        private ServerPacket packetRecv;

        public bool messageRec = false;
        public bool hasErrored;

        // Tick rate
        private Timer tickTimer;
        private TimeSpan deltaTime;
        private string path = "C:/Users/Alz/Documents/log.txt";

        public void Init()
        {
            sendBuff = new byte[128];
            recvBuff = new byte[128];

            clients = new List<ClientInfo>();
            tiles = new List<Vector2>();

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

            await Task.Run(() =>
            {
                tickTimer.Elapsed += async (object source, ElapsedEventArgs e) =>
                {
                    try
                    {
                        hasErrored = false;
                        var res = await udpServer.ReceiveAsync();
                        recvBuff = res.Buffer;
                        packetRecv.ServerRecvPacket(recvBuff);

                        //Join Resp
                        if (packetRecv.cmd == 1)
                        {
                            byte[] joinResp = ClientJoin(res.RemoteEndPoint);

                            if (packetSent.cmd != 5)
                            {
                                await sendToAll(joinResp);
                            }
                            else { 
                                await sendToOne(joinResp, res.RemoteEndPoint);
                            }
                        };

                        // Leave Resp
                        if (packetRecv.cmd == 2)
                        {
                            byte[] leaveResp = ClientLeave(res.RemoteEndPoint);

                            if (!hasErrored)
                            {
                                await sendToAll(leaveResp);
                            }
                            else
                            {
                                await sendToOne(leaveResp, res.RemoteEndPoint);
                            }
                        };

                        // Move Resp
                        if (packetRecv.cmd == 3)
                        {
                            byte[] moveResp = ClientMove(res.RemoteEndPoint);

                            if (!hasErrored)
                            {
                                await sendToAll(moveResp);
                            }
                            else
                            {
                                await sendToOne(moveResp, res.RemoteEndPoint);
                            }
                        };

                        // Place Resp
                        if (packetRecv.cmd == 4)
                        {
                            byte[] placeResp = ClientPlace(res.RemoteEndPoint);

                            if (!hasErrored)
                            {
                                await sendToAll(placeResp);
                            }
                            else
                            {
                                await sendToOne(placeResp, res.RemoteEndPoint);
                            }
                        };

                        // Error Resp
                        if (packetRecv.cmd == 5)
                        {
                            ClientError(res.RemoteEndPoint);
                        };

                    }
                    catch (SocketException ex)
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
            });
        }

        public async Task sendToAll(byte[] data)
        {
            foreach (var client in clients)
            {
                _ = await udpServer.SendAsync(data, client.ep);
            }
        }

        public async Task sendToOne(byte[] data, IPEndPoint ep)
        {
           _ = await udpServer.SendAsync(data, ep);
        }

        public byte[] ClientJoin(IPEndPoint ep)
        {
            if (clients.Count < 4)
            {
                var client = clients.Find(c => c.ep.Equals(ep));
                var clientId = 0;

                if (client == null)
                {
                    clientId = clients.Count() + 1;
                    clients.Add(new ClientInfo(ep, clientId));
                    var serverData = $"{clients.Count()}?";

                    for (var i = 1; clients.Count()>i; i++)
                    {
                        var conn = clients.Find(c => c.id.Equals(i));
                        if (conn != null)
                        {
                            serverData += $"client{i}:{conn.position.X}:{conn.position.Y}";
   
                        }
                    }

                    return packetSent.ServerSendPacket("Join", clientId, 0, 0, serverData);
                }
                else
                {
                    hasErrored = true;
                    return packetSent.ServerSendPacket("Error", clientId, 0, 0, "Client is already connected on this IP address and port");
                }
            } else {

                hasErrored = true;
                return packetSent.ServerSendPacket("Error", 0, 0, 0, "Server full");
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

                return packetSent.ServerSendPacket("Leave", clientId, 0, 0, clients.Count().ToString());
            }
            else
            {
                hasErrored = true;
                return packetSent.ServerSendPacket("Error", clientId, 0, 0, "Error on leaving server");
            }
        }

        public byte[] ClientMove(IPEndPoint ep)
        {
            var client = clients.Find(c => c.ep.Equals(ep));
            var clientId = 0;

            if (client != null)
            {
                clientId = client.id;
                client.position = new Vector2(packetRecv.posX, packetRecv.posY);

                return packetSent.ServerSendPacket("Move", clientId, packetRecv.posX, packetRecv.posY, "");
            }
            else
            {
                hasErrored = true;
                return packetSent.ServerSendPacket("Error", clientId, 0, 0, "Error on move");
            }

        }

        public byte[] ClientPlace(IPEndPoint ep)
        {
            var client = clients.Find(c => c.ep.Equals(ep));
            var clientId = 0;
            var position = new Vector2(packetRecv.posX, packetRecv.posY);

            if (client != null)
            {
                if (!tiles.Contains(position))
                {
                    clientId = client.id;
                    tiles.Add(position);
                    return packetSent.ServerSendPacket("Place", clientId, (int)position.X, (int)position.Y, "");
                }
                else
                {
                    hasErrored = true;
                    return packetSent.ServerSendPacket("Error", clientId, 0, 0, "Tile already placed here");
                }
            }
            else
            {
                hasErrored = true;
                return packetSent.ServerSendPacket("Error", clientId, 0, 0, "Error on place");
            }

        }

        public void ClientError(IPEndPoint ep)
        {
            var client = clients.Find(c => c.ep.Equals(ep));
            var clientId = 0;
            Console.WriteLine($"Client ID: {client.id}, ERROR: {packetRecv.payload}");
        }
    }
}
