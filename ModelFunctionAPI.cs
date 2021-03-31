using System;
using System.Collections.Generic;
using System.Text;




using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

namespace model
{
    partial class Model : IModel
    {
        //private string fileCsv;
        private int millisecondsTimeout = 100;
        private string fileCsv = "";
        private string fileCsvToWork = "fileCsvToWork.csv";

        private string fileAPI;
        private IntPtr ts;
        private int indexFrame = 0;

        /*
         * Importing all the functions from dll so we can use them
         */
        [DllImport("AP2Libraries.dll")]
        public static extern void HadDelete(IntPtr had);
        [DllImport("AP2Libraries.dll")]
        public static extern void SadDelete(IntPtr sad);
        [DllImport("AP2Libraries.dll")]
        public static extern void TsDelete(IntPtr ts);
        [DllImport("AP2Libraries.dll")]
        public static extern int reportSize(IntPtr report);
        [DllImport("AP2Libraries.dll")]
        public static extern int reportGetAtIndex(IntPtr report, int i);
        [DllImport("AP2Libraries.dll")]
        public static extern void reportDelete(IntPtr report);

        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr createSad();
        [DllImport("AP2Libraries.dll")]
        public static extern void SadLearnNormal(IntPtr Sad, IntPtr timeseries);
        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr SadDetect(IntPtr Sad, IntPtr timeseries);

        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr createHad();
        [DllImport("AP2Libraries.dll")]
        public static extern void HadLearnNormal(IntPtr Had, IntPtr timeseries);
        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr HadDetect(IntPtr Had, IntPtr timeseries);

        [DllImport("AP2Libraries.dll")]
        public static extern float covFromColIndex(IntPtr ts, int col1, int col2);


        [DllImport("AP2Libraries.dll")]
        public static extern float varFromColIndex(IntPtr ts, int col);

        [DllImport("AP2Libraries.dll")]
        public static extern float avgFromColIndex(IntPtr ts, int col);

        [DllImport("AP2Libraries.dll")]
        public static extern int TsGetColSize(IntPtr ts);
        [DllImport("AP2Libraries.dll")]
        public static extern float pearsonFromColIndex(IntPtr ts, int col1, int col2);
        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr TsGetColByIndex(IntPtr ts, int i);

        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr createTS(string CSVfileName);
        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr TsGetRow(IntPtr ts, int j);
        [DllImport("AP2Libraries.dll")]
        public static extern float TsGetInRow(IntPtr row, int i);
        [DllImport("AP2Libraries.dll")]
        public static extern int TsGetAttributesSize(IntPtr ts);
        [DllImport("AP2Libraries.dll")]
        public static extern void TsDeleteRow(IntPtr row);



        /// 
        public int LoadingAPI(string pathFileAPI)
        {

            return 0;
        }
        public int LoadingCSV(string PathFileCSV)
        {

            string data = "";
            string firstLine = "";
            File.WriteAllText(this.fileCsvToWork, data);
            Scattering.AddFirstLineInCsv(PathFileCSV, this.fileCsvToWork, firstLine);








            ts = createTS(fileAPI);

            return 0;
        }
        //Frame getFrame();
        public void SetIndexFrame(int index)
        {
            this.indexFrame = index;
        }
        public int GetIndexFrame()
        {
            return this.indexFrame;
        }
        public int GetNumberRows()
        {
            return TsGetColSize(this.ts);
        }
        //get speed of flight
        public float GetRefreshRate()
        {
            return this.millisecondsTimeout;
        }
        //set speed of flight
        public void SetRefreshRate(float speed)
        {
            if (speed == 0)
            {
                this.millisecondsTimeout = 200;
            }
            else if (speed == 0.5)
            {
                this.millisecondsTimeout = 150;
            }
            else if (speed == 1)
            {
                this.millisecondsTimeout = 100;
            }
            else if (speed == 1.5)
            {
                this.millisecondsTimeout = 50;
            }
            else if (speed == 2)
            {
                this.millisecondsTimeout = 20;
            }

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
        public Line lineReg(string value1, string value2)
        {
            return new Line();
        }



        public List<string> ListOfAttribute()
        {
            return new List<string>();////////////////////////////
        }




        /*
            * given an attribute it gives you the current info on the row
            * we are on now.
            * Should work like this:
            * You have IntPtr currentRow.
            * find index of attribute (int i)
            * call from DLL:
            * return TsGetInRow(currentRow, i);
            */
        public float GetDetailNow(string atrribute)
        {
            return 0;
        }

     
        /*
         * returns list of anomalies based off of reg (SAD) and circle (HAD)
         */

        public List<int> AnomalyReg()
        {
            return new List<int>();
        }
        public List<int> AnomalyCirc()
        {
            return new List<int>();
        }


    }

}


