namespace LYA
{
		public static class Program
		{
				[STAThread]
				private static void Main()
				{

						using (var game = new LYA())
								game.Run();
				}
		}
}
