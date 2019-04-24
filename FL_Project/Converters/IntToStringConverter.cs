using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FL_Project.Converters
{
     class IntToStringConverter : IValueConverter
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
                return "";
            //}
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            int i = 0;
            Int32.TryParse(str, out i);
            return i;
        }
    }
}
