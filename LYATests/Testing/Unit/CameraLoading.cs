﻿using LYA._Camera;
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
    /// Camera can be loaded with the correct initalisation values
    /// </summary>
    [TestClass()]
    public class CameraLoading
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
        }

        [TestMethod()]
        public void CameraTest()
        {
            Camera camera = new Camera();
            Assert.IsTrue(camera.scale ==1);

            // Get View
            var view = camera.GetView();
            Assert.IsTrue(view == Matrix.Identity);

            Texture2D bg1 = Globals.Content.Load<Texture2D>("BG1-320px");

            // BG Transform
            var bgTransform = camera.GetBgTransform(bg1);
            Assert.IsTrue(bgTransform != Matrix.Identity);

            // Projection
            var projection = Matrix.CreateOrthographicOffCenter(Globals.ScreenSize, 0, 1);
            Assert.IsTrue(projection != Matrix.Identity);

            // UI Scale
            var ui = camera.GetUIScale();
            Assert.IsTrue(ui == Matrix.Identity);

        }
    }
}
