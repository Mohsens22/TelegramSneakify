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
            //_hub.Received += async (sender, data) =>
            //{
            //    Auth.CheckAuth(_dialer, data);

            //    Console.WriteLine("Fuck");

            //};
            //_hub.Start();
            _dialer.Execute(new TdApi.AddProxy
            {
                Type = new TdApi.ProxyType.ProxyTypeMtproto() { Secret = "dd5b23c518418daef5b54f0bc86dcefc1a" },
                Server = "34.245.97.49",
                Port = 23816

            });
            _dialer.SetClient();
            _dialer.AddPhone();
            _dialer.AddCode();
            _dialer.AddPassword();
        }
    }
}
