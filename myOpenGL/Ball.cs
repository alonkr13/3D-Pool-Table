using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace OpenGL
{
    class Ball
    {
        public Vector _x;
        public Vector _y;
        public float _locationX;
        public float _locationY;
        public float _ballRadius = 0.25f;
        public Double angle;


        public Ball()
        {
            _x = new Vector(0);
            _y = new Vector(0);
            angle = Math.Sqrt(Math.Pow(_x.b,2) + Math.Pow(_y.b,2));
        }//constructor

        public Ball(int inY)
        {
            _x = new Vector(50);
            _y = new Vector(145 + inY * 60);
            angle = Math.Sqrt(Math.Pow(_x.b, 2) + Math.Pow(_y.b, 2));
        }//constructor

    }
}
