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
using Olive;

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

            notif.Accept(async notifications =>
            {
                try
                {
                    var mg = notifications.Message.Content as TdApi.MessageContent.MessageText;
                    if (mg.Text.Text.HasValue())
                    {
                        var r = await _dialer.ExecuteAsync(new TdApi.OpenChat { ChatId = notifications.Message.ChatId });

                        var chats = await _dialer.ExecuteAsync(new TdApi.GetChatHistory
                        {
                            ChatId = notifications.Message.ChatId,
                            Limit = 10,
                            Offset = 0,
                            OnlyLocal = false
                        });

                        var result = await _dialer.ExecuteAsync(new TdApi.ViewMessages { ChatId = notifications.Message.ChatId, MessageIds = new[] { notifications.Message.Id } });

                        var rxt = Taker.Talk(mg.Text.Text);

                        var rz = rxt;

                        for (int i = 0; i < 1; i++)
                        {
                            rz += i;

                            var rrr = await _dialer.ExecuteAsync(
                                new TdApi.SendChatAction { ChatId= notifications.Message.ChatId,Action=new TdApi.ChatAction.ChatActionTyping() }
                                );

                           var res = await _dialer.ExecuteAsync(
                                new TdApi.SetChatDraftMessage
                                { ChatId= notifications.Message.ChatId 
                                ,DraftMessage = new TdApi.DraftMessage
                                { InputMessageText=new TdApi.InputMessageContent.InputMessageText
                                {
                                    Text = new TdApi.FormattedText { Text =rz}
                                }
                                }
                                });
                            Thread.Sleep(5000);
                        }
                        var returnMsg = new TdApi.InputMessageContent.InputMessageText { Text = new TdApi.FormattedText { Text = rxt },ClearDraft=true};
                        sender.SendMessage(notifications.Chat, returnMsg);

                        var r1 = await _dialer.ExecuteAsync(new TdApi.CloseChat { ChatId = notifications.Message.ChatId });
                    }
                    
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                
                
            });


            _hub.Start();


        }

       
    }
}
