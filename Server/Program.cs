namespace Server
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            //client will load up 
            //connect to server button
            //user enters info
            //tries to connect

            using var client = new Client();
            client.Connect("127.0.0.1", 3000);
            //client.Start();
        }
    }
}
