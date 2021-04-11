using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniGis.Core
{
    public class VectorLayer: Layer
    {
        public List<MapObject> _objects = new List<MapObject>();
       

        public MapObject this[int index]
        {
            get
            {
                return _objects[index];
            }
            set
            {
                _objects[index] = value;
            }
        }

      

        public void AddObject(MapObject obgect)
        {
            if (obgect.Layer != null)
                throw new Exception("объект уже добален в слой"); //не одволяет добавлять один и тотже объект в разные слои, тоже самое можно сделать с мапом
            _objects.Add(obgect);
            obgect.Layer = this;
        }

        public void RemoveObject(int index)
        {
            _objects.RemoveAt(index);
        }

        public void ClearObjects()
        {
            _objects.Clear();
        }

        public MapObject GetObject(int index)
        {
            return _objects[index];
        }

        public int ObjectCount
        {
            get
            {
                return _objects.Count();
            }
        }

        public override void Paint(PaintEventArgs e)
        {
           // здесь будет код рисования слоя
           for (int i = ObjectCount - 1; i >= 0; i--)
           {
                MapObject mapObject = GetObject(i);
                mapObject.Paint(e);
           }
        }
        //хранение границ (свойство)
     
        //обновление границ
        public void UpdateBounds()
        {
            Bounds bounds = new Bounds();
            foreach (MapObject obj in _objects)
            {
                bounds = Bounds.Union(bounds, obj.Bounds);
                _bounds = bounds;
            }

        }
        public void ClearSelectedObjects()
        {
            for (int i  = _objects.Count-1;i>=0;i--)
            {
                _objects[i].Selected = false;
            }
            
        }

        internal MapObject FindObject(Node searchpoint, double quad)
        {
            MapObject foundobject;
            for (int i = _objects.Count - 1; i >= 0; i--)
            {
                foundobject = _objects[i];
                if (foundobject.IsIntersects(searchpoint, quad))
                {
                    return foundobject;
                }
            }
            return null;
        }
        public Polygon FindFormingPolygons()
        {
            Polygon newPolygon1 = new Polygon();
            List<Polygon> polygons = new List<Polygon>();
            for (int i = _objects.Count-1;i>=0;i--)
            {
                if (_objects[i].Selected == true)
                {
                    polygons.Add((Core.Polygon)(_objects[i]));

                }
            }
            newPolygon1 = polygons[0];

           
            
            /*  newPolygon1=  Polygon.SumPolygons(polygons);
            if (newPolygon1 != null)
            {
                for (int i = _objects.Count - 1; i >= 0; i--)
                {
                    if (_objects[i].Selected == true)
                    {
                        _objects.RemoveAt(i);
                    }
                }
                return newPolygon1;
            }
            */

            return newPolygon1;
        }

       
    }
}
