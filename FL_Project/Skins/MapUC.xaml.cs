using FL_Project.Model;
using Syncfusion.UI.Xaml.Maps;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FL_Project.Skins
{
    /// <summary>
    /// Interaction logic for MapUC.xaml
    /// </summary>
    public partial class MapUC : UserControl
    {


        public MapUC()
        {
            InitializeComponent();

            ObservableCollection<FallsLocationGroup> data = FallLocationService.GetData(new Action<bool>((a) => { }));
            var i = 0.01111;           foreach (var group in data)
            {
                if (group.FallsLocationlist != null)
                {
                    foreach (var item in group.FallsLocationlist)
                    {
                        MapAnnotations annotations = new MapAnnotations();

                        annotations.Latitude = 31.76367679378092+i;
                        annotations.Longitude = 35.22701968033334+i;
                        i = i + 0.01111;
                        var elipse = new Ellipse();
                        elipse.Fill = new SolidColorBrush(Colors.AliceBlue);
                        elipse.Height = 10;
                        elipse.Width = 10;  


                        annotations.AnnotationSymbol = elipse;
                        SFL.Annotations.Add (annotations);
                    }
                }
                if (group.EstimateFallLocation != null)
                {
                    MapAnnotations annotations = new MapAnnotations();
                    annotations.Latitude = 31.76367679378092 + i;
                    annotations.Longitude = 35.22701968033334 + i;
                    i = i + 0.01111;
                    var elipse = new Ellipse();
                    elipse.Stroke = new SolidColorBrush(Colors.Black);
                    elipse.Fill = new SolidColorBrush(Colors.AliceBlue);
                    elipse.Height = 10;
                    elipse.Width = 10;
                    annotations.AnnotationSymbol = elipse;
                    SFL.Annotations.Add(annotations);
                   }
             /*   if (group.AccurateFallLocation != null)
                {
                    MapAnnotations annotations = new MapAnnotations();
                    annotations.Latitude = 31.76367679378092 + i;
                    annotations.Longitude = 35.22701968033334 + i;
                    i = i + 0.01111;
                    var star = StarShape.getStarShape();
      
                    annotations.AnnotationSymbol = star;
                    SFL.Annotations.Add(annotations);

                }*/

                 
            }


           



        }
    }
}
