using Apos.Input;
using LYA._Camera;
using LYA.Commands;
using LYA.Helpers;
using LYA.Managers;
using LYA.Networking;
using LYA.Screens;
using LYA.Sprites;
using LYA.Sprites.Background;
using LYA.Sprites.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace LYA
{
		public class LYA : Game
		{
				// Startup
				private bool isLoaded = false;

				// Graphics
				private GraphicsDeviceManager graphics;
				private SpriteBatch spriteBatch;
				private bool isFullscreen;
				private int customFPS;

				// Camera
				private Camera camera;
				private Effect bgInfinateShader;

				// Managers
				private ScreenManager screenManager;

				// Textures
				private Texture2D bg1, bg2;
				private Texture2D astroIdleTex;
				private Texture2D toolBelt;
				private Texture2D foundationTex;

				// Sprite Objects
				public Astro astro;
				private Deque<BaseSprite> sprites;
				private Deque<BaseSprite> uiSprites;

				// Networking
				public ClientManager clientManager;

				public LYA()
				{
						// Graphics manager
						graphics=new GraphicsDeviceManager( this );

						// Content directory
						Content.RootDirectory="Content";
						Globals.Content=Content;

						// Networking
						clientManager=new ClientManager();

						// Screen Management
						screenManager=new ScreenManager();
						Components.Add( screenManager );
				}

				protected override void Initialize()
				{
						// Graphics settings
						isFullscreen=false;
						graphics.PreferredBackBufferWidth=2000;
						graphics.PreferredBackBufferHeight=1000;
						graphics.IsFullScreen=isFullscreen;
						customFPS=60;

						// Timestep/FPS
						graphics.SynchronizeWithVerticalRetrace=false;
						IsFixedTimeStep=true;
						TargetElapsedTime=TimeSpan.FromMilliseconds( 1000.0f/customFPS );

						Window.AllowUserResizing=true;
						Window.Title="Light-Years Away";
						IsMouseVisible=true;

						graphics.ApplyChanges();
						
						// Set inital viewport 
						Globals.ScreenSize=graphics.GraphicsDevice.Viewport.Bounds;

						// Create a new SpriteBatch
						spriteBatch=new SpriteBatch( GraphicsDevice );
						Globals.SpriteBatch=spriteBatch;

						base.Initialize();

						//if (!isLoaded)
						//{
						//		LoadSplash();
						//}

						//LoadMainMenu();
						LoadOuterSpace();
				}

				private void LoadSplash()
				{
						screenManager.LoadScreen( new Splash( this ), new FadeTransition( GraphicsDevice, Color.Black ) );
				}

				private void LoadMainMenu()
				{
						screenManager.LoadScreen( new MainMenu( this ), new FadeTransition( GraphicsDevice, Color.Black ) );
				}

				private void LoadOuterSpace()
				{
						screenManager.LoadScreen( new OuterSpace( this ), new FadeTransition( GraphicsDevice, Color.Black ) );
				}

				protected override void LoadContent()
				{
						InputHelper.Setup( this );
				}
				protected override void Draw( GameTime gameTime )
				{
						base.Draw( gameTime );
				}

				protected override void Update( GameTime gameTime )
				{
						Globals.Update( gameTime, graphics );
						base.Update( gameTime );
				}

				protected override void UnloadContent()
				{
						//base.UnloadContent();
				}

		}
}
