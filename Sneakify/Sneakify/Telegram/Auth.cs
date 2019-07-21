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
                    SetClient(_dialer);
                    break;

                case TdApi.AuthorizationState.AuthorizationStateWaitEncryptionKey _:
                    await _dialer.ExecuteAsync(new TdApi.CheckDatabaseEncryptionKey());
                    break;

                case TdApi.AuthorizationState.AuthorizationStateWaitPhoneNumber _:
                    AddPhone(_dialer);
                    break;

                case TdApi.AuthorizationState.AuthorizationStateWaitCode _:
                    AddCode(_dialer);
                    break;

                case TdApi.AuthorizationState.AuthorizationStateWaitPassword _:
                    AddPassword(_dialer);
                    break;

                case TdApi.AuthorizationState.AuthorizationStateReady _:
                    // now authenticated. do something here
                    break;
            }
        }
        public static async void SetClient(this Dialer dialer)
        {
            var dir = Extentions.GetExecutionPath().FullName;
            var id = ConfigurationFactory.Get("telegram:appId").To<int>();
            var hash = ConfigurationFactory.Get("telegram:appHash");
            var encryptKey = Encoding.ASCII.GetBytes(ConfigurationFactory.Get("telegram:encryptKey"));

            await dialer.ExecuteAsync(new TdApi.SetTdlibParameters
            {
                Parameters = new TdApi.TdlibParameters
                {
                    UseTestDc = false,
                    DatabaseDirectory = dir, // directory here
                    FilesDirectory = dir, // directory here
                    UseFileDatabase = true,
                    UseChatInfoDatabase = true,
                    UseMessageDatabase = true,
                    UseSecretChats = true,
                    ApiId = id, // your API ID
                    ApiHash = hash, // your API HASH
                    SystemLanguageCode = "en",
                    DeviceModel = "Windows", //System.Runtime.InteropServices.RuntimeInformation.OSDescription,
                    SystemVersion = "10.0",
                    ApplicationVersion = Constants.Version,
                    EnableStorageOptimizer = true,
                    IgnoreFileNames = false

                }
            });

            await dialer.ExecuteAsync(new TdApi.CheckDatabaseEncryptionKey() { EncryptionKey = encryptKey });
            
        }

        public async static void AddPassword(this Dialer dialer)
        {
            Console.WriteLine("You used a secondary password bitch? Just jokin, please enter it.");
            var pass = Console.ReadLine();
            await dialer.ExecuteAsync(new TdApi.CheckAuthenticationPassword
            {
                Password = pass // your password
            });
        }

        public async static void AddCode(this Dialer dialer)
        {
            Console.WriteLine("Enter auth code:");
            var authCode = Console.ReadLine();
            await dialer.ExecuteAsync(new TdApi.CheckAuthenticationCode
            {
                Code = authCode, // your auth code
            });
        }

        public static async void AddPhone(this Dialer dialer)
        {
            Console.WriteLine("Enter phone number with country code:");
            var phone = Console.ReadLine();
            await dialer.ExecuteAsync(new TdApi.SetAuthenticationPhoneNumber
            {
                PhoneNumber = phone // your phone
            });
        }
    }
}
