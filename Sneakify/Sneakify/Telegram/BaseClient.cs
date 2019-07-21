using Sneakify.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TdLib;
using Tel.Egram.Services.Persistance;
using Tel.Egram.Services.Utils.TdLib;

namespace Sneakify.Telegram
{
    class BaseClient
    {
        public Client _client;
        public Hub _hub;
        public Dialer _dialer;
        public Storage _storage;
        public Agent _agent;
        public BaseClient()
        {
            _storage = new Storage(Extentions.GetExecutionPath().FullName);
            Client.Log.SetFilePath(Path.Combine(_storage.LogDirectory, "tdlib.log"));
            Client.Log.SetMaxFileSize(1_000_000); // 1MB
            Client.Log.SetVerbosityLevel(3);
            _client = new Client();
            _hub = new Hub(_client);
            _dialer = new Dialer(_client, _hub);
            _agent = new Agent(_hub, _dialer);

        }
    }
}
