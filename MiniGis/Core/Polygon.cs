using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MiniGis.Core
{
    public class Polygon : PolyLine
    {
        public Brush OwnBrush
        {
            get;
            set;
        } = new SolidBrush(Color.White);
        public Polygon()
        {
            _objectType = MapObjectType.Polygon;
        }

        internal override void Paint(PaintEventArgs e)
        {
            
            List<System.Drawing.Point> points = new List<System.Drawing.Point>();

            foreach (Node node in _nodes)
            {
                System.Drawing.Point point = Layer.Map.MapToScreen(node);
                points.Add(point);
            }
            System.Drawing.Point[] pointsArray = points.ToArray();
            e.Graphics.FillPolygon(OwnBrush, pointsArray);
            Pen pen;
            pen = (Pen)OwnPen.Clone();
            if (Selected)
            {
                pen.Color = Color.Yellow;
                pen.Width++;
            }
            e.Graphics.DrawPolygon(pen, pointsArray);
        }
        public override bool IsIntersects(Node searchpoint, double quad)
        {
            // checking with border is the same as a polyline
            Node p1 = new Node(searchpoint.X - quad, searchpoint.Y + quad);
            Node p2 = new Node(searchpoint.X + quad, searchpoint.Y + quad);
            Node p3 = new Node(searchpoint.X + quad, searchpoint.Y - quad);
            Node p4 = new Node(searchpoint.X - quad, searchpoint.Y - quad);
            Node begin;
            Node end;
            for (int i = 0; i < NodeCount - 1; i++)
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
            begin = _nodes[NodeCount-1];
            end = _nodes[0];
            //4 уравнения
            if (Analysis.IsSegmentsIntersects(begin, end, p1, p2))
                return true;
            if (Analysis.IsSegmentsIntersects(begin, end, p2, p3))
                return true;
            if (Analysis.IsSegmentsIntersects(begin, end, p3, p4))
                return true;
            if (Analysis.IsSegmentsIntersects(begin, end, p4, p1))
                return true;

            //Принадлежность в полигне

            return Analysis.PointInsidePolygon(_nodes,searchpoint);


        }




        public static void SortNumbers(List<Node> nodesintersects, Node begin)
        {
            Node node = new Node(0, 0);
          
            for (int i=0;i<nodesintersects.Count;i++)
            {
                for (int j = nodesintersects.Count-1;j>i;j--)
                {
                    if (Math.Abs(begin.X -  nodesintersects[j].X) <  Math.Abs(begin.X -  nodesintersects[j-1].X))
                    {
                        node = nodesintersects[j - 1];
                        nodesintersects[j - 1] = nodesintersects[j];
                        nodesintersects[j] = node;
                    }
                }
            }

        }




        public static Polygon SortNumbers12(List<Node> nodesintersects,Node begin,Polygon polygon1)
        {
            Node nodeoporniy = new Node(1, 1);
            Node nodet = new Node(1, 1);
            
            if (nodesintersects.Count != 0)
            {
                for (int i = 0; i < nodesintersects.Count; i++)
                {
                    for (int j = nodesintersects.Count - 1; j > i; j--)
                    {
                        if (Math.Abs(begin.X - nodesintersects[j].X) < Math.Abs(begin.X - nodesintersects[j - 1].X))
                        {
                            nodeoporniy = nodesintersects[j - 1];
                            nodesintersects[j - 1] = nodesintersects[j];
                            nodesintersects[j] = nodeoporniy;

                        }
                    }
                }
                for (int t = 0; t < nodesintersects.Count; t++)
                {
                    nodet = nodesintersects[t];
                    nodet.IsInPolygon = true;
                    polygon1.AddNode(nodet);
                    
                }
                return polygon1;
            }
            return polygon1;
        }




        public static Polygon SumPolygons(List<Polygon> polygons)
        {
            int nodecounts = 0;
            List<Node> NodeIntersects = new List<Node>();
            Polygon newPolygon = new Polygon();
            List<Node> formingnodes = new List<Node>();
            //Нахождение узлов не являющихся подмножеством другого полигона
            for (int i = 0; i < polygons[1].NodeCount; i++)
            {
                Node node = new Node(0,0);
                node = polygons[1]._nodes[i];
                if (Analysis.IsNodeInPolygon(polygons[0], node)==true)
                {
                    newPolygon.AddNode(node);
                    formingnodes.Add(node);
                }
                
             }
            
            for (int i = 0; i < polygons[0].NodeCount; i++)
            {
                Node node = new Node(0, 0);
                node = polygons[0]._nodes[i];
                if (Analysis.IsNodeInPolygon(polygons[1], node) == true)
                {
                    newPolygon.AddNode(node);
                    formingnodes.Add(node);
                }

            }
            //Найти пересечения узлы полигонов
           /* polygons[0].AddNode(polygons[0].GetNode(0));
            polygons[1].AddNode(polygons[1].GetNode(0));
            for (int i =0;i<polygons[0].NodeCount-1;i++)
            {
                
                Node node = new Node(0, 0);
                Node begin = polygons[0]._nodes[i];
                Node end = polygons[0]._nodes[i + 1];
                for (int j=0;j<polygons[1].NodeCount-1;j++)
                {
                    Node p1 = polygons[1].GetNode(j);
                    Node p2 = polygons[1].GetNode(j + 1);
                    node = Analysis.PointofIntercestcs(begin, end, p1, p2);
                    if (node.X != 0 && node.Y != 0)
                    {
                        nodecounts++;
                        node = Analysis.PointofIntercestcs(begin, end, p1, p2);
                    }



                }

            }
            */
            polygons[0].AddNode(polygons[0].GetNode(0));
            polygons[1].AddNode(polygons[1].GetNode(0));
            List<Node> nodesintersects = new List<Node>();
            //Формируем полигоны с новыми вершинами, которые включают также пересечения 
            Polygon newpolygon1 = new Polygon();
            for (int i = 0; i < polygons[0].NodeCount - 1; i++)
            {
               // Node node = new Node(0, 0);
                Node node = null;
                Node begin = polygons[0]._nodes[i];
                Node end = polygons[0]._nodes[i + 1];
                newpolygon1.AddNode(begin);

                for (int j = 0; j < polygons[1].NodeCount - 1; j++)
                {
                    Node p1 = polygons[1].GetNode(j);
                    Node p2 = polygons[1].GetNode(j + 1);
                    node = Analysis.PointofIntercestcs(begin, end, p1, p2);
                    
               
                    if (node != null)
                    {
                        nodecounts++;
                      
                        nodesintersects.Add(node);
                        NodeIntersects.Add(node);
                        node = Analysis.PointofIntercestcs(begin, end, p1, p2);
                       
                    }
                   
                }
              
                newpolygon1 = SortNumbers12(nodesintersects, begin,newpolygon1);
                nodesintersects.Clear();
                
            }

            Polygon newpolygon2 = new Polygon();
            nodesintersects.Clear();
            for (int i = 0; i < polygons[1].NodeCount - 1; i++)
            {
       
                Node node = null;
               // Node node = new Node(0, 0);
                Node begin = polygons[1]._nodes[i];
                Node end = polygons[1]._nodes[i + 1];
                newpolygon2.AddNode(begin);
                for (int j = 0; j < polygons[0].NodeCount - 1; j++)
                {
                    Node p1 = polygons[0].GetNode(j);
                    Node p2 = polygons[0].GetNode(j + 1);
                    node = Analysis.PointofIntercestcs(begin, end, p1, p2);
                    if (node !=null)
                    {
                        nodecounts++;
                        nodesintersects.Add(node);
                        NodeIntersects.Add(node);
                        node = Analysis.PointofIntercestcs(begin, end, p1, p2);
                    }
                    
                }
               
                newpolygon2 = SortNumbers12(nodesintersects, begin, newpolygon2);
                nodesintersects.Clear();

               
            }

            if (NodeIntersects.Count > 0)
            {
                bool x = false;
                //Узнаем, есть ли такие вершины, что является часть другого полигона
                Polygon newpolygon1s = new Polygon();
                Polygon newpolygon2s = new Polygon();
                Polygon newpolygon3s = new Polygon();

                for (int i = 0; i < newpolygon1.NodeCount; i++)
                {
                    for (int j = 0; j < formingnodes.Count; j++)
                    {
                        if (newpolygon1.GetNode(i) == formingnodes[j])
                        {
                            x = true;
                            // newpolygon1s.AddNode(newpolygon1.GetNode(i));
                        }
                    }
                    if (x == false)
                    {
                        newpolygon1s.AddNode(newpolygon1.GetNode(i));
                    }
                    x = false;

                }

                bool t = false;
                for (int i = 0; i < newpolygon2.NodeCount; i++)
                {
                    for (int j = 0; j < formingnodes.Count; j++)
                    {
                        if (newpolygon2.GetNode(i) == formingnodes[j])
                        {
                            t = true;
                            //  newpolygon2s.AddNode(newpolygon2.GetNode(i));
                        }
                    }
                    if (t == false)
                    {
                        newpolygon2s.AddNode(newpolygon2.GetNode(i));

                    }
                    t = false;
                }




                //Идем по вершинам составляя новый полигон 

                for (int i = 0; i < newpolygon1s.NodeCount; i++)
                {
                    int k = 0;
                    //  int j = 0;

                    newpolygon3s.AddNode(newpolygon1s.GetNode(i));
                    if (newpolygon1s.GetNode(i).IsInPolygon == true)
                    {
                        for (int j = 0; j < newpolygon2s.NodeCount; j++)
                        {
                            if ((newpolygon2s.GetNode(j).X == newpolygon1s.GetNode(i).X) && (newpolygon2s.GetNode(j).Y == newpolygon1s.GetNode(i).Y))
                            {
                                k = j + 1;
                            }
                        }

                        while (newpolygon2s.GetNode(k).IsInPolygon != true)
                        {
                            int tm = 7;
                            newpolygon3s.AddNode(newpolygon2s.GetNode(k));
                            k++;
                            if (k >= newpolygon2s.NodeCount)
                            {
                                k = 0;
                            }

                        }


                    }

                }


                int s = 4;



                return newpolygon3s;
            }
            else
            {
                return null;

            }




        }
    }
}
