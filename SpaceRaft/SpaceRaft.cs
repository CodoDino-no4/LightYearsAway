using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceRaft.Helpers;
using SpaceRaft.Sprites;
using SpaceRaft.Sprites.Background;
using SpaceRaft.Sprites.GUI;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpaceRaft
{
		public class SpaceRaft: Game
		{
				GraphicsDeviceManager graphics;
				SpriteBatch spriteBatch;

				// Camera
				private Camera camera;
				Effect bgInfinateShader;

				// Managers
				BGManager bgManager;
				UIManager UIManager;

				// Textures
				Texture2D BG1, BG2;
				Texture2D astroIdleTexture;
				Texture2D junk1, junk2, junk3, junk4, junk5;
				Texture2D toolBelt;

				// Sprite Objects
				public Astro astro;
				public List<SpriteHandler> spaceJunk;
				public List<SpriteHandler> UIElements;

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
						graphics.SynchronizeWithVerticalRetrace=true;
						graphics.PreferredBackBufferWidth=1000;
						graphics.PreferredBackBufferHeight=1000;
						graphics.IsFullScreen=false;

						Window.AllowUserResizing=true;
						Window.Title="SpaceRaft";
						IsMouseVisible=true;

						graphics.ApplyChanges();

						// Set inital viewport 
						Globals.ScreenSize=graphics.GraphicsDevice.Viewport.Bounds;

						// Camera
						camera=new Camera();

						// Managers
						bgManager=new BGManager();
						UIManager=new UIManager();

						// Create a new SpriteBatch
						spriteBatch=new SpriteBatch(GraphicsDevice);
						Globals.SpriteBatch=spriteBatch;

						base.Initialize();
				}

				protected override void LoadContent()
				{
						InputHelper.Setup(this);

						bgInfinateShader=Content.Load<Effect>("infinite");

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

						// UI content
						toolBelt=Globals.Content.Load<Texture2D>("Toolbelt-empty");

						// UI sprites
						UIManager.AddElement(new UIElement(toolBelt));
				}
				protected override void Draw(GameTime gameTime)
				{
						// Background colour
						GraphicsDevice.Clear(Color.SlateGray);

						Matrix projection = Matrix.CreateOrthographicOffCenter(Globals.ScreenSize.X, Globals.ScreenSize.Width, Globals.ScreenSize.Height, Globals.ScreenSize.Y, 0, 1);
						Matrix uv_transform = camera.GetUVTransform(BG1, Vector2.Zero, 2f);
						camera.GetFixedScaleView();

						bgInfinateShader.Parameters["view_projection"].SetValue(Matrix.Identity*projection);
						bgInfinateShader.Parameters["uv_transform"].SetValue(Matrix.Invert(uv_transform));

						////////////////////////////////////////////////

						/* Begin Spritebatch
						 * Infinate Background */

						Globals.SpriteBatch.Begin(effect: bgInfinateShader, samplerState: SamplerState.PointWrap, transformMatrix: camera.Transform);

						bgManager.DrawBackground();

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////

						/* Begin Spritebatch
						 * Variable Position Sprites */

						Globals.SpriteBatch.Begin(samplerState: SamplerState.PointWrap, transformMatrix: camera.Transform);

						// Other Sprites
						foreach (var sprite in spaceJunk)
								sprite.Draw();

						// Astro
						astro.Draw();

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////

						/* Begin Spritebatch
						 * Fixed Position Sprites */

						Globals.SpriteBatch.Begin(samplerState: SamplerState.PointWrap, transformMatrix: camera.FixedTransform);

						// UI Manager
						UIManager.DrawElements();

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////

						base.Draw(gameTime);
				}

				protected override void Update(GameTime gameTime)
				{
						InputHelper.UpdateSetup();

						// Update Astro
						astro.Update();

						// Update the camera
						camera.UpdateCameraInput(astro.Position);

						//Update BG sprites
						bgManager.Update();

						// Update Junk sprites
						foreach (var sprite in spaceJunk)
								sprite.Update();

						//Update UI Sprites
						UIManager.Update(astro.Position);

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
