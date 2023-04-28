using LYA.Sprites;

namespace LYA.Commands
{
		/// <summary>
		/// Handles move up
		/// </summary>
		public class MoveUp : CommandManager.ICommand
		{
				public Astro astro;
				public MoveUp( Astro astro ) : base()
				{
						this.astro=astro;
				}

				public void Execute()
				{
						astro.Position.Y-=3f;
						astro.Direction.Y=1;
				}
		}
}
