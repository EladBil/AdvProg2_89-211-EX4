using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    /*  class Point
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
      }*/



    class Line
    {

        private float a, b;
        public Line(float a, float b)
        {
            this.a = a;
            this.b = b;
        }

        public float A
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
            }

        }

        public float B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
            }

        }


        public float F(float x)
        {
            return a * x + b;
        }
    }




}
