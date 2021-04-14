using System;
using System.Collections.Generic;
using System.Text;

namespace FlightSimADVProg2_ex1.Model
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


    /// <summary>
    /// The class is needed to create a regression line between variables
    /// The line contains a gradient (a) and a constant (b)
    /// </summary>
    public class Line
    {

        private float a, b;
        public Line(float a, float b)
        {
            this.a = a;
            this.b = b;
        }
        /// <summary>
        /// property
        /// </summary>
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

        /// <summary>
        /// Enter x to calculate the y on the line
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float F(float x)
        {
            return a * x + b;
        }
    }

}
