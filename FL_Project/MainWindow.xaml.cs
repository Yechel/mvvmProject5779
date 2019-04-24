using System.Windows;
using FL_Project.ViewModel;
using FL_Project.Skins;
using System;
using System.Windows.Controls;
using Syncfusion.UI.Xaml.Maps;

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

        
    }
}