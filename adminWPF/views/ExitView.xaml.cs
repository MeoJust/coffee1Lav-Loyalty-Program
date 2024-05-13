using System.Windows.Controls;
using System.Windows;


namespace adminWPF.views
{
    public partial class ExitView : UserControl
    {
        public ExitView() {
            InitializeComponent();
        }

        void exitBTN_Click(object sender, System.Windows.RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
    }
}
