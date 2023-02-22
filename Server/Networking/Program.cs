using System;

namespace LYA.Networking
{
		public static class Program
		{
				[STAThread]
				private static void Main()
				{
						using (var server = new BasicServer())
								server.Connect();
				}
		}
}
