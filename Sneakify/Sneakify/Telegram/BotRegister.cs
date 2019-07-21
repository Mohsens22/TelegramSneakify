using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TdLib;

namespace Sneakify.Telegram
{
    class BotRegister : BaseClient
    {
        public void Run()
        {
            RegisterEvent();

        }

        private void RegisterEvent()
        {
            _hub.Received += async (sender, data) =>
            {
                Auth.CheckAuth(_dialer, data);
                if(data is TdApi.Message)
                {

                }
                if (data is TdApi.Ok)
                {
                    // do something
                    
                }
                else if (data is TdApi.Error)
                {
                    // handle error
                }

            };
            _hub.Start();
        }
    }
}
