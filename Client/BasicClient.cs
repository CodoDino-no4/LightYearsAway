using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class BasicClient
    {
        private Socket udpClient;
        private IPEndPoint serverEndPoint; 
        private byte[] buffer;
        private ArraySegment<byte> bufferSegment; 

        public void Init(IPAddress ip, int port)
        {
            buffer = new byte[4096];
            bufferSegment = new ArraySegment<byte>(buffer);

            serverEndPoint = new IPEndPoint(ip, port);

            udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpClient.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.PacketInformation, true);

        }


        public void StartLoop(byte[] data)
        {
            _ = Task.Factory.StartNew(async () =>
            {
                SocketReceiveMessageFromResult res;

                while (true)
                {
                    await Send(data);
                    res = await udpClient.ReceiveMessageFromAsync(bufferSegment, SocketFlags.None, serverEndPoint);

                    Console.WriteLine(res);
                }
            });
        }

        public async Task Send(byte[] data)
        {
            var s = new ArraySegment<byte>(data);
            await udpClient.SendToAsync(s, SocketFlags.None, serverEndPoint);
        }
    }
}
