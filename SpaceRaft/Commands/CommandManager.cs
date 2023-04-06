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
						Vector2 tmpPosition = astro.Position;

						if (InputBindings.Up().Held())
						{
								var moveUp = new MoveUpCommand(astro);
								moveUp.Execute();
								astro.Position.Y=moveUp.position;
								astro.Direction.Y=moveUp.direction;
						}

						if (InputBindings.Down().Held())
						{
								var moveDown = new MoveDownCommand(astro);
								moveDown.Execute();
								astro.Position.Y=moveDown.position;
								astro.Direction.Y=moveDown.direction;
						}

						if (InputBindings.Left().Held())
						{
								var moveLeft = new MoveLeftCommand(astro);
								moveLeft.Execute();
								astro.Position.X=moveLeft.position;
								astro.Direction.X=moveLeft.direction;
						}

						if (InputBindings.Right().Held())
						{
								var moveRight = new MoveRightCommand(astro);
								moveRight.Execute();
								astro.Position.X=moveRight.position;
								astro.Direction.X=moveRight.direction;
						}

						if (tmpPosition!=astro.Position)
						{
								Globals.Packet.ClientSendPacket( "Move", Globals.ClientId, astro.Position.ToString() );
						}

						return astro.Position;
				}

				public static void PlaceTile( Astro astro, Texture2D tileTex, Bag<BaseSprite> sprites)
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

				public static void Commands( Astro astro, Texture2D tileTex, Bag<BaseSprite> sprites)
				{
						PlaceTile( astro, tileTex, sprites);
				}
		}
}
