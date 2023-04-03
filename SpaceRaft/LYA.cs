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
				private bool isLoading;
				private float delay;
				private float timeRemaining;

				// Graphics
				private GraphicsDeviceManager graphics;
				private SpriteBatch spriteBatch;
				private bool isFullscreen;
				private int customFPS;

				// Managers
				private ScreenManager screenManager;

				// Networking
				public ClientManager clientManager;

				// Menu
				private bool isMenuOpen;

				public LYA()
				{
						// Startup
						isLoading=true;
						delay=5;
						timeRemaining=delay;

						// Graphics manager
						graphics=new GraphicsDeviceManager( this );

						// Content directory
						Content.RootDirectory="Content";
						Globals.Content=Content;

						// Networking
						clientManager=new ClientManager();

						// Screen Management
						screenManager=new ScreenManager();
						Globals.ScreenManager = screenManager;
						Components.Add( Globals.ScreenManager );

						// Menu
						isMenuOpen=true;
				}

				protected override void Initialize()
				{
						// Graphics settings
						isFullscreen=false;
						graphics.PreferredBackBufferWidth=2000;
						graphics.PreferredBackBufferHeight=1000;
						graphics.IsFullScreen=isFullscreen;
						customFPS=60;

						// Timestep
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
						Globals.MaxPlayers=8;
						Globals.playerCount=3;

						base.Initialize();

						Globals.ScreenManager.LoadScreen( new MainMenu( this, clientManager ), new FadeTransition( GraphicsDevice, Color.Black, 3 ) );
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
						InputHelper.UpdateSetup();

						Globals.Update( gameTime, graphics, screenManager );

						if (InputBindings.Menu().Pressed())
						{
								isMenuOpen=!isMenuOpen;
						}

						//if (isMenuOpen)
						//{
						//		screenManager.LoadScreen( new MainMenu( this ), new FadeTransition( GraphicsDevice, Color.Black, 3 ) );
						//}

						//if (isLoading)
						//{
						//		if (timeRemaining<=Globals.ElapsedSeconds)
						//		{
						//				screenManager.LoadScreen( new MainMenu( this ), new FadeTransition( GraphicsDevice, Color.Black, 3 ) );
						//				isLoading=false;
						//		}
						//}

						base.Update( gameTime );

						InputHelper.UpdateCleanup();

						if (Globals.isMulti)
						{
								clientManager.MessageLoop();
						}
				}

				protected override void UnloadContent()
				{
						//base.UnloadContent();
				}

		}
}
