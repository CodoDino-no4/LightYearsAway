namespace Server
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            using var server = new BasicServer();
            server.Start();
        }
    }
}
