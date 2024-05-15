using adminWPF.core;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace adminWPF.windows
{
    public partial class LoginWindow : Window
    {
        APISet _apiSet = new APISet("D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json");

        public LoginWindow() {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void loginBTN_Click(object sender, RoutedEventArgs e) {
            Debug.WriteLine("Before reading loginTXT.Text");
            string login = loginTXT.Text;
            Debug.WriteLine("After reading loginTXT.Text: " + login);
            string password = passwordTXT.Text; 

            Debug.WriteLine(login);
            Debug.WriteLine(password);

            string folderPath = @"D:\_art\_csharp\coffeOneLoveProj\_keys\_admins";

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }

            foreach (string file in Directory.GetFiles(folderPath, "*.json"))
            {
                string jsonContent = File.ReadAllText(file);
                Admin admin = JsonConvert.DeserializeObject<Admin>(jsonContent);

                if (admin.Login == login && admin.Password == password)
                {
                    MainWindow mainWindow = new MainWindow(_apiSet.WalletService);
                    mainWindow.adminTXT.Text = admin.Name;
                    mainWindow.Show();
                    Close();
                    return;
                }
            }
            MessageBox.Show("Неверный логин или пароль");
        }

        private void exitBTN_Click(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}
