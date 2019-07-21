using Sneakify.Common;
using System;
using System.Collections.Generic;
using System.Text;
using TdLib;

namespace Sneakify.Telegram
{
    static class Auth
    {
        public static async void CheckAuth(Dialer _dialer, TdApi.Object data)
        {


            switch (data)
            {
                case TdApi.AuthorizationState.AuthorizationStateWaitTdlibParameters _:
                    await _dialer.ExecuteAsync(new TdApi.SetTdlibParameters
                    {
                        Parameters = new TdApi.TdlibParameters
                        {
                            UseTestDc = false,
                            DatabaseDirectory = Extentions.GetExecutionPath().FullName, // directory here
                            FilesDirectory = Extentions.GetExecutionPath().FullName, // directory here
                            UseFileDatabase = true,
                            UseChatInfoDatabase = true,
                            UseMessageDatabase = true,
                            UseSecretChats = true,
                            ApiId = 123456, // your API ID
                            ApiHash = "hash", // your API HASH
                            SystemLanguageCode = "en",
                            DeviceModel = "Windows",
                            SystemVersion = "0.1",
                            ApplicationVersion = "0.1",
                            EnableStorageOptimizer = true,
                            IgnoreFileNames = false
                        }
                    });
                    break;

                case TdApi.AuthorizationState.AuthorizationStateWaitEncryptionKey _:
                    await _dialer.ExecuteAsync(new TdApi.CheckDatabaseEncryptionKey());
                    break;

                case TdApi.AuthorizationState.AuthorizationStateWaitPhoneNumber _:
                    await _dialer.ExecuteAsync(new TdApi.SetAuthenticationPhoneNumber
                    {
                        PhoneNumber = "+01234567789" // your phone
                    });
                    break;

                case TdApi.AuthorizationState.AuthorizationStateWaitCode _:
                    await _dialer.ExecuteAsync(new TdApi.CheckAuthenticationCode
                    {
                        Code = "123456", // your auth code
                    });
                    break;

                case TdApi.AuthorizationState.AuthorizationStateWaitPassword _:
                    await _dialer.ExecuteAsync(new TdApi.CheckAuthenticationPassword
                    {
                        Password = "P@$$w0rd" // your password
                    });
                    break;

                case TdApi.AuthorizationState.AuthorizationStateReady _:
                    // now authenticated. do something here
                    break;
            }
        }
    }
}
