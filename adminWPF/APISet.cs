using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Walletobjects.v1;

namespace adminWPF
{
    //Подключение к Google API с помощью ключа сервисного аккаунта
    public class APISet
    {
        //Ключ сервисного аккаунта
        public string KeyFilePath { get; }
        //Сервисный аккаунт
        public ServiceAccountCredential Credentials { get; private set; }
        public WalletobjectsService WalletService { get; private set; }

        //Подключение к Google Wallet API, используя конкретный ключ
        public APISet(string keyFilePath) {
            KeyFilePath = keyFilePath;
            InitializeService();
        }
        //Подключение к Google Wallet API
        private void InitializeService() {
            Credentials = (ServiceAccountCredential)GoogleCredential
                .FromFile(KeyFilePath)
                .CreateScoped(new List<string>
                {
                WalletobjectsService.ScopeConstants.WalletObjectIssuer
                })
                .UnderlyingCredential;

            WalletService = new WalletobjectsService(
                new BaseClientService.Initializer()
                {
                    HttpClientInitializer = Credentials
                });
        }
    }
}

