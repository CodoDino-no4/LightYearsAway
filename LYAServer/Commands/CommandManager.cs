using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LYAServer.Commands
{
    public static class CommandManager
    {
        public interface ICommand
        {
            protected void Execute();
        }
    }
}