using Server.Commands;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Server
{
    /// <summary>
    /// Server class to initalise and start a data transmission loop
    /// </summary>
    public class BasicServer
    {
        // Socket setup
        private UdpClient udpServer;
        private const int PORT = 11000;

        // Game world data
        private List<Vector2> tiles;
        private List<ClientInfo> clients;

        // Packets to store data
        private ServerPacket packetSent;
        private ServerPacket packetRecv;
        private byte[] recvBuff;

        // Check error
        public bool hasErrored;

        // Tick rate
        private Timer tickTimer;
        private TimeSpan deltaTime;

        // Response Command
        private Response response;

        // Logging REMOVE BEFORE COMPLETION
        private string path = "C:/Users/Alz/Documents/log.txt";

        /// <summary>
        /// Initalise the server
        /// </summary>
        public void Init()
        {
            recvBuff = new byte[128];

            clients = new List<ClientInfo>();
            tiles = new List<Vector2>();

            udpServer = new UdpClient(PORT);
            udpServer.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpServer.EnableBroadcast = true;
            udpServer.DontFragment = true;

            packetSent = new ServerPacket();
            packetRecv = new ServerPacket();

            deltaTime = TimeSpan.FromMilliseconds(1000.0f / 60);

            Console.WriteLine("Server successfully intialised...");
        }

        /// <summary>
        /// Start the data transmission loop
        /// </summary>
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
                        var res = await udpServer.ReceiveAsync();

                        response = new Response(res, clients);
                        response.Execute();

                        switch (response.packetSend.cmd)
                        {
                            // Null
                            default:
                            case 0:
                                break;

                            // Join
                            case 1:
                                await sendToAll(response.data);
                                break;

                            // Leave, Move, Place
                            case 2:
                            case 3:
                            case 4:
                                await sendToAllExceptSender(response.data, response.remoteEp);
                                break;

                            // Error
                            case 5:
                                await sendToSender(response.data, response.remoteEp);
                                break;
                        }
                    }
                    catch (SocketException ex)
                    {
                        if (ex.SocketErrorCode.ToString() == "ConnectionReset")
                        {
                            Debug.WriteLine("The client is unreachable");
                        }

                        if (!File.Exists(path))
                        {
                            FileStream fs = File.Create(path);
                        }

                        File.AppendAllText(path, ex.Message);
                        Console.WriteLine("added to file...");

                        Console.Write("Press Enter to close window ...");
                        Console.Read();

                    }
                };
            });
        }

        /// <summary>
        /// Send data to all clients
        /// </summary>
        public async Task sendToAll(byte[] data)
        {
            foreach (var client in clients)
            {
                _ = await udpServer.SendAsync(data, client.ep);
            }
        }

        /// <summary>
        /// Send data to all clients except the sender client
        /// </summary>
        public async Task sendToAllExceptSender(byte[] data, IPEndPoint sender)
        {
            foreach (var client in clients)
            {
                if (sender != client.ep)
                {
                    _ = await udpServer.SendAsync(data, client.ep);
                }
            }
        }

        /// <summary>
        /// Send data to only the sender client
        /// </summary>
        public async Task sendToSender(byte[] data, IPEndPoint ep)
        {
            _ = await udpServer.SendAsync(data, ep);
        }
    }
}
