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
    /// <summary>
    /// this class is used to create a shell to calculate the anomalies
    /// We use dll files to calculate anomalies
    /// But we wanted to make a separation between the model and the dll files for this purpose
    /// so this department was built which uses the dll files for the purpose of transferring
    /// the information required to the model
    /// 
    /// The algorithm is dynamic, which means that the user can load any abnormality algorithm he is
    /// interested in, as long as the algorithm actually performs the functions.
    /// </summary>
    class AnomalyAd : IAnomalAlgorithms
    {
        //Statement of relevant dll functions
        const string myDll = "SadDLL.dll";

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




        /// <summary>
        ///   Holds a pointer to the anomaly detection object
        /// </summary>
        private IntPtr had;
        /// <summary>
        /// create Object Detection Object
        /// </summary>
        public AnomalyAd()
        {
            this.had = createAd();
        }
        /// <summary>
        /// The function calculates anomalies of the current 
        /// timeseries together with the timeseries of the learning
        /// </summary>
        /// <param name="ts">TimeSeriesModel Which holds the file for learn</param>
        /// <returns>list of timesteps anomaly </returns>
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

            //Sort and duplicate
            List<int> uniqueLst = timeStepOfAnomaly.Distinct().ToList();
            uniqueLst.Sort();


            return uniqueLst;
        }
        /// <summary>
        /// Performs learning on object timeseries
        /// </summary>
        /// <param name="ts">Name of timeseries</param>
        public void LearnNormal(TimeSeriesModel ts)
        {
          AdLearnNormal(this.had, ts.GetIntP());
        }
        /// <summary>
        /// Destruction of the anomaly detector object
        /// This is because the dll is written in c ++ and therefore does not have a garbage collector
        /// </summary>
        public void DestroyAnomaly()
        {
            AdDelete(this.had);
        }







    }





}