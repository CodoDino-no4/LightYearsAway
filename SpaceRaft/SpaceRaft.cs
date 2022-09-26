using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceRaft.Sprites;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpaceRaft
{
		public class SpaceRaft: Game
		{
				GraphicsDeviceManager graphics;
				SpriteBatch spriteBatch;

				public Astro astro;
				public List<SpriteHandler> spaceJunk;

				Texture2D BG1, BG2;
				Texture2D astroIdleTexture;
				Texture2D junk1, junk2, junk3, junk4, junk5;
				Texture2D toolBelt;

				BGManager bgManager;
				UIManager UIManager;
				Effect bgEffect;

				private Camera camera;

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
						graphics.PreferredBackBufferWidth=700;
						graphics.PreferredBackBufferHeight=700;
						graphics.IsFullScreen=false;
						graphics.SynchronizeWithVerticalRetrace=true;

						Window.AllowUserResizing=true;
						Window.Title="SpaceRaft";
						IsMouseVisible=true;

						graphics.ApplyChanges();

						// Set inital viewport 
						Globals.ScreenSize=graphics.GraphicsDevice.Viewport.Bounds;

						// Camera
						camera=new Camera();

						// Background manager
						bgManager=new BGManager(camera);
						UIManager=new UIManager();

						// Create a new SpriteBatch
						spriteBatch=new SpriteBatch(GraphicsDevice);
						Globals.SpriteBatch=spriteBatch;

						base.Initialize();
				}

				protected override void LoadContent()
				{
						InputHelper.Setup(this);

						bgEffect=Content.Load<Effect>("infinite");

						// Background content
						BG1=Globals.Content.Load<Texture2D>("BG1-320px");
						BG2=Globals.Content.Load<Texture2D>("BG2-320px");

						// Background sprites
						bgManager.AddLayer(new BGLayer(BG1, 0.1f, 0.5f));
						bgManager.AddLayer(new BGLayer(BG2, 0.2f, 0.1f));
						//bgManager.AddLayer(new BGLayer(FG1));
						//bgManager.AddLayer(new BGLayer(FG2));

						// Player Astro content
						astroIdleTexture=Globals.Content.Load<Texture2D>("Astro-Idle");

						// Player Astro sprite
						astro=new Astro(astroIdleTexture);

						LoadJunk();

						toolBelt=Globals.Content.Load<Texture2D>("Toolbelt-empty");
						UIManager.AddElement(new UIElement(toolBelt, new Vector2(0,0)));
						//LoadUI();
				}
				protected override void Draw(GameTime gameTime)
				{
						// Background colour
						GraphicsDevice.Clear(Color.SlateGray);

						Matrix projection = Matrix.CreateOrthographicOffCenter(Globals.ScreenSize.X, Globals.ScreenSize.Width, Globals.ScreenSize.Height, Globals.ScreenSize.Y, 0, 1);
						Matrix uv_transform = camera.GetUVTransform(BG1, Vector2.Zero, 2f);

						bgEffect.Parameters["view_projection"].SetValue(Matrix.Identity*projection);
						bgEffect.Parameters["uv_transform"].SetValue(Matrix.Invert(uv_transform));

						// Begin BG Spritebatch
						Globals.SpriteBatch.Begin(effect: bgEffect, samplerState: SamplerState.PointWrap, transformMatrix: camera.Transform);

						// Background
						bgManager.DrawBackground();

						// End BG SpriteBatch
						Globals.SpriteBatch.End();

						/////////////////////////////////////////////////////////////////

						// Begin Spritebatch
						Globals.SpriteBatch.Begin(samplerState: SamplerState.PointWrap, transformMatrix: camera.Transform);

						// Other Sprites
						foreach (var sprite in spaceJunk)
								sprite.Draw();

						// Astro
						astro.DrawAstro();

						// End SpriteBatch
						Globals.SpriteBatch.End();

						base.Draw(gameTime);
				}

				protected override void Update(GameTime gameTime)
				{
						InputHelper.UpdateSetup();

						// Update Astro
						astro.Update();

						// Update the camera
						camera.UpdateCameraInput(astro.Position);

						Debug.WriteLine(astro.Position);

						//Update BG sprites
						//bgManager.Update(gameTime);

						// Update Junk sprites
						foreach (var sprite in spaceJunk)
								sprite.Update();

						Globals.Update(gameTime, graphics);
						InputHelper.UpdateCleanup();
						base.Update(gameTime);
				}

				private List<SpriteHandler> LoadJunk()
				{
						// Junk content
						junk1=Globals.Content.Load<Texture2D>("junk-1");
						junk2=Globals.Content.Load<Texture2D>("junk-2");
						junk3=Globals.Content.Load<Texture2D>("junk-3");
						junk4=Globals.Content.Load<Texture2D>("junk-4");
						junk5=Globals.Content.Load<Texture2D>("junk-5");

						// Junk sprites
						spaceJunk=new List<SpriteHandler>()
						{
								new Junk(junk1)
								{ Position = new Vector2(50, 50)},
								new Junk(junk2)
								{ Position = new Vector2(-40, -40)},
								new Junk(junk3)
								{ Position = new Vector2(40, 0)},
								new Junk(junk4)
								{ Position = new Vector2(0, 100)},
								new Junk(junk5)
								{ Position = new Vector2(40, 90)}
						};

						return spaceJunk;
				}
		}
}
