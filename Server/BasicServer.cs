using Server.Commands;
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

        private int clientId = 0;

        private ServerPacket packetSent;
        private ServerPacket packetRecv;

        public bool messageRec = false;

        // Tick rate
        private Timer tickTimer;
        private TimeSpan deltaTime;

        public void Init()
        {
            sendBuff = new byte[512];
            recvBuff = new byte[512];

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

        public void StartLoop()
        {
            _ = Task.Factory.StartNew(async () =>
            {
                try
                {
                    tickTimer = new Timer();
                    tickTimer.Interval = deltaTime.TotalMilliseconds;
                    tickTimer.Enabled = true;
                    tickTimer.Elapsed += async (object source, ElapsedEventArgs e) =>
                    { 
                        // Recieve packet and data
                        var res = await udpServer.ReceiveAsync();
                        recvBuff = res.Buffer;
                        packetRecv.ServerRecvPacket(recvBuff);

                        // Handle command and send a response
                        ResponseCommand resCommand = new ResponseCommand(res);
                        resCommand.Execute();
                        await sendToAll(resCommand.data, resCommand.conns);
                    };
                }
                catch (SocketException e)
                {
                    if (e.SocketErrorCode.ToString() == "ConnectionReset")
                    {
                        Debug.WriteLine("The client is unreachable");
                    }
                }
            });
        }

        public async Task sendToAll(byte[] data, Dictionary<IPEndPoint, int> conns)
        {
            foreach (var client in conns)
            {
                _ = await udpServer.SendAsync(data, client.Key);        
            }
        }
    }
}
