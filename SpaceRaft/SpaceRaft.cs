using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceRaft.Sprites;
using System.Collections.Generic;

namespace SpaceRaft
{
		public class SpaceRaft: Game
		{
				GraphicsDeviceManager graphics;
				SpriteBatch spriteBatch;
				private RenderTarget2D renderTarget;

				public static int ScreenWidth;
				public static int ScreenHeight;

				public Astro astro;
				private List<Sprite> background;
				public List<Sprite> spaceJunk;

				Texture2D BG, FG;				Texture2D astroIdleTexture;				Texture2D junk1, junk2, junk3, junk4, junk5;

				Effect effect;

				//for game state calculation
				double TotalSeconds = 0.0;
				double ElapsedSeconds = 0.0;

				private Camera camera;

				private Vector2 _playerPosition;

				public SpaceRaft()
				{
						// Graphics manager
						graphics=new GraphicsDeviceManager(this);

						// Content directory
						Content.RootDirectory="Content";
						Globals.Content=Content;

				}

				protected override void Initialize()
				{
						// Graphics settings
						graphics.PreferredBackBufferWidth=1200;
						graphics.PreferredBackBufferHeight=960;
						graphics.IsFullScreen=false;

						Window.AllowUserResizing=true;
						Window.Title="SpaceRaft";
						IsMouseVisible=true;

						graphics.ApplyChanges();

						camera=new Camera(graphics.GraphicsDevice.Viewport);

						base.Initialize();
				}

				protected override void LoadContent()
				{
						// Create a new SpriteBatch, which can be used to draw textures.
						spriteBatch=new SpriteBatch(GraphicsDevice);
						Globals.SpriteBatch=spriteBatch;

						effect=Globals.Content.Load<Effect>("InfinateBackground");
						//effect.Parameters["ViewportSize"].SetValue(new Vector2(200, 200));

						// BG content
						BG=Globals.Content.Load<Texture2D>("BG1-square-BG-large");
						FG=Globals.Content.Load<Texture2D>("BG1-square-FG-large");

						// Background sprites
						background=new List<Sprite>()
						{
								new BackgroundTile( BG),

								new BackgroundTile( FG)

						};

						// Player Astro content
						astroIdleTexture=Globals.Content.Load<Texture2D>("Astro-Idle");

						// Player Astro sprite
						astro=new Astro(astroIdleTexture);
						LoadJunk();
				}
				protected override void Draw(GameTime gameTime)
				{
						GraphicsDevice.Clear(Color.SlateGray);

						Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, transformMatrix: camera.Transform);

						// Background
						foreach (var sprite in background)
								sprite.Draw(Globals.SpriteBatch, _playerPosition);

						// Other Sprites
						foreach (var sprite in spaceJunk)
								sprite.Draw(Globals.SpriteBatch);

						// Astro
						astro.Draw(Globals.SpriteBatch, _playerPosition);

						Globals.SpriteBatch.End();

						base.Draw(gameTime);
				}

				protected override void Update(GameTime gameTime)
				{
						// Update the postion after moving
						_playerPosition=camera.MoveCamera(_playerPosition);

						// Update the camera based on the new position
						camera.UpdateCamera(graphics.GraphicsDevice.Viewport, _playerPosition);

						// Update Junk sprites
						foreach (var sprite in spaceJunk)
								sprite.Update(gameTime);

						base.Update(gameTime);
				}

				private List<Sprite> LoadJunk()
				{
						// Junk content
						junk1=Globals.Content.Load<Texture2D>("junk-1");
						junk2=Globals.Content.Load<Texture2D>("junk-2");						junk3=Globals.Content.Load<Texture2D>("junk-3");						junk4=Globals.Content.Load<Texture2D>("junk-4");						junk5=Globals.Content.Load<Texture2D>("junk-5");

						// Junk sprites
						spaceJunk=new List<Sprite>()
						{
								new Junk(junk1)
								{ Position = new Vector2(200, 40)},								new Junk(junk2)
								{ Position = new Vector2(-40, -40)},								new Junk(junk3)
								{ Position = new Vector2(40, 0)},								new Junk(junk4)
								{ Position = new Vector2(0, 100)},								new Junk(junk5)
								{ Position = new Vector2(40, 90)}
						};

						return spaceJunk;
				}
		}
}
