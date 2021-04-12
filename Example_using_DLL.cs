/*
 * Practice. uncomment the section you would like to try
 */
using System;
using System.Runtime.InteropServices;
namespace AP2
{
    class Program
    {
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
        public static void Main(string[] args)
        {
            /*
            Console.WriteLine("1");
            IntPtr ts = createTS("reg_flight.csv");
            Console.WriteLine("2");

            //getting amount of attributes
            int size = TsGetAttributesSize(ts);
            //getting size of colomn
            int colSize = TsGetColSize(ts);
            Console.WriteLine("Row Size: " + size);
            Console.WriteLine("Col Size: " + colSize);
            */




            /*
            //using getrow to print row 5
            IntPtr row = TsGetRow(ts, 5);
            for (int i = 0; i < size; i++)
            {
                Console.Write(TsGetInRow(row, i) + ", ");
            }
            TsDeleteRow(row);
            Console.WriteLine();
            //end of getRow
            */

            /*
            //getColomn works the same way
            IntPtr col = TsGetColByIndex(ts, 16);
            for (int i = 0; i < colSize; i++)
            {
                Console.Write(TsGetInRow(col, i) + ", ");
            }
            TsDeleteRow(col);
            Console.WriteLine();
            //end of colomn
            */

            /*
            //pearson example
            Console.WriteLine("Pearson: " + pearsonFromColIndex(ts, 0, 1));
            //avg
            Console.WriteLine("avg: " + avgFromColIndex(ts, 1));
            //var
            Console.WriteLine("var: " + varFromColIndex(ts, 1));
            //cov
            Console.WriteLine("cov: " + covFromColIndex(ts, 0, 1));
            */


            /*
            //Simple Anomaly detector

            //creating ts's for normal and detect
            IntPtr t1 = createTS("train.csv");
            IntPtr t2 = createTS("test.csv");
            //created simple anomaly detctor
            IntPtr sad = createSad();
            SadLearnNormal(sad, t1);
            IntPtr anoms = SadDetect(sad, t2);
            int sizeAnoms = reportSize(anoms);
            Console.WriteLine("There are " + sizeAnoms + " anomalies");
            long[] anomalies = new long[sizeAnoms];
            for (int i = 0; i< sizeAnoms; i++)
            {
                //saving the anomalies one by one into array in c#
                anomalies[i] = reportGetAtIndex(anoms, i);
                //printing the anomaly
                Console.Write(anomalies[i] + ", ");
            }
            Console.WriteLine();
            reportDelete(anoms);
            SadDelete(sad);
            TsDelete(t1);
            TsDelete(t2);
            //end simple anomaly detector
            */


            /*
            
            //Hybrid Anomaly Detector example


            //creating ts's for normal and detect
            IntPtr t1 = createTS("train.csv");
            IntPtr t2 = createTS("test.csv");
            //created hybrid anomaly detctor
            IntPtr had = createHad();
            HadLearnNormal(had, t1);
            IntPtr anoms = HadDetect(had, t2);
            int sizeAnoms = reportSize(anoms);
            Console.WriteLine("There are " + sizeAnoms + " anomalies");
            long[] anomalies = new long[sizeAnoms];
            for (int i = 0; i < sizeAnoms; i++)
            {
                //saving the anomalies one by one into array in c#
                anomalies[i] = reportGetAtIndex(anoms, i);
                //printing the anomaly
                Console.Write(anomalies[i] + ", ");
            }
            Console.WriteLine();
            reportDelete(anoms);
            HadDelete(had);
            TsDelete(t1);
            TsDelete(t2);

            //end Hybrid anomaly detector

            */
            Console.WriteLine("end");
        }
        
    }
}
