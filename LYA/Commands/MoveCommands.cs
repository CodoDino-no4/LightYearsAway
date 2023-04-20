using LYA.Helpers;
using LYA.Sprites;

namespace LYA.Commands
{
		public class MoveCommands : CommandManager.ICommand
		{
				public Astro astro;
				private MoveUp moveUp;
				private MoveDown moveDown;
				private MoveRight moveRight;
				private MoveLeft moveLeft;

				public MoveCommands( Astro astro ) : base()
				{
						this.astro=astro;
						moveUp = new MoveUp(astro);
						moveDown = new MoveDown(astro);
						moveLeft = new MoveLeft(astro);
						moveRight = new MoveRight(astro);
				}

				public void Execute()
				{
						if (InputBindings.Up().Held())
						{
								moveUp.Execute();
						}

						if (InputBindings.Down().Held())
						{
								moveDown.Execute();
						}

						if (InputBindings.Left().Held())
						{
								moveLeft.Execute();
						}

						if (InputBindings.Right().Held())
						{
								moveRight.Execute();
						}
				}
		}
}
