using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Commands
{
		public class MoveRightCommand : CommandManager.ICommand
		{
				public float positionX
				{
						get; set;
				}
				public MoveRightCommand( SpriteHandler astro ) : base()
				{
						positionX=astro.Position.X;
				}

				public void Execute()
				{
						positionX+=3f; ;
				}
		}
}
