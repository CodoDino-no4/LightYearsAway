using LYA.Sprites;

namespace LYA.Commands
{
		public class MoveDownCommand : CommandManager.ICommand
		{
				public float position;

				public int direction;

				public MoveDownCommand( Astro astro ) : base()
				{
						position=astro.Position.Y;
						direction=-1;
				}

				public void Execute()
				{
						position+=3f;

				}
		}
}
