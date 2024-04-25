using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Walletobjects.v1;

namespace aspApp
{
    //Подключение к Google API с помощью ключа сервисного аккаунта
    public class Authorization
    {
        public string KeyFilePath { get; }
        public ServiceAccountCredential Credentials { get; private set; }
        public WalletobjectsService WalletService { get; private set; }

        public Authorization(string keyFilePath) {
            KeyFilePath = keyFilePath;
            InitializeService();
        }

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
