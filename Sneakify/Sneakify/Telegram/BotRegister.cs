using System;
using System.Collections.Generic;
using System.Text;
using TdLib;

namespace Sneakify.Telegram
{
    class BotRegister : BaseClient
    {
        public void Run()
        {
            _hub.Received += async (sender, data) =>
            {
                Auth.CheckAuth(_dialer, data);

                Console.WriteLine("Fuck");

            };
        }
    }
}
