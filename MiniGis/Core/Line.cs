using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MiniGis.Core
{
    public class Line : MapObject
    {
        private Node _begin;
        private Node _end;
        
        public Pen OwnPen
        {
            get;
            set;
        } = new Pen(Color.Black);
      

        
        public Line(double xBegin, double yBegin, double xEnd, double yEnd)
        {
            _begin = new Node(xBegin, yBegin);
            _end = new Node(xEnd, yEnd);
            _objectType = MapObjectType.Line;
        }

        public Line(Node begin, Node end) //экземпляры должны быть рпоинициализированны за ранее при вызове, в случае данного конструктора
        {
            _begin = begin;
            _end = end;
            _objectType = MapObjectType.Line;
        }

        public double XBegin
        {
            get
            {
                return _begin.X;
            }
            set
            {
                _begin.X = value;
            }
        }

        public double YBegin
        {
            get
            {
                return _begin.Y;
            }
            set
            {
                _begin.Y = value;
            }
        }

        public double XEnd
        {
            get
            {
                return _end.X;
            }
            set
            {
                _end.X = value;
            }
        }

        public double YEnd
        {
            get
            {
                return _end.Y;
            }
            set
            {
                _end.Y = value;
            }
        }

        public Node Begin
        {
            get
            {
                return _begin;
            }

        }

        public Node End
        {
            get
            {
                return _end;
            }

        }

        internal override void Paint(PaintEventArgs e)
        {
            //здесь будет отрисовка линии
            System.Drawing.Point pBegin = Layer.Map.MapToScreen(_begin);
            System.Drawing.Point pEnd = Layer.Map.MapToScreen(_end);
            Pen pen;
            pen = (Pen)OwnPen.Clone();
            if (Selected)
            {
                pen.Color = Color.Yellow;
                pen.Width++;
            }

            e.Graphics.DrawLine(pen, pBegin, pEnd);
        }
        protected override Bounds GetBounds()
        {
            return new Bounds(XBegin, YBegin, XEnd, YEnd);
        }
      
        public override bool IsIntersects(Node searchpoint, double quad)
        {
            bool isBeginInside =
                (Begin.X >= searchpoint.X - quad) && (Begin.X <= searchpoint.X + quad) &&
                (Begin.Y >= searchpoint.Y - quad) && (Begin.Y <= searchpoint.Y + quad);
            bool isEndInside =
                (End.X >= searchpoint.X - quad) && (End.X <= searchpoint.X + quad) &&
                (End.Y >= searchpoint.Y - quad) && (End.Y <= searchpoint.Y + quad);
            if (isBeginInside || isEndInside)
                return true;
            Node p1 = new Node(searchpoint.X - quad, searchpoint.Y + quad);
            Node p2 = new Node(searchpoint.X + quad, searchpoint.Y + quad);
      
            if (Analysis.IsSegmentsIntersects(Begin, End, p1, p2))
                return true;
            Node p3 = new Node(searchpoint.X + quad, searchpoint.Y - quad);
            if (Analysis.IsSegmentsIntersects(Begin, End, p2, p3))
                return true;
            Node p4 = new Node(searchpoint.X - quad, searchpoint.Y - quad);
            if (Analysis.IsSegmentsIntersects(Begin, End, p3, p4))
                return true;
            if (Analysis.IsSegmentsIntersects(Begin, End, p4, p1))
                return true;

            return false;
        }

    }
}
