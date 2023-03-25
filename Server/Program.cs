namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {

            var server = new BasicServer();

            server.Init();
            server.StartLoop();
            Packet recvPacket = new Packet();
            server.RecievedBytes();
            Console.WriteLine("Server Listening...");

            Console.ReadLine();
        }
    }
}
