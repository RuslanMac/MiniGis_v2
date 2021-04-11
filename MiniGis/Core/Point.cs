using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MiniGis.Core
{
    public class Point : MapObject
    {
        private Node _point;

        public Font OwnFont
        {
            get;
            set;
        } = new Font(new FontFamily("Wingdings"), 12);

        public int Code
        {
            get;
            set;
        } = 0x5B;

        public Point(double x, double y)
        {
            _point = new Node(x, y); //первый способ
            _objectType = MapObjectType.Point;
        }

        public double X
        {
            get
            {
                return _point.X;
            }
            set
            {
                _point.X = value;
            }
        }

        public double Y
        {
            get
            {
                return _point.Y;
            }
            set
            {
                _point.Y = value;
            }
        }

        internal override void Paint(PaintEventArgs e)
        {
            char charCode = (char)Code;
            string strCode = charCode.ToString();
            Font font;
            Brush brush;
            if (!Selected)
            {
                font = new Font(OwnFont.FontFamily, OwnFont.Size);
                brush = new SolidBrush(Color.Black);
            }
            else
            {
                font = new Font(OwnFont.FontFamily, OwnFont.Size + 2);
                brush = new SolidBrush(Color.Yellow);


            }
            SizeF size = e.Graphics.MeasureString(strCode, font);// To measure width and height of char
            System.Drawing.Point point = Layer.Map.MapToScreen(_point);
            point.X -= (int)size.Width / 2;
            point.Y -= (int)size.Height / 2;
           
            
            e.Graphics.DrawString(strCode, font, brush, point.X, point.Y);
        }
        protected override Bounds GetBounds()
        {
            return new Bounds(X, Y, X, Y);
        }
        public override bool IsIntersects(Node searchpoint, double quad)
        {
            quad *= 2;
            return 
              (X >= searchpoint.X - quad) && (X <= searchpoint.X + quad) &&
              (Y >= searchpoint.Y - quad) && (Y <= searchpoint.Y + quad);
        }
    }
}
