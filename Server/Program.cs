using LYA;

namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {
            var server = new BasicServer();
            var packet = new LYA.Packet(3, "1234", "Data innit server");

            server.Init();
            server.StartLoop(packet.GetDataStream());

            Console.ReadLine();
        }
    }
}
