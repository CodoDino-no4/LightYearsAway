using Server;
using System.Net;

namespace Client
{
    public static class Program
    {
        public static IPAddress? ipAddress { get; private set; }
        public static int port { get; private set; }

        private static void Main(string[] args)
        {
            var client = new BasicClient();
            var packet = new Packet(1, "1234", "Data innit client");

            client.Init(IPAddress.Loopback, BasicServer.PORT);
            client.StartLoop(packet.GetDataStream());

            Console.ReadLine();
        }
    }
}
