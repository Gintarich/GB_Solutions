using System;

namespace CommandFactory.Commands
{
    internal class HelloWorldCommand : IGBCommand
    {
        public void Run()
        {
            Console.WriteLine("Hello World");
        }
    }
}
