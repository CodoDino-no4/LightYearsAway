using LYA.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace LYA.Testing.Integration
{
    /// <summary>
    /// Check the game has initialised and Globals have been set.
    /// Check the game exits gracefully
    /// </summary>
    [DoNotParallelize()]
    [TestClass()]
    public class Movement
    {
        /// <summary>
        /// Initialises the game and runs one frame
        /// </summary>
        private LYA game;
        private Process proc;
        private string IP = "192.168.1.101";
        private string PORT = "11000";

        [TestInitialize()]
        public void Setup()
        {
            Globals.testing = true;

            game = new LYA();

            _ = Task.Run(() =>
            {
                // Setup server exe
                ProcessStartInfo start = new ProcessStartInfo();

                string fullPath = AppContext.BaseDirectory + "Server.exe";
                start.FileName = fullPath;

                proc = Process.Start(start);

            });

            _ = Task.Run(() =>
            { 
                game.SuppressDraw();
                game.Run();
            });

        }

        [TestMethod()]
        public void Inital()
        {
            game.clientManager.Init(IPAddress.Parse(IP), Int32.Parse(PORT));
            Thread.Sleep(500000);
        }

        [TestCleanup()]
        public void CleanUp()
        {
            game.Exit();
            game = null;

            proc.Kill();
            proc.WaitForExit();
            proc.Dispose();

            proc = null;

        }
    }
}
