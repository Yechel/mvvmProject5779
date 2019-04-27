using FL_Project.Model;
using FL_Project.ShapesFL;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using Syncfusion.UI.Xaml.Maps;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FL_Project.ViewModel
{

    public class MapViewModle : ViewModelBase
    {

        public MapAnnotations currentMapAnnotations;
        ObservableCollection<MapAnnotations> dataMapAnnotations;


        public MapAnnotations CurrentMapAnnotations
        {
            get => currentMapAnnotations; set
            {
                currentMapAnnotations = value;
                RaisePropertyChanged("CurrentMapAnnotations");
            }
        }

        public ObservableCollection<MapAnnotations> DataMapAnnotations
        {
            get => dataMapAnnotations; set
            {
                dataMapAnnotations = value;
                RaisePropertyChanged("DataMapAnnotations");
            }
        }


        public MapViewModle()
        {
            //    var serviceData = FallLocationService.GetData((a)=> {  });

            DataMapAnnotations = new ObservableCollection<MapAnnotations>();
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                SetAnnotations();
            });
            var data = FallLocationService.GetData((a) => { });
            //register to new group that added
           data.CollectionChanged += MapViewModle_CollectionChanged;
            foreach (var group in data)
            {
                //register to new Estimate accurate and reported fallLocation that added
                group.EstimateFallLocation.PropertyChanged += EstimateFallLocation_PropertyChanged;
          //      group.AccurateFallLocation.PropertyChanged += AccurateFallLocation_PropertyChanged;
                group.FallsLocationlist.CollectionChanged += FallsLocationlist_CollectionChanged;
            }
        }

        private void FallsLocationlist_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                var newItem = (FallLocation)e.NewItems;
                var gruopItem = (FallsLocationGroup)sender;
                foreach (var item in gruopItem.FallsLocationlist)
                {
                    if (newItem.ID.Equals(item.ID))
                    {
                        MapAnnotations annotations = new MapAnnotations();
                        var location = FallLocationService.getLocation(item.Adress);
                        annotations.Latitude = location[0];
                        annotations.Longitude = location[1];
                        var ellipse = EllipseFL.getEllipse(gruopItem.GroupColor);
                        annotations.AnnotationSymbol = ellipse;
                        annotations.AnnotationSymbol.Uid = "Report_" + gruopItem.GruopId;
                        DataMapAnnotations.Add(annotations);
                        break;
                    }
                }
            });
        }

        private void AccurateFallLocation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                FallsLocationGroup group = (FallsLocationGroup)sender;
                if (group.AccurateFallLocation != null) { 
                MapAnnotations annotations = new MapAnnotations();
                var location = FallLocationService.getLocation(group.AccurateFallLocation.Adress);
                annotations.Latitude = location[0];
                annotations.Longitude = location[1];
                var star = StarFL.getStarShape(group.GroupColor);
                annotations.AnnotationSymbol = star;
                annotations.AnnotationSymbol.Uid = "Accurate_" + group.GruopId;
                DataMapAnnotations.Add(annotations);
                }
            });
        }

        private void EstimateFallLocation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                FallsLocationGroup group = (FallsLocationGroup)sender;
                if (group.EstimateFallLocation != null)
                {
                    MapAnnotations annotations = new MapAnnotations();
                    var location = FallLocationService.getLocation(group.EstimateFallLocation.Adress);
                    annotations.Latitude = location[0];
                    annotations.Longitude = location[1];
                    var ellipse = EllipseFL.getEllipseWithBorder(group.GroupColor);
                    annotations.AnnotationSymbol = ellipse;
                    annotations.AnnotationSymbol.Uid = "Estimate_" + group.GruopId;
                    DataMapAnnotations.Add(annotations);
                } });
        }

        private void MapViewModle_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var gruopItem = (FallsLocationGroup) e.NewItems;
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                
                if (gruopItem.FallsLocationlist != null)
                {
                    foreach (var item in gruopItem.FallsLocationlist)
                    {
                        MapAnnotations annotations = new MapAnnotations();
                        var location = FallLocationService.getLocation(item.Adress);
                        annotations.Latitude = location[0];
                        annotations.Longitude = location[1];
                        var ellipse = EllipseFL.getEllipse(gruopItem.GroupColor);
                        annotations.AnnotationSymbol = ellipse;
                        annotations.AnnotationSymbol.Uid = "Report_" + gruopItem.GruopId;
                        DataMapAnnotations.Add(annotations);
                    }
                }
                if (gruopItem.EstimateFallLocation != null)
                {
                    MapAnnotations annotations = new MapAnnotations();
                    var location = FallLocationService.getLocation(gruopItem.EstimateFallLocation.Adress);
                    annotations.Latitude = location[0];
                    annotations.Longitude = location[1];
                    var ellipse = EllipseFL.getEllipseWithBorder(gruopItem.GroupColor);
                    annotations.AnnotationSymbol = ellipse;
                    annotations.AnnotationSymbol.Uid = "Estimate_" + gruopItem.GruopId;
                    DataMapAnnotations.Add(annotations);
                }
                if (gruopItem.AccurateFallLocation != null)
                {
                    MapAnnotations annotations = new MapAnnotations();
                    var location = FallLocationService.getLocation(gruopItem.AccurateFallLocation.Adress);
                    annotations.Latitude = location[0];
                    annotations.Longitude = location[1];
                    var star = StarFL.getStarShape(gruopItem.GroupColor);
                    annotations.AnnotationSymbol = star;
                    annotations.AnnotationSymbol.Uid = "Accurate_" + gruopItem.GruopId;
                    DataMapAnnotations.Add(annotations);
                }
            });
        }

        internal void SetAnnotations()
        {
            ObservableCollection<FallsLocationGroup> data = FallLocationService.GetData(new Action<bool>((a) => { }));

            foreach (var group in data)
            {
                if (group.FallsLocationlist != null)
                {
                    foreach (var item in group.FallsLocationlist)
                    {
                        MapAnnotations annotations = new MapAnnotations();
                        var location = FallLocationService.getLocation(item.Adress);
                        annotations.Latitude = location[0];
                        annotations.Longitude = location[1];
                        var ellipse = EllipseFL.getEllipse(group.GroupColor);
                        annotations.AnnotationSymbol = ellipse;
                        annotations.AnnotationSymbol.Uid = "Report_"+group.GruopId;
                        DataMapAnnotations.Add(annotations);
                    }
                }
                if (group.EstimateFallLocation != null)
                {
                    MapAnnotations annotations = new MapAnnotations();
                    var location = FallLocationService.getLocation(group.EstimateFallLocation.Adress);
                    annotations.Latitude = location[0];
                    annotations.Longitude = location[1];
                    var ellipse = EllipseFL.getEllipseWithBorder(group.GroupColor);
                    annotations.AnnotationSymbol = ellipse;
                    annotations.AnnotationSymbol.Uid = "Estimate_" + group.GruopId;
                    DataMapAnnotations.Add(annotations);
                }
                if (group.AccurateFallLocation != null)
                {
                    MapAnnotations annotations = new MapAnnotations();
                    var location = FallLocationService.getLocation(group.AccurateFallLocation.Adress);
                    annotations.Latitude = location[0];
                    annotations.Longitude = location[1];
                    var star = StarFL.getStarShape(group.GroupColor);
                    annotations.AnnotationSymbol = star;
                    annotations.AnnotationSymbol.Uid = "Accurate_" + group.GruopId;
                    DataMapAnnotations.Add(annotations);
                }
            }
        }

        private class FallsLocation
        {
        }
    }//MapViewModle
}//namespace

