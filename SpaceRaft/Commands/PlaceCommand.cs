using LYA.Sprites;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Collections;
using System.Collections.Generic;

namespace LYA.Commands
{
		public class PlaceCommand : CommandManager.ICommand
		{
				private Vector2 position;

				private Vector2 direction;

				private Tile tile;

				private Deque<BaseSprite> sprites;
				public PlaceCommand( Astro astro, Tile tile, Deque<BaseSprite> sprites ) : base()
				{
						direction=astro.Direction;
						position=astro.Position;
						this.tile=tile;
						this.sprites=sprites;
				}

				public void Execute()
				{
						tile.Position=position;

						if (direction.X==1||direction.X==0) //default to right
								tile.Position.X-=10;

						if (direction.X==-1) //left
								tile.Position.X-=10;

						//tile.Clone(); //doesnt actually clone anything?? hmm
						sprites.AddToFront( tile );
				}
		}
}
