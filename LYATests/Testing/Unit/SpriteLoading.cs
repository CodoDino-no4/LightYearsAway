﻿using LYA.Helpers;
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
    /// All sprites can be loaded with the correct initalisation values
    /// </summary>
    [DoNotParallelize()]
    [TestClass()]
    public class SpriteLoading
    {

        private LYA game;
        private Astro astro;
        private Tile tile;
        private BGLayer bg1;
        private BGLayer bg2;
        private Toolbelt tb;

        /// <summary>
        /// Initialises the game and runs one frame
        /// </summary>
        [TestInitialize()]
        public void Setup()
        {
            Globals.testing = true;
            Globals.IsMulti = false;

            game = new LYA();
            game.SuppressDraw();
            game.RunOneFrame();

        }

        [TestMethod()]
        public void astroTest()
        {
            astro = new Astro(Globals.Content.Load<Texture2D>("Astro-Idle"), 1);

            Assert.IsTrue(astro.InScene);
            Assert.IsTrue(astro.Position == Vector2.Zero);
            Assert.IsTrue(astro.state == Astro.State.swim);
            Assert.IsTrue(astro.Texture.Bounds == astro.Rectangle);

        }

        [TestMethod()]
        public void BGTest()
        {

            bg1 = new BGLayer(Globals.Content.Load<Texture2D>("BG1-320px"));
            bg2 = new BGLayer(Globals.Content.Load<Texture2D>("BG2-320px"));

            Assert.IsTrue(bg1.InScene);
            Assert.IsTrue(bg2.InScene);
            Assert.IsTrue(bg1.Texture.Bounds == bg1.Rectangle);
            Assert.IsTrue(bg2.Texture.Bounds == bg2.Rectangle);

        }

        [TestMethod()]
        public void ToolbeltTest()
        {
            tb = new Toolbelt(Globals.Content.Load<Texture2D>("toolbelt-empty"));

            Assert.IsTrue(tb.InScene);
            Assert.IsTrue(tb.Texture.Bounds == tb.Rectangle);
            Assert.IsTrue(tb.Position == new Vector2(Globals.ScreenSize.Width / 2, Globals.ScreenSize.Height - tb.Texture.Height * 2));
        }

        [TestMethod()]
        public void TileTest()
        {
            tile = new Tile(Globals.Content.Load<Texture2D>("foundation"));

            Assert.IsTrue(tile.InScene);
            Assert.IsTrue(tile.Texture.Bounds == tile.Rectangle);
            Assert.IsTrue(tile.type == Tile.Type.foundation);
            Assert.IsTrue(tile.Texture.Name == "foundation");

        }

        [TestCleanup()]
        public void CleanUp()
        {
            Thread.Sleep(2000);

            astro = null;
            tile = null;
            tb = null;
            bg1 = null;
            bg2 = null;

            game.Exit();
            game = null;
        }
    }
}
