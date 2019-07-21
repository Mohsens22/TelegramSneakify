using Sneakify.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TdLib;
using Tel.Egram.Services.Authentication;
using Tel.Egram.Services.Persistance;

namespace Sneakify.Telegram
{
    class BotRegister : BaseClient
    {
        public void Run()
        {
            var strage = new Storage(Extentions.GetExecutionPath().FullName);
            var athenticator = new Authenticator(;


        }

        private void RegisterEvent()
        {
            
            
        }
    }
}
