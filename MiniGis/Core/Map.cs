using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



using System.Reflection;

namespace MiniGis.Core
{
    public partial class Map : UserControl
    {
        private List<Layer> _layers = new List<Layer>();
        private int _snap = 10;

        public Node node1 = new Node(0, 0);


        public double valueT;

        Grid testgrid;

        public Bounds Bounds
        {
            get
            {
                Bounds bounds = new Bounds();
                foreach (VectorLayer layer in _layers)
                {
                    bounds = Bounds.Union(bounds, layer.Bounds);
                }
                return bounds;
            }  
        }

        public Map()
        {
            this.MouseWheel += Map_MouseWheel;
            testgrid = new Grid(5, 100, 51, 5, 5);  
            (testgrid as Grid).GenerateSurface();
            Polygon polygon = new Polygon();
            polygon.AddNode(5, 5);
            polygon.AddNode(500, 5);
            polygon.AddNode(5, 255);
            VectorLayer vectorlayer = new VectorLayer();
            vectorlayer.AddObject(polygon);

         
            AddLayer(vectorlayer);
            AddLayer(testgrid);

             



            InitializeComponent();
            //   Line line = new Line(0,0,0,0);
            //   this.onSelects += line.ClearSelections;

        }

        private void Map_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                MapScale *= 1.5;
            }
            else
            {
                MapScale /= 1.5;
            }
        }

        private Node _center = new Node(0, 0);
        public Node Center
        {
            get
            {
                return _center;
            }
            set
            {
                _center = value;
            }
        }

        private double _mapScale = 1;
        public double MapScale
        {
            get
            {
                return _mapScale; 
            }
            set
            {
                _mapScale = value;
                Invalidate();
            }
        }

        public void AddLayer(Layer layer)
        {
            _layers.Add(layer);
            layer.Map = this;
        }

        public void RemoveLayer(int index)
        {
            _layers.RemoveAt(index);
        }

        public void ClearLayers()
        {
            _layers.Clear();
        }

        public Layer GetLayer(int index)
        {
            return _layers[index];
        }

        public int LayerCount
        {
            get
            {
                return _layers.Count();
            }
        }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            for (int i = LayerCount - 1; i >= 0; i--)
            {
                Layer layer = GetLayer(i);
                layer.Paint(e);
            }

        }

        public System.Drawing.Point MapToScreen(Node point)
        {
            double x = (point.X - _center.X) * _mapScale + Width / 2;
            double y = -(point.Y - _center.Y) * _mapScale + Height / 2;
            return new System.Drawing.Point((int)Math.Round(x), (int)Math.Round(y)); //округление до целого
        }

        public Node ScreenToMap(System.Drawing.Point point)
        {
            double x = (point.X - Width / 2) / _mapScale + _center.X;
            double y = -(point.Y - Height / 2) / _mapScale + _center.Y;
            return new Node(x, y); //округление до целого
        }
        private void Map_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
        private MapToolType _activeTool = MapToolType.Select;
        public MapToolType ActiveTool
        {
            get { return _activeTool; }
            set
            {
                _activeTool = value;
                switch (ActiveTool)
                {
                    case MapToolType.Select:

                        //Cursor = new Cursor("C:\\Alternate_Select.cur");
                        //Cursor = new Cursor("Cursors\\Select.cur");
                        Cursor = Cursors.Arrow;
                        break;
                    case MapToolType.Pan:
                        Cursor = new Cursor("Cursors\\Pan.cur");
                        break;
                    case MapToolType.ZoomIn:
                        Cursor = new Cursor("Cursors\\Zoom_in.cur");
                        break;
                    case MapToolType.ZoomOut:
                        Cursor = new Cursor("Cursors\\Zoom_out.cur"); ;
                        break;
                }
            }
        }
        private System.Drawing.Point _mouseDownPosition = new System.Drawing.Point(0, 0);
        private bool _isMouseDown = false;
        private void Map_MouseDown(object sender, MouseEventArgs e)
        {
            _isMouseDown = true;
            _mouseDownPosition = e.Location;
            switch (ActiveTool)
            {
                case MapToolType.Select:
                    break;
                case MapToolType.Pan:
                    break;
                case MapToolType.ZoomIn:

                    break;
                case MapToolType.ZoomOut:
                    break;
            }

        }
        Core.Layer layer = new VectorLayer();
        private void Map_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;
            System.Drawing.Point zoomCenter = new System.Drawing.Point(
                        (_mouseDownPosition.X + e.Location.X) / 2,
                        (_mouseDownPosition.Y + e.Location.Y) / 2);
            double _width = Math.Abs(e.Location.X - _mouseDownPosition.X);
            double _height = Math.Abs(e.Location.Y - _mouseDownPosition.Y);
            double k = 1;
            switch (ActiveTool)
            {
                case MapToolType.Select:
                    int dx;
                    int dy;
                    dx = Math.Abs(_mouseDownPosition.X - e.Location.X);
                    dy = Math.Abs(_mouseDownPosition.Y - e.Location.Y);
                    if ((dx > _snap) || (dy > _snap))
                    {
                        return;
                    }
                    DoSelect(e);
                    break;
                case MapToolType.Pan:

                    Center.X += -(e.Location.X - _mouseDownPosition.X) / MapScale;
                    Center.Y -= -(e.Location.Y - _mouseDownPosition.Y) / MapScale;

                    Invalidate();
                    break;
                case MapToolType.ZoomIn:
                    Center = ScreenToMap(zoomCenter);
                    if (_width == 0 && _height == 0)
                    {
                        k = 2;
                    }
                    else if (_width == 0)
                    {
                        k = Height / _height;
                    }
                    else if (_height == 0)
                    {
                        k = Width / _width;
                    }
                    else if (_width / Width > _height / Height)
                    {
                        k = Width / _width;
                    }
                    else if (_width / Width < _height / Height)
                    {
                        k = Height / _height;
                    }
                    else
                    {
                        k = 1;
                    }
                    _mapScale *= k;
                    // Graphics gr = CreateGraphics();
                    // gr.DrawRectangle(new Pen(Color.Black), _mouseDownPosition.X, _mouseDownPosition.Y, e.Location.X - _mouseDownPosition.X, e.Location.Y - _mouseDownPosition.Y);
                    // Invalidate();


                    Invalidate();
                    break;
                case MapToolType.ZoomOut:
                    Center = ScreenToMap(zoomCenter);
                    if (_width == 0 && _height == 0)
                    {
                        k = 2;
                    }
                    else if (_width == 0)
                    {
                        k = Height / _height;
                    }
                    else if (_height == 0)
                    {
                        k = Width / _width;
                    }
                    else if (_width / Width > _height / Height)
                    {
                        k = Width / _width;
                    }
                    else if (_width / Width < _height / Height)
                    {
                        k = Height / _height;
                    }
                    else
                    {
                        k = 1;
                    }
                    _mapScale /= k;
                    Invalidate();
                    break;
            }
        }
        //Find an object,Выделение объекта
        private void DoSelect(MouseEventArgs e)
        {
            try
            {
                if (!Control.ModifierKeys.HasFlag(Keys.Control))
                {
                    ClearSelections();
                }
                const int ex = 2;
                Node searchpoint = ScreenToMap(e.Location);
                MapObject foundobject = FindObject(searchpoint, ex / MapScale);
                if (foundobject == null)
                {
                    return;
                }
                if (!Control.ModifierKeys.HasFlag(Keys.Control))
                {
                    foundobject.Selected = true;
                }
                else
                {
                    foundobject.Selected = !foundobject.Selected;
                }

            }
            finally
            {
                Invalidate();
            }

        }

       
      


        ////public delegate void ClearSelections();
        //// public event ClearSelections onSelects;
        private MapObject FindObject(Node searchpoint, double quad)
        {
            MapObject foundobject;
            for (int i = _layers.Count - 1; i >= 0; i--)
            {
                if (_layers[i] is VectorLayer)
                {
                    foundobject = (_layers[i] as VectorLayer).FindObject(searchpoint, quad);
                    if (foundobject != null)
                    {
                        return foundobject;
                    }
                }
            }
            

            return null;
        }

        public void GetColor(Color color)
        {

            if (_layers[0] is VectorLayer)
            {
                MapObject mapObject =( _layers[0] as VectorLayer)._objects[2];
                (_layers[0] as VectorLayer)._objects[2].OwnPen = new Pen(color);
                MapObject mapObject2 = (_layers[0] as VectorLayer)._objects[2];
                Invalidate();
            }
        }
        private void ClearSelections()
        {
            for (int i = _layers.Count - 1; i >= 0; i--)
            {
                if (_layers[i] is VectorLayer)
                {

                   ( _layers[i] as VectorLayer ).ClearSelectedObjects();

                }


            }
          }



        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            switch (ActiveTool)
            {
                case MapToolType.Select:
                    break;
                case MapToolType.Pan:
                    if (_isMouseDown == true)
                    {
                        Center.X += -(e.Location.X - _mouseDownPosition.X) / MapScale;
                        Center.Y -= -(e.Location.Y - _mouseDownPosition.Y) / MapScale;
                        _mouseDownPosition = e.Location;
                        Invalidate();
                    }
                    break;
                case MapToolType.ZoomIn:

                    Graphics gr = CreateGraphics();
                    if (_isMouseDown == true)
                    {
                        gr.DrawRectangle(new Pen(Color.Black), _mouseDownPosition.X, _mouseDownPosition.Y, e.Location.X - _mouseDownPosition.X, e.Location.Y - _mouseDownPosition.Y);
                        Refresh();
                    }


                    break;
                case MapToolType.ZoomOut:
                    break;
            }
        }
        public void ZoomAll()
        {
            Center.X = (Bounds.Xmax + Bounds.Xmin) / 2;
            Center.Y = (Bounds.Ymax + Bounds.Ymin) / 2;

            double _width = Math.Abs(Bounds.Xmax - Bounds.Xmin);

            double _height = Math.Abs(Bounds.Ymax - Bounds.Ymin);

            double k = 1;
          
          
            if (_width == 0 && _height == 0)
            {
                k = 2;
            }
            else if (_width == 0)
            {
                k = Height / _height;
            }
            else if (_height == 0)
            {
                k = Width / _width;
            }
            else if (_width / Width > _height / Height)
            {
              //  k = Width / _width;
                k = _width / Width;
            }
            else if (_width / Width < _height / Height)
            {
              //  k = Height / _height;
                k = _height / Height;
            }
            else
            {
                k = 1;
            }
            _mapScale =1;
            MapScale /= k;
            Invalidate();

        }

        private void Map_MouseClick(object sender, MouseEventArgs e)
        {
            node1 = ScreenToMap(e.Location);
             
            double value1 = 1;        
            





        }
    }
} 