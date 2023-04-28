using LYA.Sprites;

namespace LYA.Commands
{
		/// <summary>
		/// Handles move down
		/// </summary>
		public class MoveDown : CommandManager.ICommand
		{
				public Astro astro;
				public MoveDown( Astro astro ) : base()
				{
						this.astro=astro;
				}

				public void Execute()
				{
						astro.Position.Y+=3f;
						astro.Direction.Y=-1;
				}
		}
}
