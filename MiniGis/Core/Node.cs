using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniGis.Core
{
    public class Node
    {
        public double X;
        public double Y;
        public bool isInPolygon;
      

        public Node(double x, double y)
        {
            X = x;
            Y = y;
        }
       
       
        public bool IsInPolygon
        {
            get;set;
        }


       

    }
}
