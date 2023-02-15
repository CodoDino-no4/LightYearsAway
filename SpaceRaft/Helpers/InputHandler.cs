using LYA.Commands;
using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Helpers
{
		public class InputHandler
		{
				public InputHandler()
				{
				}

				public Vector2 PlayerCameraMovement( SpriteHandler astro )
				{
						if (InputBindings.Up().Held())
						{
								MoveUpCommand moveUp = new MoveUpCommand(astro);
								moveUp.Execute();
								astro.Position.Y=moveUp.positionY;
						}

						if (InputBindings.Down().Held())
						{
								MoveDownCommand moveDown = new MoveDownCommand(astro);
								moveDown.Execute();
								astro.Position.Y=moveDown.positionY;
						}

						if (InputBindings.Left().Held())
						{
								MoveLeftCommand moveLeft = new MoveLeftCommand(astro);
								moveLeft.Execute();
								astro.Position.X=moveLeft.positionX;
						}

						if (InputBindings.Right().Held())
						{
								MoveRightCommand moveRight = new MoveRightCommand(astro);
								moveRight.Execute();
								astro.Position.X=moveRight.positionX;
						}

						return astro.Position;
				}

				public void PlaceTile()
				{
						if (InputBindings.Place().Pressed())
						{
								PlaceCommand place = new PlaceCommand();
								place.Execute();

						}
				}
		}
}
