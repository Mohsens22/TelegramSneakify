using ManyConsole;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sneakify.Cli.Commands
{
    class RunCommand : ConsoleCommand
    {
        public RunCommand()
        {
            IsCommand("Run", "Run chatbot");
        }
        public override int Run(string[] remainingArguments)
        {
            throw new NotImplementedException();
        }
    }
}
