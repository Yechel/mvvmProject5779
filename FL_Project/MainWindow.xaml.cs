using System.Windows;
using FL_Project.ViewModel;
using FL_Project.Skins;
using System;
using System.Windows.Controls;
using Syncfusion.UI.Xaml.Maps;
using System.Windows.Controls.Primitives;

namespace FL_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();     
            
        }

        private void ToggleButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void ToggleButton_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var tb = (ToggleButton)sender;
            if (tb.IsPressed == false)
            {
                if (tb.Name.Equals(CallCenterB.Name))
                {
                    CallCenterB.IsChecked = true ;
                    AnalyzeB.IsChecked = false;
                }
                else
                {
                    CallCenterB.IsChecked = false;
                    AnalyzeB.IsChecked = true;

                }

            }



        }
    }
}