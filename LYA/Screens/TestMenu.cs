using LYA.Helpers;
using LYA.Networking;
using LYA.Testing;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Myra;
using Myra.Graphics2D.UI;
using System.Net;

namespace LYA.Screens
{
		/// <summary>
		/// Main Menu Game Screen
		/// </summary>
		public class TestMenu : GameScreen
		{
				private ClientManager clientManager;

				private Desktop desktop;
				private Grid grid;

				public TestMenu( Game game, ClientManager clientManager ) : base( game )
				{
						this.clientManager=clientManager;
				}

				private new LYA Game => (LYA) base.Game;

				public override void LoadContent()
				{
						base.LoadContent();

						MyraEnvironment.Game=Game;

						grid=new Grid
						{
								ShowGridLines=true,
								RowSpacing=50,
								ColumnSpacing=50
						};

						grid.ColumnsProportions.Add( new Proportion( ProportionType.Auto ) );
						grid.ColumnsProportions.Add( new Proportion( ProportionType.Auto ) );
						grid.RowsProportions.Add( new Proportion( ProportionType.Auto ) );
						grid.RowsProportions.Add( new Proportion( ProportionType.Auto ) );

						// Labels

						grid.Widgets.Add( DrawLabel( "title", "Light-Years Away", 2, 1 ) );

						// Buttons

						var serverBtn = new TextButton
						{
								GridColumn = 2,
								GridRow = 2,
								Text = "Start Server",
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						serverBtn.Click+=( s, a ) =>
						{
								Tests.ServerInit();
								Thread.Sleep( 2000 );
						};

						var testPlayerBtn = new TextButton
						{
								GridColumn = 2,
								GridRow = 3,
								Text = "Start Test Player",
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						testPlayerBtn.Click+=( s, a ) =>
						{
								try
								{
										// Initalise connection to server
										clientManager.Init( IPAddress.Parse( "192.168.1.101" ), Int32.Parse( "11000" ) ); //try with public ip

										if (clientManager.isInit)
										{
												grid.Widgets.Add( DrawLabel( "success", "SUCCESS", 2, 5 ) );

												// Start game
												Globals.ScreenManager.LoadScreen( new OuterSpace( Game, clientManager ), new FadeTransition( GraphicsDevice, Color.Black, 4 ) );
										}
										else
										{
												var errBox = Dialog.CreateMessageBox("Error", "Server Address Not Found");
												errBox.ShowModal( desktop );
										}
								}
								catch
								{
										var errBox = Dialog.CreateMessageBox("Error", "Server Address Not Found");
										errBox.ShowModal( desktop );
								}
						};

						var backBtn = new TextButton
						{
								GridColumn = 2,
								GridRow = 4,
								Text = "Back",
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						backBtn.Click+=( s, a ) =>
						{
								Globals.ScreenManager.LoadScreen( new MainMenu( Game, clientManager ), new FadeTransition( GraphicsDevice, Color.Black, 4 ) );
						};

						var exitBtn = new TextButton
						{
								GridColumn = 2,
								GridRow = 5,
								Text = "Exit",
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						exitBtn.Click+=( s, a ) =>
						{
								Game.Exit();
						};


						grid.Widgets.Add( serverBtn );
						grid.Widgets.Add( testPlayerBtn );
						grid.Widgets.Add( backBtn );
						grid.Widgets.Add( exitBtn );

						// Add it to the desktop
						desktop=new Desktop
						{
								Root=grid
						};
				}

				private Label DrawLabel( string id, string text, int column, int row )
				{
						var label = new Label
						{
								Id = id,
								Text = text,
								GridColumn = column,
								GridRow = row
						};

						return label;
				}

				public override void Draw( GameTime gameTime )
				{
						GraphicsDevice.Clear( Color.Black );
						desktop.Render();
				}

				public override void Update( GameTime gameTime )
				{

				}
		}
}
