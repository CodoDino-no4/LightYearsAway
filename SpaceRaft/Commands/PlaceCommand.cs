using LYA.Sprites;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Collections;

namespace LYA.Commands
{
		public class PlaceCommand : CommandManager.ICommand
		{
				private Vector2 position;

				private Vector2 direction;

				private Tile tile;

				private Bag<Tile> sprites;
				public PlaceCommand( Astro astro, Tile tile, Bag<Tile> sprites ) : base()
				{
						direction=astro.Direction;
						position=astro.Position;
						this.tile=tile;
						this.sprites=sprites;
				}

				public void Execute()
				{
						tile.Position=position;

						// Left and right placement
						//if (direction.X==1||direction.X==0) //default to right
						//		tile.Position.X-=10;

						//if (direction.X==-1) //left
						//		tile.Position.X-=10;

						sprites.Add( (Tile) tile.Clone() );
				}
		}
}
