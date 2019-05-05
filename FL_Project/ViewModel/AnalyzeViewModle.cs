using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveCharts;
using FL_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FL_Project.ViewModel
{


    public class AnalyzeViewModle : ViewModelBase
    {
         public Func<ChartPoint, string> PointLabel { get; set; }
        // String str;
        int maxDistanceValue;
        int precentageDistanceValue;
        int currentValue;
        private List<int> listOfDistance;
        public List<Image> listOfImages;

        public int CurrentValue
        {
            get => currentValue; set
            {
                currentValue = value;
                RaisePropertyChanged("CurrentValue");
            }
        }

        public List<Image> ListOfImages
        {
            get => listOfImages; 
        }

        public int MaxDistanceValue
        {
            get => maxDistanceValue; set
            {
                maxDistanceValue = value;
                RaisePropertyChanged("MaxDistanceValue");
            }
        }

        public int PrecentageDistanceValue
        {
            get => precentageDistanceValue; set
            {
                precentageDistanceValue = value;
                RaisePropertyChanged("PrecentageDistanceValue");
            }
        }

        public ICommand UpdateDiagramPresentageCommand { get; private set; }

        public AnalyzeViewModle()
        {
            PointLabel = chartPoint =>
              string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            if (FallLocationService.DataChanged == null)
            {
                FallLocationService.DataChanged = UpdateMaxDistance;
            }
            else { FallLocationService.DataChanged += UpdateMaxDistance; }
           // UpdateMaxDistance();
            UpdateDiagramPresentageCommand = new RelayCommand( UpdateDiagramPresentageMethod);
            listOfImages = new List<Image>();
            foreach (var item in FallLocationService.GetData((a)=> { }))
            {
                setImageToListImage(item);
               
            }
            FallLocationService.GetData((a) => { }).CollectionChanged += AnalyzeViewModle_CollectionChanged;



        }

        private void setImageToListImage(FallsLocationGroup item)
        {
            if (item.AccurateFallLocation != null)
            {
                Image img = new Image();
                var uriSource = new Uri(item.AccurateFallLocation.PicPath);
                img.Source = new BitmapImage(uriSource);
                ListOfImages.Add(img);
            }
        }

        private void AnalyzeViewModle_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            setImageToListImage((FallsLocationGroup)((object[])e.NewItems.SyncRoot)[0]);
        }

        private void UpdateMaxDistance()
        {
            listOfDistance = FallLocationService.getListOfDistance();
            if (listOfDistance == null || listOfDistance.Count == 0)
            { MaxDistanceValue = 0; }
            else { MaxDistanceValue = listOfDistance.Max()+1; }
       
        }

        public void UpdateDiagramPresentageMethod()
        {
            if (listOfDistance.Count == 0)
            { PrecentageDistanceValue = 0;

            }
            else { 
             int i = 0;
             listOfDistance.ForEach((itm) => { if (itm < CurrentValue) i++; });
             PrecentageDistanceValue = (int)((i /(float) listOfDistance.Count) * 100);
            }
        
        }


    }
}
