using Google;
using Google.Apis.Walletobjects.v1;
using Google.Apis.Walletobjects.v1.Data;
using System.Windows;

namespace adminWPF
{
    //Логика формы MainWindow
    public partial class MainWindow : Window
    {
        private WalletobjectsService _service;

        //Подключение к форме
        public MainWindow(WalletobjectsService service) {
            InitializeComponent();
            _service = service;
            LoadLoyaltyObjects();

        }

        //Обработка нажатия на кнопку
        private void sendBTN_Click(object sender, RoutedEventArgs e) {
            SendMessage();
        }

        //Отправка уведомления
        public void SendMessage() {
            //Вызов подключения к Google Wallet API
            APISet apiSet = new APISet("D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json");
            NotifiSender notifiSender = new NotifiSender(apiSet.WalletService);

            //Настройка сообщения
            Message message = new Message
            {
                Header = headerTXT.Text,
                Body = bodyTXT.Text,
                //Временной интервал показа уведомления
                DisplayInterval = new TimeInterval
                {
                    Start = new Google.Apis.Walletobjects.v1.Data.DateTime
                    {
                        Date = new System.DateTime(2023, 4, 1, 0, 0, 0, DateTimeKind.Utc).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'")
                    },
                    End = new Google.Apis.Walletobjects.v1.Data.DateTime
                    {
                        Date = new System.DateTime(2024, 5, 1, 23, 59, 59, DateTimeKind.Utc).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'")
                    }
                }
            };

            //Отправка уведомления по ID
            notifiSender.AddMessageToLoyaltyObject(idTXT.Text, message);

            try
            {
                //Вывод сообщения об успехе
                MessageBox.Show("Сообщение успешно отправлено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                //Вывод сообщения оь ошибке
                MessageBox.Show($"Ошибка при отправке сообщения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadLoyaltyObjects() {
            // Используйте существующий метод для получения списка объектов карт лояльности
            string classId = "3388000000022315715.coffeOneLav";
            IList<LoyaltyObject> loyaltyObjects = GetAllLoyaltyObjects(classId);
            usersLV.ItemsSource = loyaltyObjects;
        }

        public IList<LoyaltyObject> GetAllLoyaltyObjects(string classId) {
            try
            {
                // Создание запроса на получение списка объектов определенного класса карты
                LoyaltyobjectResource.ListRequest request = _service.Loyaltyobject.List();
                request.ClassId = classId;
                LoyaltyObjectListResponse response = request.Execute();
                IList<LoyaltyObject> loyaltyObjects = response.Resources;
                foreach (var lo in loyaltyObjects)
                {
                    Console.WriteLine(lo.Id);
                }
                return loyaltyObjects;
            }
            catch (GoogleApiException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}