namespace LYAServer.Commands
{
    /// <summary>
    /// Command base class
    /// </summary>
    public static class CommandManager
    {
        public interface ICommand
        {
            protected void Execute();
        }
    }
}