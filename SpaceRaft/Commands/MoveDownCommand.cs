using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Commands
{
		public class MoveDownCommand : CommandManager.ICommand
		{
				public float positionY
				{
						get; set;
				}

				public MoveDownCommand( SpriteHandler astro ) : base()
				{
						positionY=astro.Position.Y;
				}

				public void Execute()
				{
						positionY +=3f;

				}
		}
}
