using System;

namespace LYA
{
		public static class Program
		{
				[STAThread]
				static void Main()
				{
						using (var game = new LYA())
								game.Run();
				}
		}
}
