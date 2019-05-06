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
            SetAnnotations();
            var data = FallLocationService.GetData((a) => { });
            //register to new group that added
           data.CollectionChanged += MapViewModle_CollectionChanged;
            if (DataMapAnnotations != null && DataMapAnnotations.Count > 0)
            {
                CurrentMapAnnotations = DataMapAnnotations[0];
            }
            
            /*    foreach (var group in data)
             {
                 //register to new Estimate accurate and reported fallLocation that added
                 group.EstimateFallLocation.PropertyChanged += EstimateFallLocation_PropertyChanged;
                 //      group.AccurateFallLocation.PropertyChanged += AccurateFallLocation_PropertyChanged;
                 group.FallsLocationlist.CollectionChanged += FallsLocationlist_CollectionChanged;
             }*/
        }

      

        private void AccurateFallLocation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            FallsLocationGroup group = (FallsLocationGroup)sender;
            if (group.AccurateFallLocation != null)
            {
                SetAnnotation("Accurate", group.AccurateFallLocation.Adress, group.GroupColor, group.GruopId);
            }
        }

        //when estimate FL changing if it is new, then new annotation will be appear, else the exist annotaion will change the lat and long.
        /*        private void EstimateFallLocation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                    FallsLocationGroup group = (FallsLocationGroup)sender;
                    if (group.EstimateFallLocation == null)
                    {
                        SetAnnotation("Estimate", group.AccurateFallLocation.Adress, group.GroupColor, group.GruopId);
                    }
                    else
                    {
                        var location = FallLocationService.getLocation(group.EstimateFallLocation.Adress);
                        foreach (var item in DataMapAnnotations)
                        {
                            if (item.AnnotationSymbol.Uid.Equals("Estimate_" + group.GruopId))
                            {
                                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                                {
                                    item.Latitude = location[0];
                                    item.Longitude = location[1];
                                });
                            }
                        }

                    }
                }*/

        private void MapViewModle_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var group = (FallsLocationGroup)((object[])e.NewItems.SyncRoot)[0];
            SetGroupAnnotations(group);
        }




        /*  internal void SetAnnotations()
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
          }*/


        internal void SetAnnotations()
        {
            ObservableCollection<FallsLocationGroup> data = FallLocationService.GetData(new Action<bool>((a) => { }));

            foreach (var group in data)
            {
                SetGroupAnnotations(group);
            }
        }

        internal void SetGroupAnnotations(FallsLocationGroup group)
        {
            if (group.FallsLocationlist != null)
            {
                if (group.FallsLocationlist.Count > 0)
                {
                    foreach (var item in group.FallsLocationlist)
                    {
                        SetAnnotation("Report", item.Adress, group.GroupColor, group.GruopId);
                    }
                }

                    group.FallLocationChanged += new Action<FallsLocationGroup,FallLocation>((g,fl) => { SetAnnotation("Report", fl.Adress, g.GroupColor, g.GruopId); });
            }
            group.EsimateFallLocationChanged += new Action<FallsLocationGroup>((g) =>
            {
                //The existing estimate FL  annotaion will change the lat and long.
                var location = FallLocationService.getLocation(g.EstimateFallLocation.Adress);
                foreach (var item in DataMapAnnotations)
                {
                    if (item.AnnotationSymbol.Uid.Equals("Estimate_" + group.GruopId))
                    {
                        DispatcherHelper.CheckBeginInvokeOnUI(() =>
                        {
                            item.Latitude = location[0];
                            item.Longitude = location[1];
                        });
                    }
                }
            });
            if (group.EstimateFallLocation != null)
            {
                SetAnnotation("Estimate", group.EstimateFallLocation.Adress, group.GroupColor, group.GruopId);
            }
            if (group.AccurateFallLocation != null)
            {
                SetAnnotation("Accurate", group.AccurateFallLocation.Adress, group.GroupColor, group.GruopId);
            }
            else
            {
                group.AccurateFallLocationChanged += new Action<FallsLocationGroup>((g) => { SetAnnotation("Accurate", g.AccurateFallLocation.Adress, g.GroupColor, g.GruopId); });
            }

        }

        

        internal void SetAnnotation(string type, string adress, string groupColor, string groupId)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {

                MapAnnotations annotations = new MapAnnotations();
                var location = FallLocationService.getLocation(adress);
                annotations.Latitude = location[0];
                annotations.Longitude = location[1];
                Shape shape;
                switch (type)
                {
                    case "Report":
                        shape = EllipseFL.getEllipse(groupColor);
                        break;
                    case "Estimate":
                        shape = EllipseFL.getEllipseWithBorder(groupColor);
                        break;
                    case "Accurate":
                        shape = StarFL.getStarShape(groupColor);
                        break;
                    default:
                        throw new Exception("Unknown Type");
                }
                annotations.AnnotationSymbol = shape;
                annotations.AnnotationSymbol.Uid = type + "_" + groupId+ "_" + adress;
                DataMapAnnotations.Add(annotations);
            });
        }


    }//MapViewModle
}//namespace

