using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FL_Project.ShapesFL
{
  static  class  EllipseFL
    {
        public static  int SHAPE_SMALL_SIZE = 12;
        public static int SHAPE_LARGE_SIZE = 14;
        public static Ellipse getEllipse(string color)
        {

            var ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString(color));
            ellipse.Height = SHAPE_SMALL_SIZE;
            ellipse.Width = SHAPE_SMALL_SIZE;
            return ellipse;

        }

        public static Ellipse getEllipseWithBorder(string color)
        {
            var ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush((Color)System.Windows.Media.ColorConverter.ConvertFromString(color));
            ellipse.Height = SHAPE_SMALL_SIZE;
            ellipse.Width = SHAPE_SMALL_SIZE;
            ellipse.Stroke = new SolidColorBrush(Colors.Black);
            return ellipse;

        }



    }
}
