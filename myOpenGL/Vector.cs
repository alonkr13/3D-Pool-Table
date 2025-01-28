using System;
using System.Collections.Generic;
using System.Text;

namespace OpenGL
{
    class Vector
    {
        public double a, b, c;
        public Vector(double aIn)
        {
            a = aIn;
            b = 0;
            c = 0;
        }//constructor 1


        public Vector(double aIn, double bIn , double cIn )
        {
            a = aIn;
            b = bIn;
            c = cIn;
        }//constructor 2
    }
}
