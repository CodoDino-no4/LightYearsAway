using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Myra;
using Myra.Graphics2D.UI;
using static System.Net.Mime.MediaTypeNames;
using System.Data.Common;
using MonoGame.Extended.Screens.Transitions;
using LYA.Helpers;

namespace LYA.Screens
{
		internal class MainMenu : GameScreen
		{
				private Desktop desktop;
				private Grid grid;

				public MainMenu( Game game ) : base( game )
				{
				}

				private new LYA Game => (LYA) base.Game;

				public override void LoadContent()
				{
						base.LoadContent();

						MyraEnvironment.Game=Game;

						grid = new Grid
						{
								ShowGridLines = true,
								RowSpacing = 50,
								ColumnSpacing = 50
						};

						grid.ColumnsProportions.Add( new Proportion( ProportionType.Auto ) );
						grid.ColumnsProportions.Add( new Proportion( ProportionType.Auto ) );
						grid.RowsProportions.Add( new Proportion( ProportionType.Auto ) );
						grid.RowsProportions.Add( new Proportion( ProportionType.Auto ) );

						// Labels

						grid.Widgets.Add( DrawLabel("title", "Light-Years Away", 2, 1) );

						// Buttons

						var playBtn = new TextButton
						{
								GridColumn = 2,
								GridRow = 2,
								Text = "Play",
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						playBtn.Click+=( s, a ) =>
						{
								Globals.ScreenManager.LoadScreen( new OuterSpace( this.Game ), new FadeTransition( GraphicsDevice, Color.Black, 3 ) );
						};

						var multiBtn = new TextButton
						{
								GridColumn = 2,
								GridRow = 3,
								Text = "Multiplayer",
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						multiBtn.Click+=( s, a ) =>
						{
								Globals.ScreenManager.LoadScreen( new MultiMenu( this.Game ), new FadeTransition( GraphicsDevice, Color.Black, 3 ) );
						};

						var exitBtn = new TextButton
						{
								GridColumn = 2,
								GridRow = 4,
								Text = "Exit",
								HorizontalAlignment = HorizontalAlignment.Center,
						};

						exitBtn.Click+=( s, a ) =>
						{
								Game.Exit();
						};


						grid.Widgets.Add( playBtn );
						grid.Widgets.Add( multiBtn );
						grid.Widgets.Add( exitBtn );

						// Add it to the desktop
						desktop=new Desktop
						{
								Root=grid
						};
				}

				private Label DrawLabel(string id, string text, int column, int row)
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
