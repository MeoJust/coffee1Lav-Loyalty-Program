using System.Windows.Controls;


namespace adminWPF.views
{
    public partial class NotifiView : UserControl
    {
        public Action NotifiSendAction { get; set; }

        public NotifiView() {
            InitializeComponent();
        }

        private void sendBTN_Click(object sender, System.Windows.RoutedEventArgs e) {
            NotifiSendAction.Invoke();
        }
    }
}
