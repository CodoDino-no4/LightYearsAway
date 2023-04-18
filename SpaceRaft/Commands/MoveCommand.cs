using LYA.Helpers;
using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Commands
{
		public class MoveCommand : CommandManager.ICommand
		{
				public Astro astro;

				public MoveCommand( Astro astro ) : base()
				{
						this.astro=astro;
				}

				public void Execute()
				{
						if (InputBindings.Up().Held())
						{
								astro.Position.Y-=3f;
								astro.Direction.Y=1;
						}

						if (InputBindings.Down().Held())
						{
								astro.Position.Y+=3f;
								astro.Direction.Y=-1;
						}

						if (InputBindings.Left().Held())
						{
								astro.Position.X-=3f;
								astro.Direction.X=-1;
						}

						if (InputBindings.Right().Held())
						{
								astro.Position.X+=3f;
								astro.Direction.X=1;
						}
				}
		}
}
