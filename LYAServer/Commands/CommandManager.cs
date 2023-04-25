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