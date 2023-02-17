using LYA.Sprites;

namespace LYA.Commands
{
		public class MoveRightCommand : CommandManager.ICommand
		{
				public float position;

				public int direction;
				public MoveRightCommand( Astro astro ) : base()
				{
						position=astro.Position.X;
						direction=1;
				}

				public void Execute()
				{
						position+=3f;
				}
		}
}
