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

        private void loginBTN_Click(object sender, RoutedEventArgs e) {
            MainWindow mw = new MainWindow(_apiSet.WalletService);
            mw.Show();
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
