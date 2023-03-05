using Server;
using System.Net;

namespace Client
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var client = new BasicClient();
            var packet = new Packet(1, "1234", "Data innit client");
            string serverIP;
            string serverPort;

            try
            {
                Console.WriteLine("Enter the server IP Address: ");
                serverIP = Console.ReadLine();
                Console.WriteLine("Enter the server Port: ");
                serverPort = Console.ReadLine();

                client.Init(IPAddress.Parse(serverIP), Int32.Parse(serverPort));
            }
            catch (Exception e)
            {
                Console.WriteLine("An invalid IP Address was entered");
            }


            //"169.254.162.206" 4000

            client.StartLoop(packet.GetDataStream());

            Console.ReadLine();
        }
    }
}
