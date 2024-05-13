using adminWPF.core;

namespace adminWPF.vm
{
    internal class MainVM : ObservableObject
    {
        public RelayCommand BonusVC { get; set; }
        public RelayCommand NotifiVC { get; set; }
        public RelayCommand ExitVC { get; set; }

        public BonusesVM BonusesVM { get; set; }
        public NotifiVM NotifiVM { get; set; }
        public ExitVM ExitVM { get; set; }

        object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainVM() { 
            BonusesVM = new BonusesVM();
            NotifiVM = new NotifiVM();
            ExitVM = new ExitVM();

            CurrentView = BonusesVM;

            BonusVC = new RelayCommand(o => { 
                CurrentView = BonusesVM;
            });

            NotifiVC = new RelayCommand(o => {
                CurrentView = NotifiVM;
            });

            ExitVC = new RelayCommand(o => {
                CurrentView = ExitVM;
            });
        }
    }
}
