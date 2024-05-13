using Google.Apis.Walletobjects.v1.Data;
using Google.Apis.Walletobjects.v1;
using Google;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using adminWPF.views;
using adminWPF.core;

namespace adminWPF.windows
{
    public partial class MainWindow : Window
    {
        private WalletobjectsService _service;

        BonusView _bonusView;
        NotifiView _notifiView;

        public MainWindow(WalletobjectsService service) {
            InitializeComponent();
            _service = service;
            LoadLoyaltyObjects();

            idTXT.Text = "Выберите карту из списка";

            _bonusView = new BonusView();
            // Здесь вы устанавливаете методы из MainWindow как делегаты для команд в BonusView
            _bonusView.AddPointsCommand = new RelayCommand(param => this.AddPoints());
            _bonusView.SubPointsCommand = new RelayCommand(param => this.RemovePoints());

            _bonusView.AddPointsAction = AddPoints;
            _bonusView.SubPointsAction = RemovePoints;

            _notifiView = new NotifiView();
            _notifiView.NotifiSendAction = SendMessage;

            contentControl.Content = _bonusView; // Установите контент, если это еще не сделано
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

        //private void exitBTN_Click(object sender, EventArgs e) {
        //    Application.Current.Shutdown();
        //}

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void AddPoints() {
            string objectId = idTXT.Text;
            int pointsToAdd;

            if (int.TryParse(_bonusView.pointsTXT.Text, out pointsToAdd))
            {
                BonusPointsManager bonusPointsManager = new BonusPointsManager(_service);
                bonusPointsManager.UpdateLoyaltyPoints(objectId, pointsToAdd);

                _bonusView.pointsTXT.Clear();
                MessageBox.Show("Данные обновлены.", "Есть!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Сообщите пользователю, что введено недопустимое значение
                MessageBox.Show("Используйте числа!", "Неверные данные!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemovePoints() {
            string objectId = idTXT.Text;
            int pointsToAdd;

            if (int.TryParse(_bonusView.pointsTXT.Text, out pointsToAdd))
            {
                BonusPointsManager bonusPointsManager = new BonusPointsManager(_service);
                bonusPointsManager.UpdateLoyaltyPoints(objectId, -pointsToAdd);

                _bonusView.pointsTXT.Clear();
                MessageBox.Show("Данные обновлены.", "Есть!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Сообщите пользователю, что введено недопустимое значение
                MessageBox.Show("Используйте числа!", "Неверные данные!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Отправка уведомления
        public void SendMessage() {
            //Вызов подключения к Google Wallet API
            APISet apiSet = new APISet("D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json");
            NotifiSender notifiSender = new NotifiSender(apiSet.WalletService);

            System.DateTime? startDate = _notifiView.startDatePicker.SelectedDate;
            System.DateTime? endDate = _notifiView.endDatePicker.SelectedDate;

            //Настройка сообщения
            Message message = new Message
            {
                Header = _notifiView.headerTXT.Text,
                Body = _notifiView.bodyTXT.Text,
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
                _notifiView.headerTXT.Clear();
                _notifiView.bodyTXT.Clear();
            }
            catch (Exception ex)
            {
                //Вывод сообщения оь ошибке
                MessageBox.Show($"Ошибка при отправке сообщения: {ex.Message}", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
