using Olive;
using Sneakify.Common;
using System;
using System.Reactive.Linq;
using System.Text;
using TdLib;
using Tel.Egram.Services.Persistance;
using Tel.Egram.Services.Utils.TdLib;

namespace Tel.Egram.Services.Authentication
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAgent _agent;
        private readonly IStorage _storage;

        public Authenticator(
            IAgent agent,
            IStorage storage
            )
        {
            _agent = agent;
            _storage = storage;
        }

        public IObservable<TdApi.AuthorizationState> ObserveState()
        {   
            return _agent.Updates.OfType<TdApi.Update.UpdateAuthorizationState>()
                .Select(update => update.AuthorizationState);
        }
        
        public IObservable<TdApi.Ok> SetupParameters()
        {
            var id = ConfigurationFactory.Get("telegram:appId").To<int>();
            var hash = ConfigurationFactory.Get("telegram:appHash");

            return _agent.Execute(new TdApi.SetTdlibParameters
            {
                Parameters = new TdApi.TdlibParameters
                {
                    UseTestDc = false,
                    DatabaseDirectory = _storage.TdLibDirectory,
                    FilesDirectory = _storage.TdLibDirectory,
                    UseFileDatabase = true,
                    UseChatInfoDatabase = true,
                    UseMessageDatabase = true,
                    UseSecretChats = true,
                    ApiId = id,
                    ApiHash = hash,
                    SystemLanguageCode = "en",
                    DeviceModel = "Mac",
                    SystemVersion = "0.1",
                    ApplicationVersion = "0.1",
                    EnableStorageOptimizer = true,
                    IgnoreFileNames = false
                }
            });
        }

        public IObservable<TdApi.Ok> CheckEncryptionKey()
        {
            var encryptKey = Encoding.ASCII.GetBytes(ConfigurationFactory.Get("telegram:encryptKey"));
            return _agent.Execute(new TdApi.CheckDatabaseEncryptionKey {EncryptionKey= encryptKey });
        }

        public IObservable<TdApi.Ok> SetPhoneNumber(string phoneNumber)
        {
            return _agent.Execute(new TdApi.SetAuthenticationPhoneNumber
                {
                    PhoneNumber = phoneNumber
                });
        }

        public IObservable<TdApi.Ok> CheckCode(string code, string firstName, string lastName)
        {
            return _agent.Execute(new TdApi.CheckAuthenticationCode
                {
                    Code = code,
                    FirstName = firstName,
                    LastName = lastName
                });
        }

        public IObservable<TdApi.Ok> CheckPassword(string password)
        {
            return _agent.Execute(new TdApi.CheckAuthenticationPassword
                {
                    Password = password
                });
        }
    }
}