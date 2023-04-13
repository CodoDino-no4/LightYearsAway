using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYA.Testing
{
		public static class RunGame_Testing
		{
				[STAThread]
				public static void Main()
				{
						using (var game = new LYA())
								game.Run();
				}
		}
}
