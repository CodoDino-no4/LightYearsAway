using System.Net;

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
                        //Console.WriteLine("Enter the server IP Address: ");
                        //serverIP = Console.ReadLine();
                        //Console.WriteLine("Enter the server Port: ");
                        //serverPort = Console.ReadLine();

                        client.Init(IPAddress.Parse("192.168.1.255"), Int32.Parse("11000"));

                        isValid = true;

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An invalid IP Address was entered. Try Again...");
                    }
                }

                //if the server is unreachable then try again 3 times and finally fail
                while (!connected)
                {
                    int i = 0;
                    i++;

                    if (i > 3)
                    {
                        Console.WriteLine("Could not connect to server. Try again? (Y/N): ");
                        break;
                    }

                    client.StartLoop();

                    Console.WriteLine("Client Started");

                    connected = true;
                }

            }

            Console.ReadLine();

            //using (var game = new LYA.LYA())
            //    game.Run();
        }
    }
}
