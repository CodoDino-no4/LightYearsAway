using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using SpaceRaft.Models;
using SpaceRaft.Sprites;

namespace SpaceRaft
{
    public class SpaceRaft : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

				public Astro astro;
        private List<Sprite> background;
				public List<Sprite> sprites;

				Texture2D BG, FG, astroIdleTexture;

				//for game state calculation
				double TotalSeconds = 0.0;
        double ElapsedSeconds = 0.0;

				Camera camera;

				//NOTES
				//https://www.youtube.com/watch?v=ceBCDKU_mNw&list=PLV27bZtgVIJqoeHrQq6Mt_S1-Fvq_zzGZ&index=14
				//we are doing this playlist
				//we will get the camera to target a sprite and that might give us more info on how to attach the backgorund to it as well
				//oyouu also has a viedo on a static background
				//we will start a bit from scratch and try to build it up from there
				//BACKGROUND and ASTRO attached to camera
				//no reason for it to ever be different
				//woo we got this <3

				public SpaceRaft()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            ScreenWidth = graphics.PreferredBackBufferWidth;
            ScreenHeight = graphics.PreferredBackBufferHeight;

            Window.AllowUserResizing = true;
            Window.Title = "SpaceRaft";
            graphics.IsFullScreen = false;

            graphics.ApplyChanges();

						camera = new Camera( GraphicsDevice.Viewport );
				}
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

						// BG content
						BG = Content.Load<Texture2D>( "BG1-square-BG-320px" );
						FG = Content.Load<Texture2D>( "BG1-square-FG-320px" );


						// Background Sprites
						background = new List<Sprite>()
						{
								new BackgroundTile( BG),

								new BackgroundTile( FG),

						};

						// Player Astro Sprite
						astroIdleTexture = Content.Load<Texture2D>("Astro-Idle");
						astro = new Astro( astroIdleTexture );

            sprites = new List<Sprite>()
            {
                 // will hold sprites like spacejunk etc.
            };

				}

				protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

						int seconds = (int)gameTime.TotalGameTime.TotalSeconds / 3;

						spriteBatch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);

						//center sprites
						foreach (var sprite in background)
								sprite.Draw( spriteBatch );

						astro.Draw( spriteBatch );

						//other sprites
						foreach (var sprite in sprites)
                sprite.Draw(spriteBatch);

						spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            // track elapsed time since last frame, add since the game started
            // records the time that has elapsed since the last call to the update and other tracks the total time that the game has been running.
            ElapsedSeconds = gameTime.ElapsedGameTime.TotalSeconds;
            TotalSeconds += ElapsedSeconds;

						foreach (var sprite in background)
								sprite.Update(gameTime);

						astro.Update( gameTime );

						// update all the sprites in list
						foreach (var sprite in sprites)
                sprite.Update(gameTime);

						// update camera
						camera.UpdateCamera( GraphicsDevice.Viewport, astro );
						base.Update(gameTime);
        }

				//public void LoadBackground()
				//{
				//		//BG = Content.Load<Texture2D>( "BG1-square-BG-320px" );
				//		//FG = Content.Load<Texture2D>( "BG1-square-FG-320px" );

				//		// list of all the background tiles (24)
				//		//gridBG = new List<Sprite>();

				//		//// set background tile in a grid that fills the screen
				//		//for (int y = 0; y <= ScreenHeight / BG.Height + 1; y++)
				//		//{
				//		//		Loc.Y = BgTileOffset.Y + y * BG.Height;

				//		//		for (int x = 0; x <= ScreenWidth / BG.Width + 1; x++)
				//		//		{
				//		//				Loc.X = BgTileOffset.X + x * BG.Width;

				//		//				Sprite bg = new BackgroundTile( BG );
				//		//				bg.Position = Loc;
				//		//				gridBG.Add( bg );
				//		//				Sprite fg = new BackgroundTile( FG );
				//		//				fg.Position = Loc;
				//		//				gridBG.Add( fg );
				//		//		}
				//		//}
				//}
		}
}
