using ManyConsole;
using Sneakify.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sneakify.Cli.Commands
{
    class AuthCommand : ConsoleCommand
    {
        public override int Run(string[] remainingArguments)
        {
            AuthHandler.Run();
            return 1;
        }
    }
}
