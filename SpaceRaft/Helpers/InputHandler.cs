using LYA.Commands;
using LYA.Sprites;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace LYA.Helpers
{
		public class InputHandler
		{
				public InputHandler()
				{
				}

				public Vector2 PlayerCameraMovement( Astro astro )
				{

						if (InputBindings.Up().Held())
						{
								MoveUpCommand moveUp = new MoveUpCommand(astro);
								moveUp.Execute();
								astro.Position.Y=moveUp.position;
								astro.direction=moveUp.direction;
						}

						if (InputBindings.Down().Held())
						{
								MoveDownCommand moveDown = new MoveDownCommand(astro);
								moveDown.Execute();
								astro.Position.Y=moveDown.position;
								astro.direction=moveDown.direction;
						}

						if (InputBindings.Left().Held())
						{
								MoveLeftCommand moveLeft = new MoveLeftCommand(astro);
								moveLeft.Execute();
								astro.Position.X=moveLeft.position;
								astro.direction=moveLeft.direction;
						}

						if (InputBindings.Right().Held())
						{
								MoveRightCommand moveRight = new MoveRightCommand(astro);
								moveRight.Execute();
								astro.Position.X=moveRight.position;
								astro.direction=moveRight.direction;
						}

						return astro.Position;
				}

				public void PlaceTile(Astro astro)
				{
						if (InputBindings.Place().Pressed())
						{
								PlaceCommand place = new PlaceCommand(astro);
								place.Execute();

						}
				}
		}
}
