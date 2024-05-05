using System.Windows;
using System.IO;


namespace adminWPF
{
    public partial class LoginWindow : Window
    {
        string _loginPath = @"D:\_art\_csharp\coffeOneLoveProj\_keys\admLogin.txt";
        string _login;
        string _passwordPath = @"D:\_art\_csharp\coffeOneLoveProj\_keys\admPassword.txt";
        string _password;

        APISet _apiSet = new APISet("D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json");

        public LoginWindow() {
            InitializeComponent();

            _password = File.ReadAllText(_passwordPath);
            _login = File.ReadAllText(_loginPath);

            loginTXT.Text = "";
            passwordTXT.Text = "";
        }

        private void loginBTN_Click(object sender, RoutedEventArgs e) {
            if (_login == loginTXT.Text && _password == passwordTXT.Text) {
                ChooseActionWindow chooseWindow = new ChooseActionWindow();
                chooseWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Нетьь..");
            }
        }
    }
}
