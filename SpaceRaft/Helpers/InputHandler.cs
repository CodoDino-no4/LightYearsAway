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
								astro.Position=moveUp.Execute();
						}

						if (InputBindings.Down().Held())
						{
								MoveDownCommand moveDown = new MoveDownCommand(astro);
								astro.Position=moveDown.Execute();
						}

						if (InputBindings.Left().Held())
						{
								MoveLeftCommand moveLeft = new MoveLeftCommand(astro);
								astro.Position=moveLeft.Execute();
						}

						if (InputBindings.Right().Held())
						{
								MoveRightCommand moveRight = new MoveRightCommand(astro);
								astro.Position=moveRight.Execute();
						}

						return astro.Position;
				}

				//public Vector2 PlaceTile()
				//{
				//		if (InputBindings.Place().Pressed()) 
				//		{ 

				//		}
				//}
		}
}
