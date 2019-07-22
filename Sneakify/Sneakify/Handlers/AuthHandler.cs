using Sneakify.Telegram;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sneakify.Handlers
{
    public static class AuthHandler
    {
        public static void Run() => new Auth().Run();
    }
}
