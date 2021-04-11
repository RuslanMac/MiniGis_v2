using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGis.Core
{
    public class Bounds
    {
        private double _xMin;
        private double _yMin;
        private double _xMax;
        private double _yMax;

        private bool _isExistXmin;
        private bool _isExistYmin;
        private bool _isExistXmax;
        private bool _isExistYmax;

        public Bounds(double xMin, double yMin, double xMax, double yMax)
        {
            _xMin = xMin;
            _yMin = yMin;
            _xMax = xMax;
            _yMax = yMax;
        }

        public Bounds()
        {

        }

        public bool IsValue
        {
            get
            {
                return (_isExistXmin && _isExistYmin && _isExistXmax && _isExistYmax);
            }
        }
        public double Xmin
        {
            get
            {
                return _xMin;
            }
            set
            {
                _xMin = value;
                _isExistXmin = true;
            }
        }
        public double Ymin
        {
            get
            {
                return _yMin;
            }
            set
            {
                _yMin = value;
                _isExistYmin = true;
            }
        }
        public double Xmax
        {
            get
            {
                return _xMax;
            }
            set
            {
                _xMax = value;
                _isExistXmax = true;
            }
        }
        public double Ymax
        {
            get
            {
                return _yMax;
            }
            set
            {
                _yMax = value;
                _isExistYmax = true;
            }
        }
        // метод объединения границ
        public static Bounds Union
            (Bounds a, Bounds b)
        {
            if (!a.IsValue && !b.IsValue)
                return new Bounds();
            if (a.IsValue && !b.IsValue)
                return a;
            if (!a.IsValue && b.IsValue)
                return b;

            Bounds together = new Bounds();
            together.Xmin = Math.Min(a.Xmin, b.Xmin);
            together.Ymin = Math.Min(a.Ymin, b.Ymin);
            together.Xmax = Math.Max(a.Xmax, b.Xmax);
            together.Ymax = Math.Max(a.Ymax, b.Ymax);
            return together;
        }
        //переопределение метода объединения границ
        public static Bounds Union(Bounds a, Node b)
        {
            Bounds together = new Bounds();

            if (!a.IsValue)
            {
                together.Xmin = b.X;
                together.Ymin = b.Y;
                together.Xmax = b.X;
                together.Ymax = b.Y;
            }
            else
            {
                together.Xmin = Math.Min(a.Xmin, b.X);
                together.Ymin = Math.Min(a.Ymin, b.Y);
                together.Xmax = Math.Max(a.Xmax, b.X);
                together.Ymax = Math.Max(a.Ymax, b.Y);
            }

            return together;
        }
    }
}
