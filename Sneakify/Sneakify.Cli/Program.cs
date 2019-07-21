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
            throw new NotImplementedException();
        }

        private static void WaitForExit()
        {
            Console.WriteLine("Press enter key to exit");
            Console.ReadLine();
        }




    }
}
