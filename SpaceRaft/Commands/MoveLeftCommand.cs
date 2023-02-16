using LYA.Sprites;

namespace LYA.Commands
{
		public class MoveLeftCommand : CommandManager.ICommand
		{
				public float position;

				public int direction;
				public MoveLeftCommand( Astro astro ) : base()
				{
						position=astro.Position.X;
						direction=1;
				}

				public void Execute()
				{
						position-=3f;
				}
		}
}
