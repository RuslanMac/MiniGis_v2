using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniGis.Core;
using System.Drawing;

namespace MiniGis
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            Core.Point point = new Core.Point(0, 0);
            Core.Line line = new Core.Line(1, 2, 500, 300);
            line.Selected = true;
            Core.PolyLine polyLine = new Core.PolyLine();

           
            

            polyLine.AddNode(0, 0); 
            polyLine.AddNode(-500, 300);
            polyLine.AddNode(-300, -500);
            polyLine.AddNode(500, 0);


           // polyLine.RemoveNode(1);
          //  Core.Polygon polygon = new Core.Polygon();
         //   polygon.AddNode(700, 0);
         //   polygon.AddNode(0, 400);
         //   polygon.AddNode(-100, 0);
         //   polygon.AddNode(0, -300);



            Core.Polygon polygon1 = new Core.Polygon();
            polygon1.AddNode(50, 50);
            polygon1.AddNode(150, 70);
            polygon1.AddNode(120, 10);
            polygon1.AddNode(72, 10);

            Core.Polygon polygon2 = new Core.Polygon();
            polygon2.AddNode(70, 180);
            polygon2.AddNode(120, 240);
            polygon2.AddNode(190, 20);
            polygon2.AddNode(72, 40);

            Core.Polygon polygon3 = new Core.Polygon();
            polygon3.AddNode(-100, -20);
            polygon3.AddNode(-50, -20);
            polygon3.AddNode(-50, -140);
            polygon3.AddNode(-100, -140);

            Core.Polygon polygon4 = new Core.Polygon();
            polygon4.AddNode(-80, -10);
            polygon4.AddNode(-60, -10);
            polygon4.AddNode(-60, -70);
            polygon4.AddNode(-80, -70);

            

            SolidBrush ownBrush= new SolidBrush(Color.Aquamarine);
            Pen ownPen = new Pen(Color.Green);
          //  polygon.OwnBrush = ownBrush;
         //   polygon.OwnPen = ownPen;
            polygon1.OwnBrush = ownBrush;
            polygon1.OwnPen = ownPen;

            /*Core.Node node = polyLine.GetNode(0);
             polyLine.Clear();*/
            /* for (int i=0; i<100; i++)
             {
                 polyLine.AddNode(i, -i);
             }
             //polyLine[0] = polyLine[2];
             */
            Core.VectorLayer layer = new VectorLayer();

            layer.AddObject(point);
            layer.AddObject(line);
            layer.AddObject(polyLine);
          //  layer.AddObject(polygon);
            layer.AddObject(polygon1);
            layer.AddObject(polygon2);
            layer.AddObject(polygon3);
            layer.AddObject(polygon4);
         
            int pointCount = 0, lineCount = 0, polyLineCount = 0, polygonCount = 0;

            foreach(MapObject obj in layer._objects)
            {
                switch (obj.Type)
                {
                    case MapObjectType.Point:
                        pointCount++;
                        break;
                    case MapObjectType.Line:
                        lineCount++;
                        break;
                    case MapObjectType.PolyLine:
                        polyLineCount++;
                        break;
                    case MapObjectType.Polygon:
                        polygonCount++;
                        break;
                }
            }

            MyMap.AddLayer(layer);
            MyMap.MapScale *= 2;
            
        }
        private void toolSelect_Click(object sender, EventArgs e)
        {
            MyMap.ActiveTool = MapToolType.Select;

            toolSelect.Enabled = false;
            toolSelect.Checked = true;
            toolPan.Enabled = true;
            toolPan.Checked = false;
            toolZoomIn.Enabled = true;
            toolZoomIn.Checked = false;
            toolZoomOut.Enabled = true;
            toolZoomOut.Checked = false;
            button1.Enabled = true;
            
        }

        private void toolPan_Click(object sender, EventArgs e)
        {
            MyMap.ActiveTool = MapToolType.Pan;
            
            toolPan.Enabled = false;
            toolPan.Checked = true;
            toolSelect.Enabled = true;
            toolSelect.Checked = false;
            toolZoomIn.Enabled = true;
            toolZoomIn.Checked = false;
            toolZoomOut.Enabled = true;
            toolZoomOut.Checked = false;
            button1.Enabled = false;
           
        }

        private void toolZoomIn_Click(object sender, EventArgs e)
        {
            MyMap.ActiveTool = MapToolType.ZoomIn;

            toolZoomIn.Enabled = false;
            toolZoomIn.Checked = true;
            toolSelect.Enabled = true;
            toolSelect.Checked = false;
            toolPan.Enabled = true;
            toolPan.Checked = false;
            toolZoomOut.Enabled = true;
            toolZoomOut.Checked = false;
            button1.Enabled = false;
            
        }

        private void toolZoomOut_Click(object sender, EventArgs e)
        {
            MyMap.ActiveTool = MapToolType.ZoomOut;

            toolZoomOut.Enabled = false;
            toolZoomOut.Checked = true;
            toolSelect.Enabled = true;
            toolSelect.Checked = false;
            toolPan.Enabled = true;
            toolPan.Checked = false;
            toolZoomIn.Enabled = true;
            toolZoomIn.Checked = false;
            button1.Enabled = false;
            
        }
    

      

      

        private void toolZoomAll_Click_1(object sender, EventArgs e)
        {
            MyMap.ZoomAll();
        }

       

    
    }
}