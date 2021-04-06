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
    class AnomalyCirc: IAnomalAlgorithms
    {
     


        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr createHad();
        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr HadDetect(IntPtr Sad, IntPtr timeseries);
        [DllImport("AP2Libraries.dll")]
        public static extern int reportSize(IntPtr report);
        [DllImport("AP2Libraries.dll")]
        public static extern int reportGetAtIndex(IntPtr report, int i);
        [DllImport("AP2Libraries.dll")]
        public static extern void HadDelete(IntPtr sad);
        [DllImport("AP2Libraries.dll")]

        public static extern void HadLearnNormal(IntPtr Sad, IntPtr timeseries);





        private IntPtr had;

        public AnomalyCirc()
        {
            this.had = createHad();
        }
        public List<int> Detect(TimeSeriesModel ts)
        {
            
            List<int> timeStepOfAnomaly = new List<int>();
            IntPtr vectorAnomalyReport = HadDetect(this.had, ts.GetIntP());




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
      
            
            HadLearnNormal(this.had, ts.GetIntP());

            
        }

        public void DestroyAnomaly()
        {
            HadDelete(this.had);
        }







    }

    class AnomalyReg : IAnomalAlgorithms
    {
        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr createSad();
        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr SadDetect(IntPtr Sad, IntPtr timeseries);
        [DllImport("AP2Libraries.dll")]
        public static extern int reportSize(IntPtr report);
        [DllImport("AP2Libraries.dll")]
        public static extern int reportGetAtIndex(IntPtr report, int i);
        [DllImport("AP2Libraries.dll")]
        public static extern void SadDelete(IntPtr sad);
        [DllImport("AP2Libraries.dll")]

        public static extern void SadLearnNormal(IntPtr Sad, IntPtr timeseries);





        private IntPtr sad;
      
        public AnomalyReg()
        {
            this.sad = createSad();
        }
        public List<int> Detect(TimeSeriesModel ts)
        {
            
            List<int> timeStepOfAnomaly = new List<int>();
            IntPtr vectorAnomalyReport =   SadDetect(this.sad, ts.GetIntP());



       
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
            
            SadLearnNormal(this.sad, ts.GetIntP());
        }

        public void DestroyAnomaly()
        {
            SadDelete(this.sad);
        }





    }
}
