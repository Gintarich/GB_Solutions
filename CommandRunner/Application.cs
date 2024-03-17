using CommandFactory.Commands;

namespace CommandRunner
{
    public class Application
    {
        private CommandFactoryComponent _commandFactory;
        public void Run()
        {
            _commandFactory = new CommandFactoryComponent();
            _commandFactory.Initialize();
            _commandFactory.RunCommands();
        }
    }
}
