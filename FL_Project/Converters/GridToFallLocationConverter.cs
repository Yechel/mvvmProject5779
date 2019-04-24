using FL_Project.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FL_Project.Converters
{
    class GridToFallLocationConverter : IMultiValueConverter
    {
   
object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
         //   FallLocation fallLocation = new FallLocation((string)values[0] , dateStr, Int32.Parse((string)values[4]));
            throw new NotImplementedException();
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
