namespace Server
{
    class Program
    {
        static ManualResetEvent _quitEvent = new ManualResetEvent(false);
        static string path = "C:/Users/Alz/Documents/log.txt";
        public static void Main()
        {
            try
            {
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                }

                Console.CancelKeyPress += (sender, eArgs) =>
                {
                    _quitEvent.Set();
                    eArgs.Cancel = true;
                };

                var server = new BasicServer();

                server.Init();
                server.StartLoop();

                _quitEvent.WaitOne();
                Console.Write("Press Enter to close window ...");
                Console.Read();
            }
            catch (Exception e)
            {
                Console.Write("Press Enter to close window ...");
                Console.Read();

                File.AppendAllText(path, e.InnerException.Message);
                File.AppendAllText(path, e.Message);
                File.AppendAllText(path, e.ToString());
            }
            finally
            {
                Console.Write("Press Enter to close window ...");
                Console.Read();
            }
        }
    }
}
