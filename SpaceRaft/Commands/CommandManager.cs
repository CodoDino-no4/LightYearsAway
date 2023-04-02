using LYA.Helpers;
using LYA.Sprites;
using LYA.Sprites.Cloneables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;
using System.Diagnostics;

namespace LYA.Commands
{
		public static class CommandManager
		{
				public interface ICommand
				{
						protected void Execute();

				}

				public static Vector2 PlayerCameraMovement( Astro astro )
				{
						//reset direction??? 
						//astro.Direction= Vector2.Zero;

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

						}
				}

				public static void Commands( Astro astro, Texture2D tileTex, Deque<BaseSprite> sprites )
				{
						PlaceTile( astro, tileTex, sprites );
				}
		}
}
