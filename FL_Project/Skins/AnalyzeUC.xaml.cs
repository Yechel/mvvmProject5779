using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;
using FL_Project.Model;
using Syncfusion.Windows.Shared;
using GalaSoft.MvvmLight.Ioc;
using FL_Project.ViewModel;
using GalaSoft.MvvmLight.Threading;

namespace FL_Project.Skins
{
    /// <summary>
    /// Interaction logic for AnalyzeUC.xaml
    /// </summary>
    public partial class AnalyzeUC : UserControl
    {
        ToolTip ToolTip1 { get; set; }
        AnalyzeViewModle instance;

        public AnalyzeUC()
        {
            InitializeComponent();
            Root.IsEnabled = false;
            /*  var distance = FallLocationService.getListOfDistance().ToArray();
              DistanceDiagramSlider.SelectionStart = distance[0];
              DistanceDiagramSlider.SelectionEnd = distance[distance.Length - 1];*/

            //   var i = carusel.Children.Count;



            instance = SimpleIoc.Default.GetInstance<AnalyzeViewModle>();
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                if (instance != null)
                {
                    foreach (var item in instance.ListOfImages)
                    {
                       Viewbox vb = new Viewbox();
                        vb.Child = item;
                        AFLCarusel.Items.Add(new CarouselItem() { Content = vb });
                        
                       // AFLCarusel.Items.Add(item);
                    }
                }
            });
            Root.IsEnabled = true;
        }
       

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }


        // private void CB_Checked(object sender, RoutedEventArgs e)
        // {
        /*  Dispatcher.BeginInvoke((Action)(() =>
          {
              if (SelectAllCB.IsChecked == true)
              {
                  SelectAccurateCB.IsChecked = true;
                  SelectAccurateCB.IsEnabled = false;
                  SelectEstimateCB.IsChecked = true;
                  SelectEstimateCB.IsEnabled = false;
                  SelectReportedCB.IsChecked = true;
                  SelectReportedCB.IsEnabled = false;
              }
              else //(SelectAllCB.IsChecked != true)
              {
                  SelectAccurateCB.IsEnabled = true;
                  SelectEstimateCB.IsEnabled = true;
                  SelectReportedCB.IsEnabled = true;

              }

          }));*/
        // }
    }
}
