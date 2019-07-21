using ManyConsole;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sneakify.Cli.Commands
{
    class OtherCommand : ConsoleCommand
    {
        public OtherCommand()
        {
            IsCommand("Other", "Other shit");
        }
        public override int Run(string[] remainingArguments)
        {
            throw new NotImplementedException();
        }
    }
}
