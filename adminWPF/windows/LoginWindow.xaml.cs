using adminWPF.core;
using System.Windows;
using System.Windows.Input;

namespace adminWPF.windows
{
    public partial class LoginWindow : Window
    {
        readonly APISet _apiSet = new APISet("D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json");
        readonly GetAdminData _adminData;

        DragableWindow _dragableWindow = new DragableWindow();
        CloseApp _closeApp;

        public LoginWindow() {
            InitializeComponent();
            _closeApp = new CloseApp();
            _adminData = new GetAdminData(@"D:\_art\_csharp\coffeOneLoveProj\_keys\_admins");
        }

        private void loginBTN_Click(object sender, RoutedEventArgs e) {
            string login = loginTXT.Text;
            string password = passwordTXT.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }

            Admin admin = _adminData.GetData(login, password);

            if (admin != null)
            {
                MainWindow mainWindow = new MainWindow(_apiSet.WalletService, admin);
                mainWindow.adminTXT.Text = admin.Name;
                mainWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _dragableWindow.DragTheWindow(sender, e, this);
            }
        }

        private void exitBTN_Click(object sender, RoutedEventArgs e) {
            _closeApp.Close();
        }
    }
}
