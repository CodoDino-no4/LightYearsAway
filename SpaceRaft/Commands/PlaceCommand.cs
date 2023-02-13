using LYA.Helpers;
using Microsoft.Xna.Framework;

namespace LYA.Commands
{
		public class PlaceCommand : CommandManager.ICommand
		{
				//needs the position to be placed in
				//needs astros direction

				private Vector2 placePosition;
				public PlaceCommand() : base()
				{
				}

				public Vector2 Execute()
				{
								return Vector2.Zero;
				}



		}
}
