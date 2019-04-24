/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:FL_Project.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using FL_Project.Model;
using System.Collections.ObjectModel;

namespace FL_Project.ViewModel
{
    public class MapViewModle
    {

        ObservableCollection<FallsLocationGroup> data = FallLocationService.GetData(new System.Action<bool>((a)=> {  }));
        
    }
}