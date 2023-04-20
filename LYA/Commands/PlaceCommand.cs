using LYA.Helpers;
using LYA.Sprites;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;

namespace LYA.Commands
{
		public class PlaceCommand : CommandManager.ICommand
		{
				private Tile tile;

				private Bag<Tile> sprites;

				public PlaceCommand( Astro astro, Texture2D tileTex, Bag<Tile> sprites ) : base()
				{
						tile = new Tile(tileTex)
						{
								Position = astro.Position,
								Direction = astro.Direction,
						};

						this.sprites=sprites;
				}

				public void Execute()
				{
						bool emptyPos = true;

						foreach (var sprite in sprites)
						{
								if (sprite.Position==tile.Position)
								{
										emptyPos=false;
								}
						}
						if (emptyPos)
						{
								sprites.Add( (Tile) tile.Clone() );
						}				
				}
		}
}
