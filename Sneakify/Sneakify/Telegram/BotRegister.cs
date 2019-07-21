using Sneakify.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TdLib;
using Tel.Egram.Services.Authentication;
using Tel.Egram.Services.Messaging.Notifications;
using Tel.Egram.Services.Persistance;
using Tel.Egram.Services.Utils.Reactive;
using Tel.Egram.Services.Utils.TdLib;

namespace Sneakify.Telegram
{
    class BotRegister : BaseClient
    {
        public void Run()
        {

            var authent = new Authenticator(_agent,_storage);
            authent.SetupParameters();
            var state = authent.ObserveState();
            authent.CheckEncryptionKey();

            

            var notify = new NotificationSource(_agent);
            var notif = notify.MessagesNotifications();

            notif.Accept(notifications =>
            {
                var mg = notifications.Message.Content as TdApi.MessageContent.MessageText;
                Console.WriteLine(mg.Text.Text);
            });


            _hub.Start();


        }

       
    }
}
