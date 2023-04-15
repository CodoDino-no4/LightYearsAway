using LYA.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Myra.Graphics2D.UI;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace LYA.Testing.Unit
{
    /// <summary>
    /// Check the game has initialised and Globals have been set.
    /// Check the game exits gracefully
    /// </summary>
    [TestClass()]
    public class Game
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
            game.SuppressDraw();
            game.RunOneFrame();

            // Setup server exe
            ProcessStartInfo start = new ProcessStartInfo();

            string fullPath = AppContext.BaseDirectory + "Server.exe";
            start.FileName = fullPath;

            proc = Process.Start(start);
            Thread.Sleep(1000);
        }

        [TestMethod()]
        public void GlobalsTest()
        {
            Assert.IsTrue(game.screenManager == Globals.ScreenManager);
            Assert.IsTrue(game.spriteBatch == Globals.SpriteBatch);
            Assert.IsTrue(game.Content == Globals.Content);
            Assert.IsTrue(game.GraphicsDevice.Viewport.Bounds == Globals.ScreenSize);
            Assert.IsTrue(Globals.PlayerCount == 1);
            Assert.IsTrue(Globals.MaxPlayers == 8);
        }

        [TestMethod()]
        public void MultiplayerConnectTest()
        {
            Globals.IsMulti = true;

            game.clientManager.Init(IPAddress.Parse(IP), Int32.Parse(PORT));
            Assert.IsNotNull(Encoding.UTF8.GetString(game.clientManager.packetJoin.sendData));
        }

        [TestMethod()]
        public void MultiplayerExitTest()
        {
            Globals.IsMulti = true;

            game.clientManager.Init(IPAddress.Parse(IP), Int32.Parse(PORT));
            game.clientManager.LeaveServer();

            Assert.IsTrue(Encoding.UTF8.GetString(game.clientManager.packetLeave.sendData) == "\u0002\0\0\0\u0001\0\0\0");
        }

        [TestMethod()]
        public void SinglePlayerExitTest()
        {
            Globals.IsMulti = false;
            game.Exit();
            game.Dispose();

            Assert.IsTrue(game.Components == null);
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
