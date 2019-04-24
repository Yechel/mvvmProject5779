using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FL_Project.Converters
{
    class DateToStringConverter : IValueConverter
    {




        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            return dateTime.ToString("MM/dd/yyyy hh:mm");
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string date = (string)value;
            date.Replace('/', '&');
            date.Replace(':','&');
            date.Replace(' ', '&');
            var dateS = date.Split('&');
            DateTime dateTime = new DateTime(Int32.Parse(dateS[2]), Int32.Parse(dateS[0]), Int32.Parse(dateS[1]), Int32.Parse(dateS[3]), Int32.Parse(dateS[4]),0);
            return dateTime;
        }
    }
}
