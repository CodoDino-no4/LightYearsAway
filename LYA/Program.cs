namespace LYA
{
		public static class Program
		{
				[STAThread]
				public static void Main()
				{
						using (var game = new LYA(false))
								game.Run();
				}
		}
}
