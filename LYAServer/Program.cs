namespace LYAServer
{
    public class Program
    {
        private static ManualResetEvent _quitEvent = new ManualResetEvent(false);
        public static void Main()
        {
            try
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
            catch (Exception e)
            {
                Console.Write(e);
                Console.Write("Press Enter to close window ...");
                Console.Read();
            }
        }
    }
}
