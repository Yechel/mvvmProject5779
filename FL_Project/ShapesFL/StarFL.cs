using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FL_Project.ShapesFL
{
    public static class StarFL
    {
        public static int SHAPE_SMALL_SIZE = 15;
        public static int SHAPE_LARGE_SIZE = 17;

        public static Polygon getStarShape()
        {
            Polygon star = new Polygon();
            star.Fill = new SolidColorBrush(Colors.Yellow);
            star.Stroke = new SolidColorBrush(Colors.Black);
            star.StrokeThickness = 0.5;
            star.StrokeLineJoin = PenLineJoin.Round;
            star.Width = 15;
            star.Height = 15;
            star.Stretch = Stretch.Fill;

            star.Points.Add(new Point(10, 2));
            star.Points.Add(new Point(10, 7));
            star.Points.Add(new Point(17, 7));
            star.Points.Add(new Point(12, 10));
            star.Points.Add(new Point(14, 15));
            star.Points.Add(new Point(9, 12));
            star.Points.Add(new Point(4, 15));
            star.Points.Add(new Point(6, 10));
            star.Points.Add(new Point(1, 7));
            star.Points.Add(new Point(7, 7));
            return star;
        }

        internal static Polygon getStarShape(string v)
        {
            Polygon star = new Polygon();
            star.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(v));
            star.Stroke = new SolidColorBrush(Colors.Black);
            star.StrokeThickness = 0.5;
            star.StrokeLineJoin = PenLineJoin.Round;
            star.Width = 15;
            star.Height = 15;
            star.Stretch = Stretch.Fill;
            star.Points.Add(new Point(10, 2));
            star.Points.Add(new Point(10, 7));
            star.Points.Add(new Point(17, 7));
            star.Points.Add(new Point(12, 10));
            star.Points.Add(new Point(14, 15));
            star.Points.Add(new Point(9, 12));
            star.Points.Add(new Point(4, 15));
            star.Points.Add(new Point(6, 10));
            star.Points.Add(new Point(1, 7));
            star.Points.Add(new Point(7, 7));
            return star;
        }


    }
}