using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;


namespace model
{
    interface Model : INotifyPropertyChanged
    {
        
        //Frame getFrame();
        void setIndexFrame(int index);
        int getIndexFrame();
        int getNumberRows();
        float getRefreshRate();
        void setRefreshRate(float speed);
        /*
         * get most cor receives a field (thats on a csv) and returns which one of the other fields are the most corralitive from the 
         * begining to end based
         * on the pearson.
         * */
        string getMostCor(string givenIndex);

        //Line getRegres(






    }





    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
