using LYA.Helpers;
using LYA.Sprites;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LYA.Commands
{
		public class PlaceCommand : CommandManager.ICommand
		{
				private Vector2 position;

				private Vector2 direction;

				private Tile tile;

				private string tileName;

				private List<BaseSprite> sprites;
				public PlaceCommand( Astro astro, Tile tile, List<BaseSprite> sprites ) : base()
				{
						direction=astro.Direction;
						position=astro.Position;
						this.tile = tile;
						this.sprites=sprites;
				}

				public void Execute()
				{
						if (direction.X==1 || direction.X==0) //default to right
						{
								//position.X+=3;
								tile.Clone();
								sprites.Add( tile );
						}

						if (direction.X==-1) //left
						{
								//position.X-=3;
								tile.Clone();
								sprites.Add( tile );
						}
				}

				public void AddTile()
				{
						

				}



		}
}
