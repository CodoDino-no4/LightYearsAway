using LYA.Helpers;
using LYA.Networking;
using LYA.Sprites;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;
using System;
using System.Diagnostics;

namespace LYA.Commands
{
		public static class CommandManager
		{
				public interface ICommand
				{
						protected void Execute();
				}

				public static Vector2 PlayerCameraMovement( Astro astro)
				{
						// Previous Position
						Vector2 tmpPosition = astro.Position;

						// Execute Command
						var move = new MoveCommand(astro);
						move.Execute();

						// Send Packet
						if (tmpPosition!=astro.Position)
						{
								Globals.Packet.ClientSendPacket( "Move", Globals.ClientId, astro.Position.ToString() );
						}

						return astro.Position;
				}

				public static void PlaceTile( Astro astro, Texture2D tileTex, Bag<Tile> sprites)
				{
						bool emptyPos = true;

						if (InputBindings.Place().Pressed())
						{
								Tile tile = new Tile(tileTex)
								{
										Position = astro.Position
								};

								foreach (var sprite in sprites)
								{
										if (sprite.Position==tile.Position)
										{
												emptyPos=false;
										}
								}

								if (emptyPos)
								{
										var place = new PlaceCommand(astro, tile, sprites);
										place.Execute();
										Globals.Packet.ClientSendPacket( "Place", Globals.ClientId, astro.Position.ToString() );
								}
								else {

										Debug.WriteLine( "Tile already placed here" );
								}
						}
				}

				public static void Commands( Astro astro, Texture2D tileTex, Bag<Tile> sprites)
				{
						PlaceTile( astro, tileTex, sprites);
				}
		}
}
