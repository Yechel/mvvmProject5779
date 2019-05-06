using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using FL_Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Syncfusion.Windows.Shared;
using GalaSoft.MvvmLight.Messaging;

namespace FL_Project.ViewModel
{
   public class CallCenterViewModle: ViewModelBase
    {
        private ObservableCollection<FallsLocationGroup> listOfGruops;
        private ObservableCollection<FallLocation> listFL;
        public String newFallLocationAdress = "";
        public int newFallLocationNumberOfFalls;
        public DateTime newFallLocationDate;
        public DateTime newFallLocationTime;
        public FallLocation selsectedFL;
        public static bool selectedVisible;
        public String picPath;
    

        public bool SelectedVisible
        {
            get => selectedVisible; set
            {
                selectedVisible = value;
                RaisePropertyChanged("SelectedVisible");
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

        

        public ObservableCollection<FallsLocationGroup> ListOfGruops
        {
            get => listOfGruops;
            set
            {
                Set<ObservableCollection<FallsLocationGroup>>(() => this.ListOfGruops, ref listOfGruops, value);
            }
        }
        public string NewFallLocationAdress
        {
            get => newFallLocationAdress;
            set
            {
                newFallLocationAdress = value;
                RaisePropertyChanged("NewFallLocationAdress");
            }
        }
        public int NewFallLocationNumberOfFalls
        {
            get => newFallLocationNumberOfFalls; set
            {
                newFallLocationNumberOfFalls = value;
                RaisePropertyChanged("NewFallLocationNumberOfFalls");
            }
        }
        public DateTime NewFallLocationTime
        {
            get => newFallLocationTime; set
            {
                newFallLocationTime = value;
                RaisePropertyChanged("NewFallLocationTime");
            }
        }
        public DateTime NewFallLocationDate
        {
            get => newFallLocationDate; set
            {
                newFallLocationDate = value;
                RaisePropertyChanged("NewFallLocationDate");
            }
        }

        public string PicPath
        {
            get => picPath;
            set
            {
                picPath = value;
                RaisePropertyChanged("PicPath");
            }
        }




        public ICommand AddNewFallCommand { get; private set; }
        public ICommand AddNewAccurateFallCommand { get; private set; }
        public ObservableCollection<FallLocation> ListFL { get => listFL; set => listFL = value; }

        


        public CallCenterViewModle()
        {
            AddNewFallCommand = new RelayCommand(AddNewFallLocationMethod);
            AddNewAccurateFallCommand = new RelayCommand<object>((obj) => AddNewAccurateFLMethod(obj));
            ListOfGruops = Model.FallLocationService.GetData(new Action<bool>((x) => { }));
          
            
        }

      /*  public CallCenterViewModle(ref ObservableCollection<FallLocation> listFL)
        {
            this.listFL = listFL;
        }*/

        private void AddNewFallLocationMethod()
        {
            if (NewFallLocationNumberOfFalls == 0)
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ilegal accurate number of falls."));
            }
            else if (NewFallLocationDate == null || NewFallLocationTime == null)
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ilegal date."));

            }
            else if (NewFallLocationAdress == null || NewFallLocationAdress == "")
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Ilegal adress."));
            }
            else
            {
                DateTime dateTime = new DateTime(NewFallLocationDate.Year, NewFallLocationDate.Month, NewFallLocationDate.Day, NewFallLocationTime.Hour, NewFallLocationTime.Minute, 00);
                FallLocation fl = new FallLocation(NewFallLocationAdress, (dateTime).ToString("MM/dd/yyyy hh:mm"), NewFallLocationNumberOfFalls);
                FallLocationService.AddFL(fl);
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("New FL added."));
            }
        }



        private void AddNewAccurateFLMethod(object obj)
        {
            string str = obj as String;
            DateTime dateTime = new DateTime(NewFallLocationDate.Year, NewFallLocationDate.Month, NewFallLocationDate.Day, NewFallLocationTime.Hour, NewFallLocationTime.Minute, 00);
            Action succsess = () => { Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Acuurate FL added.")); };
            Action fail = () => { Messenger.Default.Send<NotificationMessage>(new NotificationMessage("Acuurate FL fail to add.")); };
            AccurateFallLocation afl = new AccurateFallLocation((dateTime).ToString("MM/dd/yyyy hh:mm"), PicPath,succsess,fail);
            FallLocationService.AddAFL(afl);
        }

      



    }
}
