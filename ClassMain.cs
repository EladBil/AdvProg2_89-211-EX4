using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    class ClassMain
    {

        public static void Main()
        {
             MyTelnetFlightGear c = new MyTelnetFlightGear();
             c.Connect("127.0.0.1", 11000);
             c.Write("hii my friend");
            c.Read();
            c.Disconnect();
          

        }


    }   
}
