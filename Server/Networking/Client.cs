using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class Client
    {
        public const int PORT = 5000;

        private byte[] dataRecv;
        private ArraySegment<byte> dataRecvSegment;
        private UdpClient udpClient;
        private IPEndPoint endPoint;

        public Client()
        {
            dataRecv = new byte[4096];
            dataRecvSegment = new ArraySegment<byte>(dataRecv);
            udpClient = new UdpClient();
        }


        public void Send()
        {
            Console.WriteLine("Start");

            try
            {

                // IP and PORT
                IPAddress serverIP = IPAddress.Parse("127.0.0.1"); //localhost
                endPoint = new IPEndPoint(serverIP, PORT);

                udpClient.Connect(endPoint);

                string data = "the packet has been sent oh yea client";

                dataRecv = Encoding.ASCII.GetBytes(data);

                udpClient.Send(dataRecv);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("Sent");
        }

    }
}
