using LYA.Helpers;
using LYA.Sprites;
using LYA.Sprites.Cloneables;
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
						tile=new Tile( tileTex )
						{
								Position=astro.Position,
								Direction=astro.Direction,
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
										break;
								}
						}

						if (emptyPos)
						{
								sprites.Add( (Tile) tile.Clone() );
						}

						// Send Packet
						if (Globals.IsMulti)
						{
								Globals.Packet.ClientSendPacket( "Place", Globals.ClientId, (int) tile.Position.X, (int) tile.Position.Y, "" );
						}
				}
		}
}
