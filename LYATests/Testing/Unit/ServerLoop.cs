using LYA._Camera;
using LYA.Helpers;
using LYA.Networking;
using LYA.Sprites;
using LYA.Sprites.Background;
using LYA.Sprites.Cloneables;
using LYA.Sprites.GUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace LYA.Testing.Unit
{
    /// <summary>
    /// Checks the server can initialise run indefinitely
    /// </summary>
    [TestClass()]
    public class ServerLoop
    {
        /// <summary>
        /// Initialises the game and runs one frame
        /// </summary>
        LYA game;

        [TestInitialize()]
        public void Setup()
        {
        }

        [TestMethod()]
        public void ServerMessageLoop()
        {

        }
    }
}
