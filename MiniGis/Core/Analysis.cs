using System;
using System.Collections.Generic;

namespace MiniGis.Core
{
    public static class Analysis
    {
        /// <summary>
        /// Алгоритм пересечение двух отрезков
        /// взято отсюда https://www.e-olymp.com/ru/blogs/posts/25
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static bool IsSegmentsIntersects(Node a, Node b, Node c, Node d)
        {
            double ABx, ABy, ACx, ACy, ADx, ADy;
            double CAx, CAy, CBx, CBy, CDx, CDy;
            double ACxAB, ADxAB, CAxCD, CBxCD;

            if (!RectanglesIntersects(
                Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Max(a.X, b.X), Math.Max(a.Y, b.Y),
                Math.Min(c.X, d.X), Math.Min(c.Y, d.Y), Math.Max(c.X, d.X), Math.Max(c.Y, d.Y)))
                return false;

            ACx = c.X - a.X;
            ACy = c.Y - a.Y;
            ABx = b.X - a.X;
            ABy = b.Y - a.Y;
            ADx = d.X - a.X;
            ADy = d.Y - a.Y;

            CAx = a.X - c.X;
            CAy = a.Y - c.Y;
            CBx = b.X - c.X;
            CBy = b.Y - c.Y;
            CDx = d.X - c.X;
            CDy = d.Y - c.Y;

            ACxAB = ACx * ABy - ACy * ABx;
            ADxAB = ADx * ABy - ADy * ABx;

            CAxCD = CAx * CDy - CAy * CDx;
            CBxCD = CBx * CDy - CBy * CDx;

            return ACxAB * ADxAB <= 0 && CAxCD * CBxCD <= 0;
        }

        static bool RectanglesIntersects(
            double aMinX, double aMinY, double aMaxX, double aMaxY,
            double bMinX, double bMinY, double bMaxX, double bMaxY)
        {
            if ((bMinX - aMaxX) * (bMaxX - aMinX) > 0)
                return false;
            if ((bMinY - aMaxY) * (bMaxY - aMinY) > 0)
                return false;

            return true;
        }
        public static bool PointInsidePolygon(List<Node> nodes,Node point)
        {
            bool result = false;
            for (int i = 0, j = nodes.Count - 1; i < nodes.Count;j=i++)
            {
                if (((nodes[i].Y <= point.Y) && (point.Y < nodes[j].Y) || ((nodes[j].Y <= point.Y) && (point.Y < nodes[i].Y))) && (point.X > (nodes[j].X - nodes[i].X) * (point.Y - nodes[i].Y) / (nodes[j].Y - nodes[i].Y) + nodes[i].X))
                    result = !result;
            }
            return result;
        }
        public static bool IsNodeInPolygon(Polygon polygon, Node point)
        {
            bool result = false;
            List<Node> nodes = polygon.GetNodes();
            for (int i = 0, j = polygon.NodeCount - 1; i < polygon.NodeCount; j = i++)
            {
                if (((nodes[i].Y <= point.Y) && (point.Y < nodes[j].Y) || ((nodes[j].Y <= point.Y) && (point.Y < nodes[i].Y))) && (point.X > (nodes[j].X - nodes[i].X) * (point.Y - nodes[i].Y) / (nodes[j].Y - nodes[i].Y) + nodes[i].X))
                    result = !result;
            }
            return result;
        }




        public static Node PointofIntercestcs(Node a,Node b,Node c, Node d)
        {
            // Node node = new Node(0, 0);
            double x = 0;
            double y = 0;

           
             /* double det = Math.Abs(-(b.X - a.X) * (d.Y - c.Y) + (d.X - c.X) * (b.Y - a.Y));
              double det1 = Math.Abs(-(c.X - a.X) * (d.Y - c.Y) + (d.X - c.X) * (c.Y - a.Y));
              double det2 = Math.Abs(-(b.X - a.X) * (c.Y - a.Y) + (c.X - a.X) * (b.Y - a.Y));
              double t1 = det1 / det;
              double t2 = det2 / det;
              if (t1>=0 && t1<=1 && t2>=0 && t2 <=1)
              {
                  node.X = a.X + (b.X - a.X) * t1;
                  node.Y = a.Y + (b.Y - a.Y) * t1;
                  return node;
              }
              return node;*/
              if (b.X-a.X==0 && d.Y-c.Y==0)
            {
                Node node = new Node(0, 0);


                if ((b.X >= Math.Min(d.X, c.X) && b.X <= (Math.Max(d.X, c.X))) && c.Y >= Math.Min(a.Y, b.Y) && (c.Y <= Math.Max(a.Y, b.Y)))
                    {
                        node.X = b.X;
                        node.Y = c.Y;
                        return node;

                    }

                
                
             //   return node;

            }
             if (a.Y-b.Y==0 && d.X-c.X==0)
            {
                Node node = new Node(0, 0);
                if (a.Y >= Math.Min(c.Y, d.Y) && a.Y <= Math.Max(c.Y, d.Y) && c.X <= Math.Max(a.X, b.X) && (c.X >= Math.Min(a.X, b.X)))
                {
                    node.X = d.X;
                    node.Y = a.Y;
                    return node;
                }
               // return node;
            }
             if (a.Y-b.Y==0)
            {
                y = a.Y;
                 x = (d.X - c.X) * (a.Y - c.Y) / (d.Y - c.Y) + c.X;
                if (x<=Math.Max(a.X,b.X)  && x  <= Math.Max(a.X,b.X) && y <= Math.Max(c.Y,d.Y) && y >= Math.Min(c.Y,d.Y))
                {
                    Node node = new Node(x, y);
                    return node;
                }
               
            }
            if (c.Y - d.Y == 0)
            {
                y = c.Y;
                x = (b.X - a.X) * (c.Y - a.Y) / (b.Y - a.Y) + a.X;
                if (x <= Math.Max(c.X, d.X) && x <= Math.Max(c.X, d.X) && y <= Math.Max(a.Y, b.Y) && y >= Math.Min(a.Y, b.Y))
                {
                    Node node = new Node(x, y);
                    return node;
                }

            }
            if (a.X - b.X == 0)
            {
                x = a.X;
                y = (a.X - c.X) * (d.Y - c.Y) / (d.X - c.X) + c.Y;
                if (x >= Math.Min(c.X, d.X) && x <= Math.Max(c.X, d.X) && y <= Math.Max(a.Y, b.Y)+1 && y >= Math.Min(a.Y, b.Y)-1)
                {
                    Node node = new Node(a.X, y);
                    return node;
                }

            }
            if (c.X - d.X == 0)
            {
                x = c.X;
                y = (c.X - a.X) * (b.Y - a.Y) / (b.X - a.X) + a.Y;
                if (x >= Math.Min(a.X, b.X) && x <= Math.Max(a.X, b.X) && y <= Math.Max(c.Y, d.Y) && y >= Math.Min(c.Y, d.Y))
                {
                    Node node = new Node(c.X, y);
                    return node;
                }

            }
            

            double z1 = (b.X - a.X) / (b.Y - a.Y);
            double z2 = (d.X - c.X) / (d.Y - c.Y);
            double det = Math.Abs(-(1 * z2) + z1 * 1);
            double det1 = Math.Abs(-(a.X - z1 * a.Y) * z2 + z1 * (c.X - z2 * c.Y));
            double det2 = Math.Abs(-(1 * c.X - z2 * c.Y) + (a.X - z1 * a.Y) * 1);
            double t1 = det1 / det;
            double t2 = det2 / det;
           
            x = det1 / det;
            y = det2 / det;

            if (x<=Math.Max(a.X,b.X) && x>=Math.Min(a.X,b.X) && y<=Math.Max(c.Y,d.Y) && y>=Math.Min(c.Y,d.Y))
            {
                Node node = new Node(0, 0);
                node.X = x;
                node.Y = y;
                return node;
            }
            return null;
          
            


        }

    }
}
