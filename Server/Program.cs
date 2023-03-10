using Server.Networking;

namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {
            var server = new BasicServer();

            server.Init();
            Packet packet = new Packet("Join");
            byte[] data = packet.MakeBytes();
            server.MessageLoop(data);

            Console.ReadLine();
        }
    }
}
