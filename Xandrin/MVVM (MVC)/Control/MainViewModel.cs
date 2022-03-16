using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xandrin.Core;

namespace Xandrin.MVVM__MVC_.Control
{
    class MainViewModel : ObservableObject
    {

        private bool gtasaLoaded = false;

        public HomeViewModel HomeVM { get; set;  }
        public RelayCommand HomeViewCommand { get; set; }

        public CreditsViewModel CreditsVM { get; set; }
        public RelayCommand CreditsViewCommand { get; set; }

        public InfoViewModel InfoVM { get; set; }
        public RelayCommand InfoViewCommand { get; set; }

        public delegate bool ViewChanged(object currentview);
        public ViewChanged OnViewChanged = null;

        private object _currentView;
        private object _previousView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public bool GtasaLoaded { get => gtasaLoaded; set => gtasaLoaded = value; }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel(this);
            CreditsVM = new CreditsViewModel(this);
            InfoVM = new InfoViewModel(this);
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                _previousView = _currentView;
                CurrentView = HomeVM;
                if(OnViewChanged != null)
                {
                    if (OnViewChanged(CurrentView) == false)
                    {
                        CurrentView = HomeVM;
                    }
                }
            });

            CreditsViewCommand = new RelayCommand(o =>
            {
                _previousView = _currentView;
                CurrentView = CreditsVM;
                if (OnViewChanged != null)
                {
                    if (OnViewChanged(CurrentView) == false)
                    {
                        CurrentView = HomeVM;
                    }
                }
            });

            InfoViewCommand = new RelayCommand(o =>
            {
                _previousView = _currentView;
                CurrentView = InfoVM;
                if (OnViewChanged != null)
                {
                    if (OnViewChanged(CurrentView) == false)
                    {
                        CurrentView = HomeVM;
                    }
                }
            });
        }
    }
}
