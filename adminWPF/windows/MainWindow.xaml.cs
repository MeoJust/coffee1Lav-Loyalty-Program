using Google.Apis.Walletobjects.v1.Data;
using Google.Apis.Walletobjects.v1;
using Google;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace adminWPF.windows
{
    public partial class MainWindow : Window
    {
        private WalletobjectsService _service;

        public MainWindow(WalletobjectsService service)
        {
            InitializeComponent();
            _service = service;
            LoadLoyaltyObjects();

            idTXT.Text = "Выберите карту из списка";
        }

        //Вывод списка объектов
        private void LoadLoyaltyObjects()
        {
            string classId = "3388000000022315715.coffeOneLav";
            IList<LoyaltyObject> loyaltyObjects = GetAllLoyaltyObjects(classId);
            usersLV.ItemsSource = loyaltyObjects;
        }

        //Получение списка объектов
        public IList<LoyaltyObject> GetAllLoyaltyObjects(string classId)
        {
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

        private void exitBTN_Click(object sender, EventArgs e) {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
