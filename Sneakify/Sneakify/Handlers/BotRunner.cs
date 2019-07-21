using Sneakify.Telegram;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sneakify.Handlers
{
    public static class BotRunner
    {
        public static void Run() => new BotRegister().Run();
    }
}
