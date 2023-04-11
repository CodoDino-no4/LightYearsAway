﻿using System.Diagnostics;
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
        private DateTime currentTime;
        private bool onInterval;

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
            currentTime = DateTime.Now;
            onInterval = false;

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
                        var res = await udpServer.ReceiveAsync();
                        recvBuff = res.Buffer;
                        packetRecv.ServerRecvPacket(recvBuff);

                        //Join Resp
                        if (packetRecv.cmd == 1) // for second player either unreachable or no response given
                        {
                            byte[] joinResp = ClientJoin(res.RemoteEndPoint);
                            await udpServer.SendAsync(joinResp, res.RemoteEndPoint);
                        };

                        // Leave Resp
                        if (packetRecv.cmd == 2)
                        {
                            byte[] leaveResp = ClientLeave(res.RemoteEndPoint);
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
                    };
                }
                catch (SocketException err)
                {
                    Console.WriteLine(err);
                }
            });

        }

        public void IncomingData(IAsyncResult res)
        {
            messageRec = true;
        }

        public byte[] ClientJoin(IPEndPoint ep)
        {
            if (!conns.ContainsKey(ep))
            {
                conns.Add(ep, conns.Count() + 1);
                clientId = conns.GetValueOrDefault(ep);
            }

            return packetSent.ServerSendPacket("Join", clientId, $"{conns.Count()}");
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
