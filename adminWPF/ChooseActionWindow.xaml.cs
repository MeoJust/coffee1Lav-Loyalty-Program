using System.Windows;

namespace adminWPF
{
    public partial class ChooseActionWindow : Window
    {
        APISet _apiSet = new APISet("D:\\_art\\_csharp\\coffeOneLoveProj\\_keys\\saKey.json");

        public ChooseActionWindow() {
            InitializeComponent();
        }

        private void messBTN_Click(object sender, RoutedEventArgs e) {
            NotifiWindow notifiWindow = new NotifiWindow(_apiSet.WalletService);
            notifiWindow.Show();
            Close();
        }

        private void pointsBTN_Click(object sender, RoutedEventArgs e) {
            BonusPointsWindow bpWindow = new BonusPointsWindow(_apiSet.WalletService);
            bpWindow.Show();
            Close();
        }

        private void haliavaBTN_Click(object sender, RoutedEventArgs e) {
            FreeCupWindow freeCupWindow = new FreeCupWindow(_apiSet.WalletService);
            freeCupWindow.Show();
            Close();
        }
    }
}
