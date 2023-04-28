using LYA.Networking;
using System.Diagnostics;

namespace LYA.Testing
{
		public static class Tests
		{
				/// <summary>
				/// Test assist functions
				/// </summary>
				private static Process proc;
				private static string IP = "192.168.1.101";
				private static string PORT = "11000";
				private static ClientManager clientManager;

				public static async void ServerInit()
				{
						await Task.Run( () =>
						{
								// Setup server exe
								ProcessStartInfo start = new ProcessStartInfo();

								string[] initalPath = AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar);
								string rootPath = "";
								foreach (var dir in initalPath)
								{
										rootPath+=$"{dir}\\";

										if (dir=="SpaceRaftMono")
										{
												break;
										}
								}

								string fullPath = rootPath + "Server\\bin\\Debug\\net7.0-windows\\Server.exe";
								start.FileName=fullPath;

								proc=Process.Start( start );

						} );
				}
		}
}
