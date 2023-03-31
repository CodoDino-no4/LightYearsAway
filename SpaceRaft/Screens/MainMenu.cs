using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Myra;
using Myra.Graphics2D.UI;

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

						//playBtn.Click+=( s, a ) =>
						//{
						//		var messageBox = Dialog.CreateMessageBox("Message", "Some message!");
						//		messageBox.ShowModal( _desktop );
						//};

						grid.Widgets.Add( DrawBtn( 2, 2, "Play" ) );
						grid.Widgets.Add( DrawBtn( 2, 3, "Multiplayer" ) );
						grid.Widgets.Add( DrawBtn( 2, 4, "Exit" ) );

						// Add it to the desktop
						desktop=new Desktop
						{
								Root=grid
						};
				}

				private TextButton DrawBtn(int column, int row, string text)
				{
						var btn = new TextButton
						{
								GridColumn = column,
								GridRow = row,
								Text = text,
						};

						return btn;
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
