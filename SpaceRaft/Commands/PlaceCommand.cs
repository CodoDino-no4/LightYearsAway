using LYA.Helpers;
using LYA.Sprites;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LYA.Commands
{
		public class PlaceCommand : CommandManager.ICommand
		{
				private Vector2 position;

				private Vector2 direction;

				private Texture2D tileTex;

				private Tile tile;

				private string tileName;
				public PlaceCommand( Astro astro ) : base()
				{
						direction=astro.Direction;
						position=astro.Position;
				}

				public void Execute()
				{

						// Ship Foundation content
						tileTex=Globals.Content.Load<Texture2D>( tileName );

						// Ship Foundation sprite
						tile=new Tile( tileTex );

						if (direction.X==-1)
						{
								position.X-=2;
								tile.Draw(); //how to actually do this dynamically with or without spritebatch?
														 //placeleft
						}

						if (direction.X==1)
						{
								position.X+=2;
								tile.Draw();
								//placeright
						}
				}



		}
}
