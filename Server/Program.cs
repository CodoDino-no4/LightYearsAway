namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {

            var server = new BasicServer();

            server.Init();
            Packet sendPacket = new Packet("Move");
            server.StartLoop(sendPacket.MakeBytes());
            Packet recvPacket = new Packet();
            server.RecievedBytes();
            Console.WriteLine("Server Listening...");

            Console.ReadLine();
        }
    }
}
