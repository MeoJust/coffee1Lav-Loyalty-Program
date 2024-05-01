using Google.Apis.Walletobjects.v1.Data;
using System.Windows;

namespace adminWPF
{
    //Логика формы MainWindow
    public partial class MainWindow : Window
    {
        //Подключение к форме
        public MainWindow() {
            InitializeComponent();
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
    }
}