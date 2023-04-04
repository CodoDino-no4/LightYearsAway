using LYA.Helpers;
using LYA.Networking;
using LYA.Sprites;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;

namespace LYA.Commands
{
		public static class CommandManager
		{
				public interface ICommand
				{
						protected void Execute();

				}

				private static PacketFormer packet=new PacketFormer();

				public static Vector2 PlayerCameraMovement( Astro astro )
				{

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

						packet.ClientSendPacket( "Move", Globals.clientId, "Move Request" );
						return astro.Position;
				}

				public static void PlaceTile( Astro astro, Texture2D tileTex, Deque<BaseSprite> sprites )
				{
						if (InputBindings.Place().Pressed())
						{
								Tile tile = new Tile(tileTex)
								{
										Position = astro.Position
								};
								var place = new PlaceCommand(astro, tile, sprites);
								place.Execute();

								packet.ClientSendPacket( "Place", Globals.clientId, "Place Request" );

						}
				}

				public static void Commands( Astro astro, Texture2D tileTex, Deque<BaseSprite> sprites )
				{
						PlaceTile( astro, tileTex, sprites );
				}
		}
}
