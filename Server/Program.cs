namespace Server
{
    class Program
    {
        private static void Main(string[] args)
        {
            var server = new BasicServer();

            server.Init();
            server.StartLoop();

            Console.ReadLine();
        }
    }
}
