/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:FL_Project.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

using FL_Project.Model;
using System;
using System.Windows;

namespace FL_Project.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CallCenterViewModle>();
            SimpleIoc.Default.Register<MapViewModle>();
            SimpleIoc.Default.Register<AnalyzeViewModle>();
            Messenger.Default.Register<NotificationMessage>(this, NotifyUserMethod);
            
        }

        public MainViewModel MainViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public AnalyzeViewModle AnalyzeVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AnalyzeViewModle>();
            }
        }

        public CallCenterViewModle CallCenterVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CallCenterViewModle>();
            }
        }

        public MapViewModle MapVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MapViewModle>();
            }
        }

        internal static void Cleanup()
        {
            FallLocationService.DataChanged = null;
            Console.Out.WriteLine("program close...");
        }

        private void NotifyUserMethod(NotificationMessage message)
        {
            MessageBox.Show(message.Notification);
        }

    } 

        
    }