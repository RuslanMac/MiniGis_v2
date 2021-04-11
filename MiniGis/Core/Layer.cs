using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniGis.Core
{
    public abstract  class Layer
    {

        
        public Bounds _bounds;


        public Map Map
        {
            get;
            set;
        }
        public Bounds Bounds
        {

            get
            {
                
                return _bounds;
            }
        }
        //обновление границ
        public abstract void Paint(PaintEventArgs e);
        
    }
}
