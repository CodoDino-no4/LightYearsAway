﻿namespace LYAServer
{
    public class Program
    {
        private static ManualResetEvent _quitEvent = new ManualResetEvent(false);
        private static string port;

        /// <summary>
        /// Runs the program
        /// </summary>
        public static void Main(string[] args)
        {
            try
            {
                // Keep console running until a cancel event occurs
                Console.CancelKeyPress += (sender, eArgs) =>
                {
                    _quitEvent.Set();
                    eArgs.Cancel = true;
                };

                var server = new BasicServer();

                if (!args.Contains("Integrated"))
                {
                    Console.WriteLine("Enter the port the server should run on:");
                    port = Console.ReadLine();
                }
                else
                {
                    port = args[0];
                }

                server.Init(port);
                server.StartLoop();

                _quitEvent.WaitOne();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.Write("Press Enter to close window ...");
            }
        }
    }
}
