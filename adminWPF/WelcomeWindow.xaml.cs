using System.Windows;

namespace adminWPF
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow() {
            InitializeComponent();
        }

        private void loginBTN_Click(object sender, RoutedEventArgs e) {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}
