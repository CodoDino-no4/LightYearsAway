using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Commands
{
    public static class CommandManager
    {
        public interface ICommand
        {
            protected void Execute();
        }
    }
}