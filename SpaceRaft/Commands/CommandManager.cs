using LYA.Sprites;
using Microsoft.Xna.Framework;

namespace LYA.Commands
{
		public class CommandManager
		{
				public interface ICommand
				{
						protected void Execute();

				}
				private static CommandManager Instance
				{
						get; set;
				}

				public CommandManager()
				{
						Instance=this;
				}
		}
}
