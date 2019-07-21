using Olive;
using Sneakify.Common;
using System;
using System.Collections.Generic;
using System.Text;
using TdLib;
using Tel.Egram.Services.Authentication;
using Tel.Egram.Services.Persistance;
using Tel.Egram.Services.Utils.TdLib;

namespace Sneakify.Telegram
{
    class Auth:BaseClient
    {

        public void Run()
        {
            var strage = new Storage(Extentions.GetExecutionPath().FullName);
            var agent = new Agent(_hub, _dialer);
            var athenticator = new Authenticator(agent, strage);
            athenticator.SetupParameters();
            athenticator.CheckEncryptionKey();


            Console.WriteLine("Enter phone number with country code:");
            var phone = Console.ReadLine();
            athenticator.SetPhoneNumber(phone);

            Console.WriteLine("Enter auth code:");
            var authCode = Console.ReadLine();
            athenticator.CheckCode(authCode, null, null);

            Console.WriteLine("You used a secondary password bitch? Just jokin, please enter it.");
            var pass = Console.ReadLine();
            athenticator.CheckPassword(pass);



        }
    }
}
