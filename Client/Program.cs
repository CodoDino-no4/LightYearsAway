using LYA;
using Microsoft.Xna.Framework;
using Server;
using System.Net;

namespace Client
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var client = new BasicClient();
            var packet = new LYA.Packet(1, "1234", "Data innit client");
            string serverIP;
            string serverPort;
            try
            {
                Console.WriteLine("Enter the server IP Address: "); // 169.254.162.206 4000
                serverIP = Console.ReadLine();
                Console.WriteLine("Enter the server Port: ");
                serverPort = Console.ReadLine();

                client.Init(IPAddress.Parse(serverIP), Int32.Parse(serverPort));
            }
            catch (Exception)
            {
                Console.WriteLine("An invalid IP Address was entered");
            }

            client.StartLoop(packet.GetDataStream());

            using (var game = new LYA.LYA())
                game.Run();

        }
    }
}
