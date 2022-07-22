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

				Texture2D BG, FG, astroIdleTexture, junkAll;

				//for game state calculation
				double TotalSeconds = 0.0;
				double ElapsedSeconds = 0.0;

				private Camera camera;

				private Vector2 _playerPosition;

				public SpaceRaft()
				{
						graphics=new GraphicsDeviceManager( this );
						Content.RootDirectory="Content";
						IsMouseVisible=true;

						ScreenWidth=graphics.PreferredBackBufferWidth;
						ScreenHeight=graphics.PreferredBackBufferHeight;

						Window.AllowUserResizing=true;
						Window.Title="SpaceRaft";
						graphics.IsFullScreen=false;

						graphics.ApplyChanges();

						camera = new Camera(graphics.GraphicsDevice.Viewport);
				}

				protected override void Initialize()
				{
						base.Initialize();
				}

				protected override void LoadContent()
				{
						// Create a new SpriteBatch, which can be used to draw textures.
						spriteBatch=new SpriteBatch( GraphicsDevice );

						// BG content
						BG=Content.Load<Texture2D>( "BG1-square-BG-320px" );
						FG=Content.Load<Texture2D>( "BG1-square-FG-320px" );

						// Background sprites
						background=new List<Sprite>()
						{
								new BackgroundTile( BG),

								new BackgroundTile( FG)

						};

						// Player Astro content
						astroIdleTexture=Content.Load<Texture2D>( "Astro-Idle" );

						// Player Astro sprite
						astro=new Astro( astroIdleTexture );

						//Other sprites content
						junkAll=Content.Load<Texture2D>( "space_junk_all_frames" );
						//sprite2 = Content.Load<Texture2D>( "sprite" );

						// Other sprites
						sprites=new List<Sprite>()
						{
								// will hold sprites like spacejunk etc.
								new Sprite(junkAll)
						};
				}
				protected override void Draw( GameTime gameTime )
				{
					GraphicsDevice.Clear( Color.CornflowerBlue );

					spriteBatch.Begin( SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformMatrix: camera.Transform );

					// Background
					foreach (var sprite in background)
						sprite.Draw( spriteBatch, _playerPosition );

					// Astro
					astro.Draw( spriteBatch, _playerPosition );

					// Other Sprites
					foreach (var sprite in sprites)
						sprite.Draw( spriteBatch );

					spriteBatch.End();


					base.Draw( gameTime );
				}

				protected override void Update( GameTime gameTime )
				{
						//_playerPosition = camera.MoveCamera(_playerPosition);
						_playerPosition = camera.UpdateCamera( graphics.GraphicsDevice.Viewport, _playerPosition );

						base.Update( gameTime );
				}
		}
}
