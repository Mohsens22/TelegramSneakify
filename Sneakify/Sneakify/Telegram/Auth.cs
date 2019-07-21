using Olive;
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
                            ApiId = ConfigurationFactory.Get("telegram:appId").To<int>(), // your API ID
                            ApiHash = ConfigurationFactory.Get("telegram:appHash"), // your API HASH
                            SystemLanguageCode = "en",
                            DeviceModel = "Windows", //System.Runtime.InteropServices.RuntimeInformation.OSDescription,
                            SystemVersion = "10.0",
                            ApplicationVersion = Constants.Version,
                            EnableStorageOptimizer = true,
                            IgnoreFileNames = false
                        }
                    });
                    break;

                case TdApi.AuthorizationState.AuthorizationStateWaitEncryptionKey _:
                    await _dialer.ExecuteAsync(new TdApi.CheckDatabaseEncryptionKey());
                    break;

                case TdApi.AuthorizationState.AuthorizationStateWaitPhoneNumber _:
                    Console.WriteLine("Enter phone number with country code:");
                    var phone = Console.ReadLine();
                    await _dialer.ExecuteAsync(new TdApi.SetAuthenticationPhoneNumber
                    {
                        PhoneNumber = phone // your phone
                    });
                    break;

                case TdApi.AuthorizationState.AuthorizationStateWaitCode _:
                    Console.WriteLine("Enter auth code:");
                    var authCode = Console.ReadLine();
                    await _dialer.ExecuteAsync(new TdApi.CheckAuthenticationCode
                    {
                        Code = authCode, // your auth code
                    });
                    break;

                case TdApi.AuthorizationState.AuthorizationStateWaitPassword _:
                    Console.WriteLine("You used a secondary password bitch? Just jokin, please enter it.");
                    var pass = Console.ReadLine();
                    await _dialer.ExecuteAsync(new TdApi.CheckAuthenticationPassword
                    {
                        Password = pass // your password
                    });
                    break;

                case TdApi.AuthorizationState.AuthorizationStateReady _:
                    // now authenticated. do something here
                    break;
            }
        }
    }
}
