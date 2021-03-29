using System;
using System.Collections.Generic;
using System.Text;

namespace model
{
    class ClassMain
    {

        public static void Main()
        {
            ITelnetClient telnetClientFlightGear = new MyTelnetFlightGear();

            Model m = new Model(telnetClientFlightGear);
            m.Connect("localhost", 11000);
            m.NameOfFile = "reg_flight.csv";
            m.start();
            //m.Disconnect();

            /*  MyTelnetFlightGear c = new MyTelnetFlightGear();
             c.Connect("localhost", 11000);
             c.Write("hii my friend");
             Console.WriteLine(c.Read());
             c.Disconnect();

             */
        }


    }   
}
