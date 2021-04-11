using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniGis.Core
{
    public abstract class MapObject
    {
        protected MapObjectType _objectType;
        public MapObjectType Type
        {
            get
            {
                return _objectType;
            }
        }

        public Layer Layer
        {
            get;
            set;
        }
        public Bounds Bounds
        {
            get
            {
                return GetBounds();
            }
        }

        public Brush OwnBrush
        {
            get;
            set;
        } = new SolidBrush(Color.White);


        public Pen OwnPen
        {
            get;
            set;
        } = new Pen(Color.Black);
        public bool Selected { get; set; }
        
        internal abstract void Paint(PaintEventArgs e)  ;
        protected abstract Bounds GetBounds();
        public abstract bool IsIntersects(Node searchpoint, double quad);
        
    }
   
}
