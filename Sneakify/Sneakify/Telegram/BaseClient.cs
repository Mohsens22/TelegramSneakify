using System;
using System.Collections.Generic;
using System.Text;
using TdLib;

namespace Sneakify.Telegram
{
    class BaseClient
    {
        public Client _client;
        public Hub _hub;
        public Dialer _dialer;
        public BaseClient()
        {
            _client = new Client();
            _hub = new Hub(_client);
            _dialer = new Dialer(_client, _hub);

            
            //TdLib.Client.Log.SetVerbosityLevel(1);

        }
    }
}
