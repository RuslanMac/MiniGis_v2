using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MiniGis.Core
{
    class Grid : Layer
    {
        public double[,] matrix;

        public double cell;

        private int _count_X; 

        private int _count_Y; 

        private double _Min_X;

        private double _Max_X;

        private double _Min_Y;

       private double _Max_Y;


        public Grid(double cellt, int count_X, int count_Y, int Min_X, int Min_Y) 
        {
            // расстояние d
            cell = cellt;
            _count_X = count_X;
            _count_Y = count_Y;
            _Min_X = Min_X;
            _Min_Y = Min_Y;
            _Max_X = count_X * cell;
            _Max_Y = count_Y * cell;
            matrix = new double[_count_X, _count_Y];

            _bounds = new Bounds(_Min_X, _Min_Y, _Max_X, _Max_Y);

        }
        public override void Paint(PaintEventArgs e)
        {

            Color colormin = Color.Red;
            Color colormax = Color.Green;
            double minZ = matrix[0, 0];
            double maxZ = matrix[0, 0];
            for (int i=0;  i< _count_X; i++)
            {
                for (int j=0; j< _count_Y; j++)
                {
                    if (matrix[i,j] < minZ)
                    {
                        minZ = matrix[i, j];
                    }
                    if (matrix[i,j] > maxZ)
                    {
                        maxZ = matrix[i, j];
                    }
                }
            }

            Bitmap bitmap = new Bitmap(_count_X, _count_Y);
            byte R;
            byte G;
            byte B;
            for (int i=0; i< _count_X; i++) 
            {
                for (int j=0; j<    _count_Y; j++)   
                {


                    R = (byte)((colormax.R - colormin.R) / (maxZ - minZ) * (matrix[i, j] - minZ) + colormin.R);
                    G = (byte)((colormax.G - colormin.G) / (maxZ - minZ) * (matrix[i, j] - minZ) + colormin.G); 
                    B = (byte)((colormax.B - colormin.B) / (maxZ - minZ) * (matrix[i, j] - minZ) + colormin.B); 
                    bitmap.SetPixel(i,j, Color.FromArgb(R, G, B));
                }
            }
            e.Graphics.DrawImage(bitmap, new Rectangle(Map.MapToScreen(new Node(_Min_X, _Min_Y)), new Size((int)((_Max_X - _Min_X)* Map.MapScale), - (int)((_Max_Y - _Min_Y)*Map.MapScale))));
           // throw new NotImplementedException();
        }

        

        public void GenerateSurface()
        {
            double a = 100;
            for (int i=0; i< _count_X; i++)
            {
                for (int j=0; j< _count_Y; j++)
                {
                    matrix[i, j] = Math.Sin(a * i) * Math.Sin(a * j)*a;
                }
            }

        }


    }
}
