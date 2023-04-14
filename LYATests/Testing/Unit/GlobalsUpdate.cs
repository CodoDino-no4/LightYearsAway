using LYA._Camera;
using LYA.Helpers;
using LYA.Sprites;
using LYA.Sprites.Background;
using LYA.Sprites.Cloneables;
using LYA.Sprites.GUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LYA.Testing.Unit
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass()]
    public class GlobalsUpdate
    {
        /// <summary>
        /// Initialises the game and runs one frame
        /// </summary>
        LYA game; 

		[TestInitialize()]
        public void Setup()
        {
            Globals.testing = true;
            Globals.IsMulti = false;

            game = new LYA();
            game.RunOneFrame();
        }

        [TestMethod()]
        public void GlobalsTest()
        {
           
        }
    }
}
