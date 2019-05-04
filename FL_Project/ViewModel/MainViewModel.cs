using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FL_Project.Model;
using FL_Project.Skins;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace FL_Project.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        Model.FallLocation newFL;
        Model.FallLocation selsectedFL;
        Model.AccurateFallLocation selsectedAFL;
        ObservableCollection<Model.FallLocation> listFL;
        ObservableCollection<Model.AccurateFallLocation> listAFL;
        ObservableCollection<FallsLocationGroup> listOfGruops;

        private ViewModelBase _currentViewModel;
        private CallCenterViewModle _callCenterViewModel;
        private AnalyzeViewModle _analyzeViewModel;
        private MapViewModle _mapViewModel;

        public ICommand CallCenterViewCommand { get; private set; }
        public ICommand AnalyzeViewCommand { get; private set; }
        public ICommand AddNewFallCommand { get; private set; }


        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public MapViewModle MapViewModel
        {
            get
            {
                return _mapViewModel;
            }
      /*      set
            {
                if (_mapViewModel == value)
                    return;
                _mapViewModel = value;
                RaisePropertyChanged("MapViewModel");
            }*/
        }

        public ObservableCollection<FallLocation> ListFL
        {
            get => listFL;
            set
            {
                listFL = value;
                RaisePropertyChanged("ListFL");
            }
        }
        public AccurateFallLocation SelsectedAFL
        {
            get => selsectedAFL; set
            {
                selsectedAFL = value;
                RaisePropertyChanged("SelsectedAFL");
            }
        }
        public FallLocation SelsectedFL
        {
            get => selsectedFL; set
            {
                selsectedFL = value;
                RaisePropertyChanged("SelsectedFL");
            }
        }
        public FallLocation NewFL
        {
            get => newFL; set
            {
                newFL = value;
                RaisePropertyChanged("NewFL");
            }
        }
        public ObservableCollection<AccurateFallLocation> ListAFL
        {
            get => listAFL;
            set
            {
                Set<ObservableCollection<AccurateFallLocation>>(() => this.ListAFL, ref listAFL, value);
            }
         
        }

        public ObservableCollection<FallsLocationGroup> ListOfGruops
        {
             get  =>  listOfGruops;
            set
            {
                Set<ObservableCollection<FallsLocationGroup>>(() => this.ListOfGruops, ref listOfGruops, value);
            }
        }


        public ICommand HideAllSimpleFallLocationCommand { get; private set; }
        public ICommand HideAllEstimateFallLocationCommand { get; private set; }
        public ICommand HideAllAccurateFallLocationCommand { get; private set; }
        public ICommand LimitDatesCommand { get; private set; }




        public MainViewModel()
        {
            _callCenterViewModel = new CallCenterViewModle();
            
            _analyzeViewModel = new AnalyzeViewModle();
            _currentViewModel = _callCenterViewModel;

            _mapViewModel = new MapViewModle();


        CallCenterViewCommand = new RelayCommand(ExecuteCallCenterViewCommand);
            AnalyzeViewCommand = new RelayCommand(ExecuteAnalyzeViewCommand);
            HideAllSimpleFallLocationCommand = new RelayCommand(HideAllSimpleFallLocationMethod);


            // Console.WriteLine("FROM call center vm1: " + _callCenterViewModel.ListOfGruops.GetHashCode());
        }

        private void HideAllSimpleFallLocationMethod()
        {
         /*   //TODO: add boolean from selector
            bool hide = true;
            foreach (var group in ListOfGruops)
            {
                foreach (var fl in group.FallsLocationlist)
                {
                    fl.Visability = hide;
                }

            }
*/
        }

        private void ExecuteCallCenterViewCommand()
        {
            CurrentViewModel = _callCenterViewModel;
        }

        private void ExecuteAnalyzeViewCommand()
        {
            CurrentViewModel = _analyzeViewModel;

        }
        






       


    }
    /*
    private readonly IDataService _dataService;

    /// <summary>
    /// The <see cref="WelcomeTitle" /> property's name.
    /// </summary>
    public const string WelcomeTitlePropertyName = "WelcomeTitle";

    private string _welcomeTitle = string.Empty;

    /// <summary>
    /// Gets the WelcomeTitle property.
    /// Changes to that property's value raise the PropertyChanged event. 
    /// </summary>
    public string WelcomeTitle
    {
        get
        {
            return _welcomeTitle;
        }
        set
        {
            Set(ref _welcomeTitle, value);
        }
    }

    /// <summary>
    /// Initializes a new instance of the MainViewModel class.
    /// </summary>
    public MainViewModel(IDataService dataService)
    {
        _dataService = dataService;
        _dataService.GetData(
            (item, error) =>
            {
                if (error != null)
                {
                    // Report error here
                    return;
                }

                WelcomeTitle = item.Title;
            });
    }

    ////public override void Cleanup()
    ////{
    ////    // Clean up if needed

    ////    base.Cleanup();
    ////}
}*/
}