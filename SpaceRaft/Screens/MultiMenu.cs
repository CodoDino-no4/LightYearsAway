using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using Myra.Graphics2D.UI;
using Myra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYA.Networking;
using System.Net;
using LYA.Helpers;
using MonoGame.Extended.Screens.Transitions;
using Microsoft.Extensions.Hosting;

namespace LYA.Screens
{
		public class MultiMenu : GameScreen
		{
				private Desktop desktop;
				private Grid grid;
				ClientManager clientManager;
				string ipAddress;
				string port;

				public MultiMenu( Game game ) : base( game )
				{
				}

				private new LYA Game => (LYA) base.Game;

				public override void LoadContent()
				{
						base.LoadContent();

						MyraEnvironment.Game=Game;
						clientManager = new ClientManager();

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

						grid.Widgets.Add( DrawLabel( "title", "Light-Years Away Multiplayer", 2, 1 ) );
						grid.Widgets.Add( DrawLabel( "ip-input", "Enter Server IP Addess: ", 2, 2 ) );
						grid.Widgets.Add( DrawLabel( "port-input", "Enter Server Port: ", 2, 3 ) );

						// Inputs

						var ip = new TextBox
						{
								GridColumn = 3,
								GridRow = 2,
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						var port = new TextBox
						{
								GridColumn = 3,
								GridRow = 3,
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						// Buttons

						var connBtn = new TextButton
						{
								GridColumn = 3,
								GridRow = 4,
								Text = "Connect",
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						connBtn.Click+=( s, a ) =>
						{
								try
								{
										clientManager.Init( IPAddress.Parse( "192.168.1.255" ), Int32.Parse( "11000" ) );

										HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
										builder.Services.AddWindowsService( options =>
										{
												options.ServiceName=".NET Joke Service";
										} );


										if (clientManager.isInit)
										{
												Globals.ScreenManager.LoadScreen( new OuterSpace( this.Game ), new FadeTransition( GraphicsDevice, Color.Black, 4 ) );
										}
								}
								catch { 

										var errBox = Dialog.CreateMessageBox("Error", "Server Address Not Found");
										errBox.ShowModal( desktop );
								}

						};

						var exitBtn = new TextButton
						{
								GridColumn = 3,
								GridRow = 5,
								Text = "Exit",
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						exitBtn.Click+=( s, a ) =>
						{
								Game.Exit();
						};

						grid.Widgets.Add( connBtn );
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
