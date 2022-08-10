using System;

namespace SpaceRaft
{
		public static class Program
		{
				[STAThread]
				static void Main()
				{
						using (var game = new SpaceRaft())
								game.Run();
				}
		}
}
