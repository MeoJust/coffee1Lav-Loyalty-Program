using Google;
using Google.Apis.Walletobjects.v1;
using Google.Apis.Walletobjects.v1.Data;
using System.Windows;
using System.Windows.Controls;
using System;

namespace adminWPF
{
    //Логика формы MainWindow
    public partial class NotifiWindow : Window
    {
        private WalletobjectsService _service;

        //Подключение к форме
        public NotifiWindow(WalletobjectsService service) {
            InitializeComponent();
            _service = service;
            LoadLoyaltyObjects();

            idTXT.Text = "Выберите карту из списка или введите вручную";
            headerTXT.Text = "Заголовок уведомления";
            bodyTXT.Text = "Текст уведомления";
        }

        //Обработка нажатия на кнопку
        private void sendBTN_Click(object sender, RoutedEventArgs e) {
            SendMessage();
        }

        private void backBTN_Click(object sender, RoutedEventArgs e) {
            ChooseActionWindow chooseWindow = new ChooseActionWindow();
            chooseWindow.Show();
            Close();
        }

        //Отправка уведомления
        public void SendMessage() {
            //Вызов подключения к Google Wallet API
            APISet apiSet = new APISet("D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json");
            NotifiSender notifiSender = new NotifiSender(apiSet.WalletService);

            System.DateTime? startDate = startDatePicker.SelectedDate;
            System.DateTime? endDate = endDatePicker.SelectedDate;

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
                        Date = startDate.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'")
                    },
                    End = new Google.Apis.Walletobjects.v1.Data.DateTime
                    {
                        Date = endDate.Value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'")
                    }
                }
            };

            //Отправка уведомления по ID
            notifiSender.AddMessageToLoyaltyObject(idTXT.Text, message);

            try
            {
                //Вывод сообщения об успехе
                MessageBox.Show("Сообщение успешно отправлено!", "Есть!", MessageBoxButton.OK, MessageBoxImage.Information);
                //Сброс текстовых полей
                headerTXT.Clear();
                bodyTXT.Clear();
            }
            catch (Exception ex)
            {
                //Вывод сообщения оь ошибке
                MessageBox.Show($"Ошибка при отправке сообщения: {ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Вывод списка объектов
        private void LoadLoyaltyObjects() {
            string classId = "3388000000022315715.coffeOneLav";
            IList<LoyaltyObject> loyaltyObjects = GetAllLoyaltyObjects(classId);
            usersLV.ItemsSource = loyaltyObjects;
        }

        //Получение списка объектов
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

        //Возможность выбора обьекта карты из списка
        private void usersLV_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (usersLV.SelectedItem is LoyaltyObject selectedLoyaltyObject)
            {
                idTXT.Text = selectedLoyaltyObject.Id;
            }
        }
    }
}