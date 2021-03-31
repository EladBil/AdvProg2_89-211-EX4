using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    class Point
    {
        float x;
        public float X
        {
            get { return x; }
            set
            {
                x = value;
               
            }
        }
        float y;
        public float Y
        {
            get { return y; }
            set
            {
                y = value;
            }
        }
    }



    class Line
    {
        List<Point> points = new List<Point>();
        
       
    }


}
