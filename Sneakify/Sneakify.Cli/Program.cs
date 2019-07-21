using ManyConsole;
using Sneakify.Common;
using System;

namespace Sneakify.Cli
{
    class Program
    {
        static int Main(string[] args)
        {
            var result = RegisterCommands(args);
#if DEBUG
            WaitForExit();
#endif
            return result;
        }

        private static int RegisterCommands(string[] args)
        {
            var commands = ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);

        }

        private static void WaitForExit()
        {
            Console.WriteLine("Press enter key to exit");
            Console.ReadLine();
        }




    }
}
