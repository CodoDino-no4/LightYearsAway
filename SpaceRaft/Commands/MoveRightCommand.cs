using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Commands
{
		public class MoveRightCommand : CommandManager.ICommand
		{
				public Vector2 Position;
				public MoveRightCommand( SpriteHandler astro ) : base()
				{
						Position=astro.Position;
				}

				public Vector2 Execute()
				{
						Position.X+=3f; ;

						return Position;
				}
		}
}
