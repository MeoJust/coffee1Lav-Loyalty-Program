using System.Windows.Controls;
using System.Windows;
using adminWPF.core;


namespace adminWPF.views
{
    public partial class ExitView : UserControl
    {
        CloseApp _closeApp;

        public ExitView() {
            InitializeComponent();
            _closeApp = new CloseApp();
        }

        private void exitBTN_Click(object sender, RoutedEventArgs e) {
            _closeApp.Close();
        }
    }
}
