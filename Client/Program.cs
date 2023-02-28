using Server;
using System.Net;
using System.Text;

namespace Client
{
    public static class Program
    {
        public static IPAddress ipAddress { get; private set; }
        public static int port { get; private set; }

        private static async Task Main(string[] args)
        {

            var client = new BasicClient();

            client.Init(IPAddress.Loopback, BasicServer.PORT);
            client.StartLoop();

            Console.WriteLine("Client Started");

            await client.Send(Encoding.UTF8.GetBytes("HIYA from client"));

            Console.ReadLine();
        }
    }
}
