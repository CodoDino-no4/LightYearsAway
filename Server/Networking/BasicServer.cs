﻿using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class BasicServer : IDisposable
    {

        public const int PORT = 5000;

        private Socket udpServer;
        private EndPoint endPoint;

        private byte[] dataRecv;
        private ArraySegment<byte> dataRecvSegment;
        private bool disposedValue;
        private ArrayList conns;

        public BasicServer()
        {
            dataRecv = new byte[4096];
            dataRecvSegment = new ArraySegment<byte>(dataRecv);

            endPoint = new IPEndPoint(IPAddress.Any, PORT);

            udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpServer.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);
            udpServer.Bind(endPoint);

            Console.WriteLine("Server connected on port: " + PORT);

            conns = new ArrayList();

        }

        public void Start()
        {
            Console.WriteLine("Start");
            _=Task.Run(async () =>
            {
                SocketReceiveMessageFromResult res;

                Console.WriteLine("Start Task.Run");
                while (true)
                {
                    res = await udpServer.ReceiveMessageFromAsync(dataRecvSegment, SocketFlags.None, endPoint);
                    await SendTo(res.RemoteEndPoint, Encoding.UTF8.GetBytes("Hello back!"));

                    Console.WriteLine("Start Task.Run while(true)");

                }
            });
        }

        public async Task SendTo(EndPoint recipient, byte[] data)
        {
            var segment = new ArraySegment<byte>(data);
            await udpServer.SendToAsync(segment, SocketFlags.None, recipient);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~BasicServer()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
