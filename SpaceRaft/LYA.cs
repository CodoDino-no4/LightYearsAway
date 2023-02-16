using Apos.Input;
using LYA._Camera;
using LYA.Commands;
using LYA.Helpers;
using LYA.Managers;
using LYA.Sprites;
using LYA.Sprites.Background;
using LYA.Sprites.GUI;
using LYA.Sprites.Junk;
using LYA.Sprites.ShipTile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LYA
{
		public class LYA : Game
		{
				// Graphics
				private GraphicsDeviceManager graphics;
				private SpriteBatch spriteBatch;
				private bool isFullscreen;

				// Camera
				private Camera camera;
				private Effect bgInfinateShader;

				// Managers
				private BGManager bgManager;
				private UIManager uiManager;
				private JunkManager junkManager;

				// Textures
				private Texture2D bg1, bg2;
				private Texture2D astroIdleTex;
				private Texture2D junk1, junk2, junk3, junk4, junk5;
				private Texture2D toolBelt;
				private Texture2D foundationTex;

				// Sprite Objects
				public Astro astro;
				private Tile foundationTile;
				private List<SpriteHandler> spaceJunk;

				InputHandler inputHandler;
				CommandManager commandManager;

				public LYA()
				{
						// Graphics manager
						graphics=new GraphicsDeviceManager( this );

						// Content directory
						Content.RootDirectory="Content";
						Globals.Content=Content;
				}

				protected override void Initialize()
				{
						// Graphics settings
						isFullscreen=false;
						graphics.SynchronizeWithVerticalRetrace=true;
						graphics.PreferredBackBufferWidth=2000;
						graphics.PreferredBackBufferHeight=1000;
						graphics.IsFullScreen=isFullscreen;

						Window.AllowUserResizing=true;
						Window.Title="Light-Years Away";
						IsMouseVisible=true;

						graphics.ApplyChanges();

						// Set inital viewport 
						Globals.ScreenSize=graphics.GraphicsDevice.Viewport.Bounds;

						// Camera
						camera=new Camera();

						// Managers
						bgManager=new BGManager();
						uiManager=new UIManager();
						junkManager=new JunkManager();

						inputHandler=new InputHandler();

						commandManager=new CommandManager();

						// Create a new SpriteBatch
						spriteBatch=new SpriteBatch( GraphicsDevice );
						Globals.SpriteBatch=spriteBatch;

						base.Initialize();
				}

				protected override void LoadContent()
				{
						InputHelper.Setup( this );

						bgInfinateShader=Content.Load<Effect>( "infinite" );

						// Background content
						bg1=Globals.Content.Load<Texture2D>( "BG1-320px" );
						bg2=Globals.Content.Load<Texture2D>( "BG2-320px" );

						// Background sprites
						bgManager.AddElement( new BGLayer( bg1 ) );
						bgManager.AddElement( new BGLayer( bg2 ) );
						//bgManager.AddLayer(new BGLayer(FG1));
						//bgManager.AddLayer(new BGLayer(FG2));

						// Player Astro content
						astroIdleTex=Globals.Content.Load<Texture2D>( "Astro-Idle" );

						// Player Astro sprite
						astro=new Astro( astroIdleTex );

						// Junk content
						junk1=Globals.Content.Load<Texture2D>( "junk-1" );
						junk2=Globals.Content.Load<Texture2D>( "junk-2" );
						junk3=Globals.Content.Load<Texture2D>( "junk-3" );
						junk4=Globals.Content.Load<Texture2D>( "junk-4" );
						junk5=Globals.Content.Load<Texture2D>( "junk-5" );

						// Junk sprites
						junkManager.AddElement( new Junk( junk1 ) );
						spaceJunk=new List<SpriteHandler>()
						{
								new Junk(junk1)
								{ Position = new Vector2(0, 0)},
								new Junk(junk2)
								{ Position = new Vector2(-40, -40)},
								new Junk(junk3)
								{ Position = new Vector2(40, 0)},
								new Junk(junk4)
								{ Position = new Vector2(0, 100)},
								new Junk(junk5)
								{ Position = new Vector2(40, 90)}
						};

						// UI content
						toolBelt=Globals.Content.Load<Texture2D>( "toolbelt-empty" );

						uiManager.AddElement( new Toolbelt( toolBelt ) );

						// Ship Foundation content
						foundationTex=Globals.Content.Load<Texture2D>( "foundation" );

						// Ship Foundation sprite
						foundationTile=new Tile( foundationTex );

				}
				protected override void Draw( GameTime gameTime )
				{
						// Background colour
						GraphicsDevice.Clear( Color.SlateGray );

						Matrix projection = Matrix.CreateOrthographicOffCenter(Globals.ScreenSize, 0, 1);
						Matrix bg_transform = camera.GetBgTransform(bg1);
						Matrix ui_scale = camera.GetUIScale();

						bgInfinateShader.Parameters[ "view_projection" ].SetValue( Matrix.Identity*projection );
						bgInfinateShader.Parameters[ "uv_transform" ].SetValue( Matrix.Invert( bg_transform ) );

						////////////////////////////////////////////////

						/* Begin Spritebatch
						 * Infinate Background */

						Globals.SpriteBatch.Begin( effect: bgInfinateShader, samplerState: SamplerState.PointWrap, transformMatrix: camera.Transform );

						bgManager.Draw();

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////

						/* Begin Spritebatch
						 * Variable position Sprites */

						Globals.SpriteBatch.Begin( samplerState: SamplerState.PointWrap, transformMatrix: camera.Transform );

						// Other Sprites
						foreach (var sprite in spaceJunk)
								sprite.Draw();

						foundationTile.Draw();

						// Astro
						astro.Draw();

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////

						/* Begin Spritebatch
						 * UI Layer Sprites */

						Globals.SpriteBatch.Begin( samplerState: SamplerState.PointWrap, transformMatrix: ui_scale );

						// UI Manager
						uiManager.Draw();

						Globals.SpriteBatch.End();

						////////////////////////////////////////////////

						base.Draw( gameTime );
				}

				protected override void Update( GameTime gameTime )
				{
						InputHelper.UpdateSetup();

						// Update the camera
						camera.UpdateCameraInput( inputHandler.PlayerCameraMovement( astro ) );

						//Update BG sprites
						bgManager.Update();

						// Update Junk sprites
						foreach (var sprite in spaceJunk)
								sprite.Update();

						//Update UI Sprites
						uiManager.Update();

						HasQuit();

						Globals.Update( gameTime, graphics );
						InputHelper.UpdateCleanup();
						base.Update( gameTime );
				}

				private void HasQuit()
				{
						if (InputBindings.Quit().Pressed())
								Exit();
				}
		}
}
