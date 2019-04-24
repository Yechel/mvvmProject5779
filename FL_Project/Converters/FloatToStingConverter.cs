using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FL_Project.Converters
{
    class FloatToStingConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            /*  if (targetType is String)
              {
                  int i = (int)value;
                  return i.ToString();
              }
              else
              {*/
            float t = (float)value;

            return (int)t + " M";
            //}
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float t = (float)value;

            return (int)t + " M";
        }

       
    }
}
