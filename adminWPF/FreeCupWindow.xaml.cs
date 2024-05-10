using Google.Apis.Walletobjects.v1.Data;
using Google.Apis.Walletobjects.v1;
using Google;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace adminWPF
{
    public partial class FreeCupWindow : Window
    {
        private WalletobjectsService _service;
        private APISet _apiSet;
        private ReplasibleMessages _replasibleMessages;

        public FreeCupWindow(WalletobjectsService service) {
            InitializeComponent();
            _service=service;

            _apiSet = new APISet("D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json");
            _replasibleMessages = new ReplasibleMessages(_apiSet.WalletService);

            LoadLoyaltyObjects();

            idTXT.Text = "Выберите карту из списка или введите вручную";
            headerTXT.Text = "";
            bodyTXT.Text = "";
            MessageBodyContent();
        }

        private void sendBTN_Click(object sender, RoutedEventArgs e) {
            SendReplasibleMessage();
        }

        private void backBTN_Click(object sender, RoutedEventArgs e) {
            ChooseActionWindow chooseWindow = new ChooseActionWindow();
            chooseWindow.Show();
            Close();
        }

        public void SendReplasibleMessage() {
            //Вызов подключения к Google Wallet API
            //APISet apiSet = new APISet("D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json");
            //ReplasibleMessages replasibleMessages = new ReplasibleMessages(apiSet.WalletService);

            //Настройка сообщения
            Message message = new Message
            {
                //для замены сообщения headerTXT должен быть таким же
                Header = headerTXT.Text,
                Body = bodyTXT.Text,
                DisplayInterval = new TimeInterval
                {
                    Start = new Google.Apis.Walletobjects.v1.Data.DateTime
                    {
                        Date = new System.DateTime(2023, 06, 06, 23, 59, 59, DateTimeKind.Utc).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'")
                    },
                    End = new Google.Apis.Walletobjects.v1.Data.DateTime
                    {
                        //Сообщение висит, пока его не заменят
                        Date = new System.DateTime(3000, 01, 01, 23, 59, 59, DateTimeKind.Utc).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'")
                    }
                }
            };

            //Отправка уведомления по ID
            _replasibleMessages.ReplaceMessageInLoyaltyObject(idTXT.Text, message, headerTXT.Text);

            try
            {
                //Вывод сообщения об успехе
                MessageBox.Show("Сообщение успешно отправлено!", "Есть!", MessageBoxButton.OK, MessageBoxImage.Information);
                MessageBodyContent();
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
                // Получаем тело текущего сообщения для выбранного объекта карты.
                UpdateBodyTextForSelectedLoyaltyObject(selectedLoyaltyObject);
            }
        }

        // Этот метод обновляет bodyTXT.Text на основе выбранного объекта карты.
        private void UpdateBodyTextForSelectedLoyaltyObject(LoyaltyObject loyaltyObject) {
            // Найти текущее сообщение на основе какого-то критерия, например, по заголовку.
            Message currentMessage = loyaltyObject.Messages?.FirstOrDefault(m => m.Header == headerTXT.Text);
            // Проверяем, найдено ли сообщение.
            if (currentMessage != null)
            {
                // Обновляем bodyTXT.Text на основе текущего сообщения.
                bodyTXT.Text = currentMessage.Body;
            }
            else
            {
                // Если текущее сообщение не найдено, сбрасываем bodyTXT.Text.
                bodyTXT.Text = "";
            }
        }

        private void MessageBodyContent() {
            // Выбранный объект карты.
            LoyaltyObject selectedLoyaltyObject = usersLV.SelectedItem as LoyaltyObject;
            if (selectedLoyaltyObject != null)
            {
                // Получить тело текущего сообщения для выбранного объекта карты.
                string messageBody = _replasibleMessages.GetMessageBody(selectedLoyaltyObject.Id, "Акция! Каждая шестая чашка кофе будет бесплатной!");

                if (!string.IsNullOrEmpty(messageBody)) // Убедитесь, что messageBody не пусто
                {
                    // Теперь bodyTXT.Text будет обновляться напрямую из messageBody.
                    if (messageBody == "☕☕☕☕☕")
                    {
                        headerTXT.Text = "Акция! Каждая шестая чашка кофе будет бесплатной!";
                        bodyTXT.Text = "☕☕☕☕";
                    }
                    else if (messageBody == "☕☕☕☕")
                    {
                        headerTXT.Text = "Акция! Каждая шестая чашка кофе будет бесплатной!";
                        bodyTXT.Text = "☕☕☕";
                    }
                    else if (messageBody == "☕☕☕")
                    {
                        headerTXT.Text = "Акция! Каждая шестая чашка кофе будет бесплатной!";
                        bodyTXT.Text = "☕☕";
                    }
                    else if (messageBody == "☕☕")
                    {
                        headerTXT.Text = "Акция! Каждая шестая чашка кофе будет бесплатной!";
                        bodyTXT.Text = "☕";
                    }
                    else if (messageBody == "☕")
                    {
                        headerTXT.Text = "Акция! Каждая шестая чашка кофе будет бесплатной!";
                        bodyTXT.Text = "Поздравляем! Следующая чашка за наш счет!";
                    }
                    else
                    {
                        headerTXT.Text = "Акция! Каждая шестая чашка кофе будет бесплатной!";
                        bodyTXT.Text = messageBody;
                    }
                }
                else
                {
                    // Если messageBody пусто, установите значение по умолчанию или покажите сообщение об ошибке.
                    headerTXT.Text = "Акция! Каждая шестая чашка кофе будет бесплатной!";
                    bodyTXT.Text = "☕☕☕☕☕";
                }
            }
        }
    }
}
