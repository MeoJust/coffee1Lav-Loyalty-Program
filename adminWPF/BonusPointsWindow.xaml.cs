using Google.Apis.Walletobjects.v1.Data;
using Google.Apis.Walletobjects.v1;
using Google;
using System.Windows;
using System.Windows.Controls;


namespace adminWPF
{
    public partial class BonusPointsWindow : Window
    {
        private WalletobjectsService _service;

        public BonusPointsWindow(WalletobjectsService service) {
            InitializeComponent();
            _service = service;
            LoadLoyaltyObjects();

            idTXT.Clear();
            pointsTXT.Clear();
        }

        private void addPointsBTN_Click(object sender, RoutedEventArgs e) {
            string objectId = idTXT.Text;
            int pointsToAdd;

            if (int.TryParse(pointsTXT.Text, out pointsToAdd))
            {
                BonusPointsManager bonusPointsManager = new BonusPointsManager(_service);
                bonusPointsManager.UpdateLoyaltyPoints(objectId, pointsToAdd);

                pointsTXT.Clear();
                MessageBox.Show("Данные обновлены.", "Есть!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Сообщите пользователю, что введено недопустимое значение
                MessageBox.Show("Цыфры нада!!", "Неверные данные!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void subPointsBTN_Click(object sender, RoutedEventArgs e) {
            string objectId = idTXT.Text;
            int pointsToAdd;

            if (int.TryParse(pointsTXT.Text, out pointsToAdd))
            {
                BonusPointsManager bonusPointsManager = new BonusPointsManager(_service);
                bonusPointsManager.UpdateLoyaltyPoints(objectId, -pointsToAdd);

                pointsTXT.Clear();
                MessageBox.Show("Данные обновлены.", "Есть!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Сообщите пользователю, что введено недопустимое значение
                MessageBox.Show("Цыфры нада!!", "Неверные данные!", MessageBoxButton.OK, MessageBoxImage.Error);
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
