using LYA.Helpers;
using LYA.Networking;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Myra;
using Myra.Graphics2D.UI;
using System.Net;

namespace LYA.Screens
{
		/// <summary>
		/// Multi Menu Game Screen
		/// </summary>
		public class MultiMenu : GameScreen
		{
				private ClientManager clientManager;

				private Desktop desktop;
				private Grid grid;
				private Panel panel;
				private TextButton serverBtn;
				private string ipAddress;
				private string port;

				public MultiMenu( Game game, ClientManager clientManager ) : base( game )
				{
						this.clientManager=clientManager;
				}

				private new LYA Game => (LYA) base.Game;

				public override void Initialize()
				{
						desktop=new Desktop
						{
								HasExternalTextInput=true
						};

						base.Initialize();
				}

				public override void LoadContent()
				{
						base.LoadContent();

						MyraEnvironment.Game=Game;

						grid=new Grid
						{
								ShowGridLines=true,
								RowSpacing=30,
								ColumnSpacing=30
						};

						panel=new Panel()
						{

						};

						grid.ColumnsProportions.Add( new Proportion( ProportionType.Auto ) );
						grid.ColumnsProportions.Add( new Proportion( ProportionType.Auto ) );
						grid.RowsProportions.Add( new Proportion( ProportionType.Auto ) );
						grid.RowsProportions.Add( new Proportion( ProportionType.Auto ) );

						// Labels

						grid.Widgets.Add( DrawLabel( "title", "Light-Years Away Multiplayer", 2, 1 ) );
						grid.Widgets.Add( DrawLabel( "inter-port-input", "Enter Integrated Server Port: ", 2, 2 ) ); //
						grid.Widgets.Add( DrawLabel( "ip-input", "Enter Server IP Addess: ", 2, 4 ) );
						grid.Widgets.Add( DrawLabel( "port-input", "Enter Server Port: ", 2, 5 ) );

						// Inputs

						Game.Window.TextInput+=( s, a ) =>
						{
								desktop.OnChar( a.Character );
						};

						var ip = new TextBox
						{
								GridColumn = 3,
								GridRow = 4,
								HorizontalAlignment = HorizontalAlignment.Center,
								Width = 500,
								Height = 100,
						};

						var port = new TextBox
						{
								GridColumn = 3,
								GridRow = 5,
								HorizontalAlignment = HorizontalAlignment.Center,
								Width = 500,
								Height = 100
						};

						var serverPort = new TextBox
						{
								GridColumn = 3,
								GridRow = 2,
								HorizontalAlignment = HorizontalAlignment.Center,
								Width = 500,
								Height = 100
						};

						grid.Widgets.Add( ip );
						grid.Widgets.Add( port );
						grid.Widgets.Add( serverPort );

						// Buttons

						serverBtn=new TextButton
						{
								GridColumn=3,
								GridRow=3,
								Text="Start Integrated Server",
								HorizontalAlignment=HorizontalAlignment.Center,
						};

						serverBtn.Click+=( s, a ) =>
						{
								clientManager.StartIntegratedServer( serverPort.Text );
						};

						var connBtn = new TextButton
						{
								GridColumn = 3,
								GridRow = 6,
								Text = "Connect",
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						connBtn.Click+=( s, a ) =>
						{
								try
								{
										// Initalise connection to server
										clientManager.Init( IPAddress.Parse( ip.Text ), Int32.Parse( port.Text ) );

										if (clientManager.isInit)
										{
												grid.Widgets.Add( DrawLabel( "success", "SUCCESS", 2, 6 ) );

												// Start game
												Globals.ScreenManager.LoadScreen( new OuterSpace( Game, clientManager ), new FadeTransition( GraphicsDevice, Color.Black, 4 ) );
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
								GridColumn = 3,
								GridRow = 7,
								Text = "Back",
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						backBtn.Click+=( s, a ) =>
						{
								Globals.ScreenManager.LoadScreen( new MainMenu( Game, clientManager ), new FadeTransition( GraphicsDevice, Color.Black, 4 ) );
						};

						var exitBtn = new TextButton
						{
								GridColumn = 3,
								GridRow = 8,
								Text = "Exit",
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						exitBtn.Click+=( s, a ) =>
						{
								Game.Exit();
						};

						grid.Widgets.Add( serverBtn );
						grid.Widgets.Add( connBtn );
						grid.Widgets.Add( backBtn );
						grid.Widgets.Add( exitBtn );

						// Add it to the desktop
						desktop.Root=grid;
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
						if (clientManager.proc!=null)
						{
								serverBtn.Text="Integrated Server Running";
								serverBtn.Enabled=false;
						}
				}
		}
}
