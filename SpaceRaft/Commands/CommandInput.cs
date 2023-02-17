using LYA.Helpers;
using LYA.Sprites;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace LYA.Commands
{
		public class CommandInput
		{
				public CommandInput()
				{
				}

				public Vector2 PlayerCameraMovement( Astro astro )
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

						Debug.WriteLine( astro.Direction );
						return astro.Position;
				}

				public void PlaceTile( Astro astro )
				{
						if (InputBindings.Place().Pressed())
						{
								var place = new PlaceCommand(astro);
								place.Execute();

						}
				}
		}
}
