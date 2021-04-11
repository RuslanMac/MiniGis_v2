using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MiniGis.Core
{
    public class PolyLine : MapObject 
    {
        protected List<Node> _nodes = new List<Node>();
        private Bounds _bounds = new Bounds();

    /*    public Pen OwnPen
        {
            get;
            set;
        } = new Pen(Color.Black);
        */

        public PolyLine()
        {
            _objectType = MapObjectType.PolyLine;
        }

        public Node this[int index]
        {
            get
            {
                return _nodes[index];
            }
            set
            {
                _nodes[index] = value;
            }
        }

        public void AddNode(double x, double y)
        {
            Node node = new Node(x, y);
            _nodes.Add(node);
        }

        public void AddNode(Node node)
        {
            _nodes.Add(node);
        }

        public void RemoveNode(int index)
        {
            _nodes.RemoveAt(index);
        }

        public void Clear()
        {
            _nodes.Clear();
        }

        public Node GetNode(int index)
        {
            return _nodes[index];
        }
      
        public List<Node> GetNodes()
        {
            return _nodes;
        }
        internal override void Paint(PaintEventArgs e)
        {
            List<System.Drawing.Point> points = new List<System.Drawing.Point>();
            
            foreach (Node node in _nodes)
            {
                System.Drawing.Point point = Layer.Map.MapToScreen(node);
                points.Add(point);
            }
            Pen pen;
            pen = (Pen)OwnPen.Clone();
            if (Selected)
            {
                pen.Color = Color.Yellow;
                pen.Width++;
            }
            e.Graphics.DrawLines(pen, points.ToArray());
        }

        internal void RemoveNode()
        {
            throw new NotImplementedException();
        }

        public int NodeCount
        {
            get
            {
               return _nodes.Count();
            }
        }
        //возвращение границ
        protected override Bounds GetBounds()
        {
            UpdateBounds();
            return _bounds;
        }
        //обновление границ
        public void UpdateBounds()
        {
            Bounds bounds = new Bounds();
            foreach (Node node in _nodes)
            {
                bounds = Bounds.Union(bounds, node);
            }
            _bounds = bounds;
        }
        public override bool IsIntersects(Node searchpoint, double quad)
        {
            Node p1 = new Node(searchpoint.X - quad, searchpoint.Y + quad);
            Node p2 = new Node(searchpoint.X + quad, searchpoint.Y + quad);
            Node p3 = new Node(searchpoint.X + quad, searchpoint.Y - quad);
            Node p4 = new Node(searchpoint.X - quad, searchpoint.Y - quad);
            Node begin;
            Node end;
            for (int i =0;i<NodeCount-1;i++)
            {
                begin = _nodes[i];
                end = _nodes[i + 1];

                if (Analysis.IsSegmentsIntersects(begin, end, p1, p2))
                    return true;
               
                if (Analysis.IsSegmentsIntersects(begin, end, p2, p3))
                    return true;
             
                if (Analysis.IsSegmentsIntersects(begin, end, p3, p4))
                    return true;
                if (Analysis.IsSegmentsIntersects(begin, end, p4, p1))
                    return true;
            }


            return false;
        }
    }
}
