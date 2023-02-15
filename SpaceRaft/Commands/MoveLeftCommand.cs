using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Commands
{
		public class MoveLeftCommand : CommandManager.ICommand
		{
				public float positionX
				{
						get; set;
				}
				public MoveLeftCommand( SpriteHandler astro ) : base()
				{
						positionX=astro.Position.X;
				}

				public void Execute()
				{
						positionX-=3f;
				}
		}
}
