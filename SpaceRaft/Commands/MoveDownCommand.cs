using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Commands
{
		public class MoveDownCommand : CommandManager.ICommand
		{
				public Vector2 Position;
				public MoveDownCommand( SpriteHandler astro ) : base()
				{
						Position=astro.Position;
				}

				public Vector2 Execute()
				{
						Position.Y+=3f;

						return Position;
				}
		}
}
