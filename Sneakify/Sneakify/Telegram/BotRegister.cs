using Sneakify.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TdLib;
using Tel.Egram.Services.Authentication;
using Tel.Egram.Services.Messaging.Messages;
using Tel.Egram.Services.Messaging.Notifications;
using Tel.Egram.Services.Persistance;
using Tel.Egram.Services.Utils.Reactive;
using Tel.Egram.Services.Utils.TdLib;
using Sneakify.Mitsuku;
using System.Threading;

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
            var sender = new MessageSender(_agent);



            var notify = new NotificationSource(_agent);
            var notif = notify.MessagesNotifications();

            notif.Accept(notifications =>
            {
                var mg = notifications.Message.Content as TdApi.MessageContent.MessageText;

                var rxt = Taker.Talk(mg.Text.Text);
                Thread.Sleep(5000);
                var returnMsg = new TdApi.InputMessageContent.InputMessageText { Text = new TdApi.FormattedText { Text = rxt } };
                sender.SendMessage(notifications.Chat, returnMsg);
                
            });


            _hub.Start();


        }

       
    }
}
