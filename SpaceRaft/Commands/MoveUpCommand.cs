using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Commands
{
		public class MoveUpCommand : CommandManager.ICommand
		{
				private Vector2 position;
				public MoveUpCommand( SpriteHandler astro ) : base()
				{
						position=astro.Position;
				}

				public Vector2 Execute()
				{
						position.Y-=3f;

						return position;
				}
		}
}
