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

            AddPointsCommand = new RelayCommand(AddPoints);
            SubPointsCommand = new RelayCommand(SubPoints);

            this.DataContext = this;
        }

        private void AddPoints(object obj) {

        }

        private void SubPoints(object obj) {
            
        }

        private void addBTN_Click(object sender, System.Windows.RoutedEventArgs e) {
            AddPointsAction.Invoke();
        }

        private void subBTN_Click(object sender, System.Windows.RoutedEventArgs e) {
            SubPointsAction.Invoke();
        }
    }
}
