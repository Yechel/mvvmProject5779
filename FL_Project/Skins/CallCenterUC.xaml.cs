using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Syncfusion.Windows.Shared;

namespace FL_Project.Skins
{
    /// <summary>
    /// Interaction logic for CallCenterUC.xaml
    /// </summary>
    public partial class CallCenterUC : UserControl
    {
       
        public TimeSpan MyTime { get; set; }
        
        /*  {
              get { return (string)GetValue(PicPathProperty); }
              set
              {
                  SetValue(PicPathProperty, value);
              }
          }

          private static void OnPicPathPropertyChanged(DependencyObject source,
          DependencyPropertyChangedEventArgs e)
          {
              CallCenterUC control = (CallCenterUC)source;

          }

          public static readonly DependencyProperty PicPathProperty = DependencyProperty.Register(
                "PicPath1",
                typeof(string),
                typeof(CallCenterUC),
                new FrameworkPropertyMetadata(null,
                    OnPicPathPropertyChanged
                )
              );*/


        public CallCenterUC()
        {
            InitializeComponent();
            MyTime = DateTime.Now.TimeOfDay;
            

            // DataContext = this;
                    }

        public CallCenterUC(ref ObservableCollection<Model.FallsLocationGroup> _listFL)
        {

            InitializeComponent();

            //Create the instance of TimeSpanEdit

            TimeSpanEdit timespan = new TimeSpanEdit();

            timespan.Width = 150;

            timespan.Height = 30;

            timespan.VerticalAlignment = VerticalAlignment.Top;

            //Adding control to the window

           // this.ReportTS = timespan;
        }

        public Model.FallLocation GetFallLocationToSave()
        {
            // return new FallLocation(adressTB.Text,"",Int32.Parse(numberOfFallsTB.Text) );
            throw new Exception("a");
        }








        private void CB_Checked(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
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

            }));
        }

        Point startPoint;
        private void List_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Store the mouse position
            startPoint = e.GetPosition(null);
        }

        private void List_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAnchestor<ListViewItem>((DependencyObject)e.OriginalSource);

                // Find the data behind the ListViewItem
                var contact = listView.ItemContainerGenerator.
                    ItemFromContainer(listViewItem);

                // Initialize the drag & drop operation
                DataObject dragData = new DataObject("myFormat", contact);
                DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
            }
        }

        private void DropList_DragEnter(object sender, DragEventArgs e)
        {
            AccurateFLCnvs.Background = new SolidColorBrush(Colors.AliceBlue);
        }

        private void DropList_DragLeave(object sender, DragEventArgs e)
        {
            AccurateFLCnvs.Background = new SolidColorBrush(Colors.LightGray);
        }

        private void DropList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] path = (string[])e.Data.GetData(DataFormats.FileDrop);
                System.Uri uri = new System.Uri(path[0]);
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = new BitmapImage(uri);
                AccurateFLCnvs.Background = imageBrush;
                RemoveB.Visibility = Visibility.Visible;
                this.PicPathV.Content = path[0];
                
            }
        }

        private static T FindAnchestor<T>(DependencyObject current)
         where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void RemoveB_Click(object sender, RoutedEventArgs e)
        {
            AccurateFLCnvs.Background = new SolidColorBrush(Colors.LightGray);
            RemoveB.Visibility = Visibility.Hidden;
            PicPathV.Content = "";
        }

        private void NewFallLocationTN_SelectionChanging(object sender, Syncfusion.Windows.Tools.Controls.TabSelectionChangingEventArgs e)
        {
            var col = (Syncfusion.Windows.Tools.Controls.TabNavigationControl)sender;
            if (newFallLocationTN.SelectedIndex == 1)
            {
                // col.SelectedTabItem = (Syncfusion.Windows.Tools.Controls.TabNavigationItem)e.OldItem;

                Action d = new Action(() => { newFallLocationTN.SelectedIndex++; });
                Dispatcher.BeginInvoke(d, null);

            }
            else
            {
                Action d = new Action(()=> { newFallLocationTN.SelectedIndex--; });
                Dispatcher.BeginInvoke(d,null);
           
            }
            
            e.Cancel = true;
          
        }

        
    }




}

