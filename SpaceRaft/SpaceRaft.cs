using Apos.Input;
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
				public List<SpriteHandler> spaceJunk;

				Texture2D BG, FG;				Texture2D astroIdleTexture;				Texture2D junk1, junk2, junk3, junk4, junk5;

				BGManager bgManager;

				Effect effect;

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

						// will be where we left off at (serialisation)
						_playerPosition=new Vector2(0, 0);
						Globals.playerPosition=_playerPosition;

						// Camera
						camera=new Camera(graphics.GraphicsDevice.Viewport);

						// Background manager
						bgManager=new BGManager();

						// Create a new SpriteBatch, which can be used to draw textures.
						spriteBatch=new SpriteBatch(GraphicsDevice);
						Globals.SpriteBatch=spriteBatch;

						base.Initialize();
				}

				protected override void LoadContent()
				{
						InputHelper.Setup(this);

						// Background content
						BG=Globals.Content.Load<Texture2D>("BG1-320px");
						FG=Globals.Content.Load<Texture2D>("BG2-320px");

						// Background sprites
						bgManager.AddLayer(new BGLayer(BG, 0.1f, 0.5f));
						bgManager.AddLayer(new BGLayer(FG, 0.2f, 0.1f));
						//bgManager.AddLayer(new BGLayer(FG1));
						//bgManager.AddLayer(new BGLayer(FG2));

						// Player Astro content
						astroIdleTexture=Globals.Content.Load<Texture2D>("Astro-Idle");

						// Player Astro sprite
						astro=new Astro(astroIdleTexture);
						LoadJunk();
				}
				protected override void Draw(GameTime gameTime)
				{
						// Background colour
						GraphicsDevice.Clear(Color.SlateGray);

						// Begin Spritebatch
						Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, transformMatrix: camera.Transform);

						// Background
						bgManager.DrawInCentre();

						// Astro
						astro.DrawInCentre();

						// Other Sprites
						foreach (var sprite in spaceJunk)
								sprite.Draw();

						// End SpriteBatch
						Globals.SpriteBatch.End();

						base.Draw(gameTime);
				}

				protected override void Update(GameTime gameTime)
				{
						InputHelper.UpdateSetup();

						// Update the postion after moving
						Globals.playerPosition=camera.MoveCamera(Globals.playerPosition);

						// Update the camera based on the new position
						camera.UpdateCamera(graphics.GraphicsDevice.Viewport, Globals.playerPosition);

						//Update BG sprites
						bgManager.Update(gameTime);

						// Update Junk sprites
						foreach (var sprite in spaceJunk)
								sprite.Update(gameTime);

						Globals.Update(gameTime);
						InputHelper.UpdateCleanup();
						base.Update(gameTime);
				}

				private List<SpriteHandler> LoadJunk()
				{
						// Junk content
						junk1=Globals.Content.Load<Texture2D>("junk-1");
						junk2=Globals.Content.Load<Texture2D>("junk-2");						junk3=Globals.Content.Load<Texture2D>("junk-3");						junk4=Globals.Content.Load<Texture2D>("junk-4");						junk5=Globals.Content.Load<Texture2D>("junk-5");

						// Junk sprites
						spaceJunk=new List<SpriteHandler>()
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
