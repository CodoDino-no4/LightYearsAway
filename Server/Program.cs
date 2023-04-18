namespace Server
{
    public class Program
    {
        private static ManualResetEvent _quitEvent = new ManualResetEvent(false);
        public static void Main()
        {
            Console.CancelKeyPress += (sender, eArgs) =>
            {
                _quitEvent.Set();
                eArgs.Cancel = true;
            };

            var server = new BasicServer();

            server.Init();
            server.StartLoop();

            _quitEvent.WaitOne();
        }
    }
}
