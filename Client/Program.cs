using System.Net;
using Client.Networking;

namespace Client
{
    public static class Program
    {
            static bool isMultiplayer = false;
            static bool isValid = false;
            static bool connected = false;

        private static void Main(string[] args)
        {
            var client = new BasicClient();

            string serverIP;
            string serverPort;
            string ans;

            // Server connection detials
            Console.WriteLine("Enable multiplayer? (Y/N): ");
            ans = Console.ReadLine().ToLower();

            if (ans == "y" || ans == "yes")
            {
                isMultiplayer = true;

                while (!isValid)
                {
                    try
                    {

                        Console.WriteLine("Enter the server IP Address: "); // 169.254.162.206 4000
                        serverIP = Console.ReadLine();
                        Console.WriteLine("Enter the server Port: ");
                        serverPort = Console.ReadLine();

                        client.Init(IPAddress.Parse(serverIP), Int32.Parse(serverPort));

                        isValid = true;

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An invalid IP Address was entered. Try Again...");
                    }
                }

                while (!connected)
                {
                    int i = 0;
                    i++;

                    if (i > 3)
                    {
                        Console.WriteLine("Could not connect to server. Try again? (Y/N): ");
                        break;
                    }

                    Packet packet = new Packet("Join");
                    byte[] data = packet.MakeBytes();

                    client.JoinServer(data);

                    connected = true;
                }

            }

            using (var game = new LYA.LYA())
                game.Run();
        }
    }
}
