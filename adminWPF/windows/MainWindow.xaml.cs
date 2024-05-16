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
        ExitView _exitView;

        public MainWindow(WalletobjectsService service) {
            InitializeComponent();
            _service = service;
            LoadLoyaltyObjects();

            idTXT.Text = "Выберите карту из списка";

            _bonusView = new BonusView();
            _bonusView.AddPointsCommand = new RelayCommand(param => AddPoints());
            _bonusView.SubPointsCommand = new RelayCommand(param => RemovePoints());

            _bonusView.AddPointsAction = AddPoints;
            _bonusView.SubPointsAction = RemovePoints;

            _notifiView = new NotifiView();
            _notifiView.NotifiSendAction = SendMessage;

            _exitView = new ExitView();

            contentControl.Content = _bonusView;
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void AddPoints() {
            IdTXTCheck();

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
            IdTXTCheck();

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
                MessageBox.Show("Используйте числа!", "Неверные данные!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Отправка уведомления
        public void SendMessage() {
            IdTXTCheck();

            //Вызов подключения к Google Wallet API
            APISet apiSet = new APISet("D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json");
            NotifiSender notifiSender = new NotifiSender(apiSet.WalletService);

            System.DateTime? startDateCheck = _notifiView.startDatePicker.SelectedDate;
            System.DateTime? endDateCheck = _notifiView.endDatePicker.SelectedDate;

            if(startDateCheck == null || endDateCheck == null)
            {
                MessageBox.Show("Укажите дату начала и оканчания показа уведомления!", "Неверные данные!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(_notifiView.headerTXT.Text == "" || _notifiView.bodyTXT.Text == "")
            {
                MessageBox.Show("Заполните заголовок и текст уведомления!", "Неверные данные!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            System.DateTime startDate = startDateCheck.Value.Date + new TimeSpan(0, 0, 1);
            System.DateTime endDate = endDateCheck.Value.Date + new TimeSpan(23, 59, 59);

            if (endDate < System.DateTime.Now)
            {
                MessageBox.Show("Дата окончания показа уведомления не может быть в прошлом!", "Неверные данные!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (startDate > endDate)
            {
                MessageBox.Show("Дата окончания показа уведомления не может быть раньше даты начала!", "Неверные данные!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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
                        Date = startDate.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'")
                    },
                    End = new Google.Apis.Walletobjects.v1.Data.DateTime
                    {
                        Date = endDate.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'")
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

        private void IdTXTCheck() {
            if (idTXT.Text == "Выберите карту из списка")
            {
                MessageBox.Show("Карта не выбрана!");
                return;
            }
        }

        private void bonusBTN_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = _bonusView;
            lableTXT.Text = "Бонусы";
            contentTXT.Text = "начисление и снятие бонусов";
        }

        private void notifiBTN_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = _notifiView;
            lableTXT.Text = "Уведомления";
            contentTXT.Text = "отправка уведомлений";
        }

        private void exitBTN_Click(object sender, RoutedEventArgs e) {
            contentControl.Content = _exitView;
            lableTXT.Text = "Выход";
            contentTXT.Text = "";
        }
    }
}
