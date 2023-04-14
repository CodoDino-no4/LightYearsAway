using LYA._Camera;
using LYA.Helpers;
using LYA.Networking;
using LYA.Sprites;
using LYA.Sprites.Background;
using LYA.Sprites.Cloneables;
using LYA.Sprites.GUI;
using Server;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        LYA game; 

		[TestInitialize()]
        public void Setup()
        {
            Globals.testing = true;

            game = new LYA();
            game.SuppressDraw();
            game.RunOneFrame();

            //Server.Program.Main();
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
            Assert.IsTrue(Globals.IsMulti == false);
        }

        [TestMethod()]
        public void MultiplayerExitTest()
        {
            Globals.IsMulti = true;
            game.clientManager.Init(IPAddress.Parse("192.168.1.101"), Int32.Parse("11000"));
            game.clientManager.LeaveServer();

            var data = Globals.Packet.sendData;
            if (data != null)
            { 
                Assert.IsTrue(Encoding.UTF8.GetString(data) == "\u0002\0\0\0\u0001\0\0\0");
            }
        }

        [TestMethod()]
        public void SinglePlayerExitTest()
        {
            Globals.IsMulti = false;
            game.Exit();
        }
    }
}
