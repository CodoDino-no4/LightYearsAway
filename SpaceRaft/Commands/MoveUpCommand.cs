using LYA.Sprites;

namespace LYA.Commands
{
		public class MoveUpCommand : CommandManager.ICommand
		{
				public float position;

				public int direction;

				public MoveUpCommand( Astro astro ) : base()
				{
						position=astro.Position.Y;
						direction=1;
				}

				public void Execute()
				{
						position-=3f;
				}

		}
}
