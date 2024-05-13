using adminWPF.core;
using System.Windows.Controls;
using System.Windows.Input;

namespace adminWPF.views
{
    public partial class BonusView : UserControl
    {
        public ICommand AddPointsCommand { get; set; }
        public ICommand SubPointsCommand { get; set; }

        public Action AddPointsAction { get; set; }
        public Action SubPointsAction { get; set; }

        public BonusView() {
            InitializeComponent();

            // Инициализируйте команды
            AddPointsCommand = new RelayCommand(AddPoints);
            SubPointsCommand = new RelayCommand(SubPoints);
            // Установите DataContext для привязки команд в XAML
            this.DataContext = this;
        }

        private void AddPoints(object obj) {
            // Здесь вы можете вызвать метод AddPoints из MainWindow или ViewModel
            // Например, используя делегат или сообщая через MVVM
        }

        private void SubPoints(object obj) {
            // Аналогично вызовите метод SubPoints
        }

        private void addBTN_Click(object sender, System.Windows.RoutedEventArgs e) {
            AddPointsAction.Invoke();
        }

        private void subBTN_Click(object sender, System.Windows.RoutedEventArgs e) {
            SubPointsAction.Invoke();
        }
    }
}
