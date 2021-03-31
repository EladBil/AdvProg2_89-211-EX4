using System;
using System.Collections.Generic;
using System.Text;




using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Runtime.InteropServices;

namespace model
{
    partial class Model : IModel
    {
        private string fileCsv;
        private string fileAPI;


        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr CreateTs(string name);



        /// 
        public int LoadingAPI(string fileAPI)
        {
            this.fileAPI = String.copfileAPI;
            string fileAPI = String.Copy(fileAPI);
            return 0;
        }
        public int LoadingCSV(string fileAPI)
        {
            return 0;
        }
        //Frame getFrame();
        public void SetIndexFrame(int index)
        {

        }
        public int GetIndexFrame()
        {
            return 0;
        }
        public int GetNumberRows()
        {
            return 0;
        }
        //get speed of flight
        public float GetRefreshRate()
        {
            return 0;
        }
        //set speed of flight
        public void SetRefreshRate(float speed)
        {

        }
        /*
         * get most cor receives a field (thats on a csv) and returns which one of the other fields are the most corralitive from the 
         * begining to end based
         * on the pearson.
         * */
        public string GetMostCor(string givenIndex)
        {
            return "ye";
        }
        public int[,] GetRowOfPoint(string graph)
        {
            int[,] array2D = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
            return array2D;
        }
        public Line lineReg(string value1 , string value2)
        {
            return new Line();
        }
    }


}


