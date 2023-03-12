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
        private ArraySegment<byte> bufferSegment;
        private bool disposedValue;
        private ArrayList conns;

        public void Init()
        {
            Console.WriteLine("Server intialisaing...");

            buffer = new byte[4096];
            bufferSegment = new ArraySegment<byte>(buffer);
            conns = new ArrayList();

            udpServer = new UdpClient(PORT);
            remoteEndpoint = new IPEndPoint(IPAddress.Any, PORT);

            Console.WriteLine("Server successfully intialised at port :" + PORT);

        }

        public void StartLoop(byte[] data)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast");
                    bufferSegment = udpServer.Receive(ref remoteEndpoint);

                    Console.WriteLine($"Received broadcast from {remoteEndpoint} :");
                    Console.WriteLine(Encoding.UTF8.GetString(bufferSegment));
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
        }



        //_ = Task.Run(async () =>
        //    {
        //        SocketReceiveMessageFromResult res;

        //        while (true)
        //        {
        //            res = await udpServer.ReceiveMessageFromAsync(bufferSegment, SocketFlags.None, remoteEndpoint);
        //            await SendTo(res.RemoteEndPoint, data);

        //            Console.WriteLine(res);

        //        }
        //    });

        //}

        //public async Task SendTo(EndPoint recipient, byte[] data)
        //{
        //    var s = new ArraySegment<byte>(data);
        //    await udpServer.SendToAsync(s, SocketFlags.None, recipient);
        //}
    }
}
