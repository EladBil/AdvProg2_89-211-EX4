using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Linq;

namespace FlightSimADVProg2_ex1.Model
{


    interface IAnomalAlgorithms
    {


    }

    class AnomalyAd : IAnomalAlgorithms
    {
        const string myDll = "HadDLL.dll";

        [DllImport(myDll)]
        public static extern IntPtr createAd();
        [DllImport(myDll)]
        public static extern IntPtr AdDetect(IntPtr Sad, IntPtr timeseries);
        [DllImport(myDll)]
        public static extern int reportSize(IntPtr report);
        [DllImport(myDll)]
        public static extern int reportGetAtIndex(IntPtr report, int i);
        [DllImport(myDll)]
        public static extern void AdDelete(IntPtr sad);
        [DllImport(myDll)]

        public static extern void AdLearnNormal(IntPtr Sad, IntPtr timeseries);





        private IntPtr had;

        public AnomalyAd()
        {
            this.had = createAd();
        }
        public List<int> Detect(TimeSeriesModel ts)
        {

            List<int> timeStepOfAnomaly = new List<int>();
            IntPtr vectorAnomalyReport = AdDetect(this.had, ts.GetIntP());




            //size of vector reports
            int sizeOfVector = reportSize(vectorAnomalyReport);


            for (int i = 0; i < sizeOfVector; i++)
            {
                timeStepOfAnomaly.Add(reportGetAtIndex(vectorAnomalyReport, i));
            }


            List<int> uniqueLst = timeStepOfAnomaly.Distinct().ToList();
            uniqueLst.Sort();


            return uniqueLst;
        }
        public void LearnNormal(TimeSeriesModel ts)
        {


            AdLearnNormal(this.had, ts.GetIntP());


        }

        public void DestroyAnomaly()
        {
            AdDelete(this.had);
        }







    }





}
