using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Commands
{
		public class MoveUpCommand : CommandManager.ICommand
		{
				public float positionY
				{
						get; set;
				}
				public MoveUpCommand( SpriteHandler astro ) : base()
				{
						positionY=astro.Position.Y;
				}

				public void Execute()
				{
						positionY-=3f;
				}

		}
}
