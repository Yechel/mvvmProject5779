using FL_Project.Model;
using FL_Project.ShapesFL;
using FL_Project.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using Syncfusion.UI.Xaml.Maps;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FL_Project.Skins
{
    /// <summary>
    /// Interaction logic for MapUC.xaml
    /// </summary>
    public partial class MapUC : UserControl
    {

        MapViewModle instance;

        public MapUC()
        {
            InitializeComponent();
            instance = SimpleIoc.Default.GetInstance<MapViewModle>();
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                if (instance != null)
                {
                    foreach (var item in instance.DataMapAnnotations)
                    {
                        item.AnnotationSymbol.MouseEnter += Shape_MouseEnter;
                        item.AnnotationSymbol.MouseLeave += Shape_MouseLeave;
                        item.AnnotationSymbol.MouseDown += Shape_MouseDown;
                        item.AnnotationSymbol.MouseUp += AnnotationSymbol_MouseUp;
                        SFL.Annotations.Add(item);
                    }
                }
            });
            instance.DataMapAnnotations.CollectionChanged += DataMapAnnotations_CollectionChanged;

          
        }

        private void DataMapAnnotations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                        var item = (MapAnnotations)((object[])e.NewItems.SyncRoot)[0];
                        item.AnnotationSymbol.MouseEnter += Shape_MouseEnter;
                        item.AnnotationSymbol.MouseLeave += Shape_MouseLeave;
                        item.AnnotationSymbol.MouseDown += Shape_MouseDown;
                item.AnnotationSymbol.MouseUp += AnnotationSymbol_MouseUp; 
                SFL.Annotations.Add(item);
            });
        }

        private void Shape_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var shape = (System.Windows.Shapes.Shape)sender;
            var key = shape.Uid.Split('_')[1];
            foreach (var item in instance.DataMapAnnotations ) 
            {
                if (item.AnnotationSymbol.Uid.Contains(key))
                {
                    item.AnnotationSymbol.Opacity = 0.5;

                }
            }
            //  shape.Fill = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString("Red"));
        }


        private void AnnotationSymbol_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var shape = (System.Windows.Shapes.Shape)sender;
            var key = shape.Uid.Split('_')[1];
            foreach (var item in instance.DataMapAnnotations)
            {
                if (item.AnnotationSymbol.Uid.Contains(key))
                {
                    item.AnnotationSymbol.Opacity = 1;

                }
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MapAnnotations ma = new MapAnnotations();
            ma.Latitude = 31.76367679378092;
            ma.Longitude = 35.22701968033334;
            ma.AnnotationLabel = "ירושלים";
            ma.AnnotationLabelFontSize = 20;
            var ellipse = new System.Windows.Shapes.Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.Orange);
            ellipse.Height = 20;
            ellipse.Width = 20;
            ellipse.MouseEnter += Shape_MouseEnter;
            ellipse.MouseLeave += Shape_MouseLeave;
            ellipse.MouseDown += Shape_MouseDown;
            ma.AnnotationSymbol = ellipse;
            SFL.Annotations.Add(ma);


        }

        

        private void Shape_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Ellipse)
            {
                Ellipse shape = (Ellipse)sender;
                shape.Opacity = 1;
                shape.Height = EllipseFL.SHAPE_SMALL_SIZE;
                shape.Width = EllipseFL.SHAPE_SMALL_SIZE;
            }
            else if (sender is Polygon)
            {
                Polygon shape = (Polygon)sender;
                shape.Opacity = 1;
                shape.Height = StarFL.SHAPE_SMALL_SIZE;
                shape.Width = StarFL.SHAPE_SMALL_SIZE;
                            }
            foreach (var item in instance.DataMapAnnotations)
            {
                if (item.AnnotationSymbol.Opacity!=1)
                {
                    item.AnnotationSymbol.Opacity = 1;

                }
            }
        }

        private void Shape_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Ellipse)
            {
                Ellipse shape = (Ellipse)sender;
                shape.Opacity = 1;
                shape.Height = EllipseFL.SHAPE_LARGE_SIZE;
                shape.Width = EllipseFL.SHAPE_LARGE_SIZE;
            }
            else if (sender is Polygon)
            {
                Polygon shape = (Polygon)sender;
                shape.Opacity = 1;
                shape.Height = StarFL.SHAPE_LARGE_SIZE;
                shape.Width = StarFL.SHAPE_LARGE_SIZE;

            }
        }


        private void setLableVisability(object sender)
        {
            Action action = new Action(() =>
            {

                var lable = (Label)sender;
                var name = lable.Name;
                if (lable.Opacity == 1)
                {
                    lable.Opacity = 0.60;
                    foreach (var annotation in SFL.Annotations)
                    {
                        if (annotation.AnnotationSymbol.Uid.Contains(name))
                        {
                            annotation.AnnotationSymbol.Visibility = System.Windows.Visibility.Hidden;
                        }
                    }
                }
                else
                {
                    lable.Opacity = 1;
                    foreach (var annotation in SFL.Annotations)
                    {
                        if (annotation.AnnotationSymbol.Uid.Contains(name))
                        {
                            annotation.AnnotationSymbol.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                }
            });
            Dispatcher.BeginInvoke(action);
        }

        private void Label_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Label)
            {
                var label = (Label)sender;
                if (label.Name.Equals("Filter"))
                {
                    return;
                }
                    label.FontSize = 14;
                    label.Foreground = new SolidColorBrush(Colors.CornflowerBlue);
                
            }
        }


        private void Label_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if (sender is Label)
            {
                var label = (Label)sender;
                if (label.Name.Equals("Filter"))
                {
                    return;
                }
                label.FontSize = 13;
                label.Foreground = new SolidColorBrush(Colors.DeepSkyBlue);
            }
        }


        private void Label_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            setLableVisability(sender);
        }
    }


}


