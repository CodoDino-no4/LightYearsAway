using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Commands
{
		public class MoveLeftCommand : CommandManager.ICommand
		{
				private Vector2 Position;
				public MoveLeftCommand( SpriteHandler astro ) : base()
				{
						Position=astro.Position;
				}

				public Vector2 Execute()
				{
						Position.X-=3f;

						return Position;
				}
		}
}
