using System.Collections.Generic;

namespace CommandFactory.Commands
{
    public class CommandFactoryComponent
    {
        private readonly List<IGBCommand> _commands = new List<IGBCommand>();

        public void Initialize()
        {
            _commands.Add(new HelloWorldCommand());
            _commands.Add(new CreateTrussCommand());
            _commands.Add(new ReadKeyCommand());
        }

        public void RunCommands()
        {
            foreach (var command in _commands)
            {
                command.Run();
            }
        }
    }
}
