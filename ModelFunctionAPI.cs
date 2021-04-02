using System;
using System.Collections.Generic;
using System.Text;




using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;

namespace model
{
    partial class Model : IModel
    {


        //private string fileCsv;
        /// <summary>
        /// Current speed level
        /// </summary>
        private float speed = 1;
        /// <summary>
        /// The delay time between pulling lines of the ts
        /// </summary>
        private int millisecondsTimeout = 100;
        /// <summary>
        /// The path of the csv file we get to explore
        /// </summary>
        private string fileCsv = "";
        /// <summary>
        /// The same file is sent to timeseries plus a row of attribute names
        ///In order not to change the current file
        /// </summary>
        private readonly string fileCsvToWork = "fileCsvToWork.csv";
        /// <summary>
        /// The first line we want to add to untitled files
        /// </summary>
        private string firstLine = "";
        /// <summary>
        /// Will hold the api file name
        /// </summary>
        private string fileAPI = "";
        /// <summary>
        /// Holds the number of existing attributes
        /// </summary>
        private int countAttribute = 0;
        /// <summary>
        /// Holds the number of rows (without the attribute names) of the csv file
        /// </summary>
        private int countRows = 0;
        /// <summary>
        /// Holds the IP of the flight gear
        /// </summary>
        private string ip = "";
        /// <summary>
        /// Holds the port of the flight gear
        /// </summary>
        private int port = 0;
        /// <summary>
        /// Holds the pointer to the current vector (line) that we use in the file
        /// At first it is initialized to line 0
        /// </summary>
        private IntPtr vectorRowNow;
        /// <summary>
        /// The dictionary "speedToMilliseconds" links the number of speeds to the number of milliseconds 
        /// that the thread will rest between each pull of a row from timeseries
        /// </summary>
        private Dictionary<float, int> speedToMilliseconds;

        
        /// <summary>
        /// "DictValuesToNumInCsv" - map between the values and their column number in the csv file
        /// </summary>
        private Dictionary<string, int> DictValuesToNumInCsv;


        /// <summary>
        /// Pointer to timeseries that the current file holder holds
        /// </summary>
        private IntPtr ts;

       

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



        /// <summary>This method uppload file API and create dictionary 
        /// Which maps between values and numbers.</summary>
        /// <param name="pathFileAPI">the path of file API.</param>
        ///
        public int LoadingAPI(string pathFileAPI)
        {
            this.fileAPI = pathFileAPI;
            this.firstLine = Scattering.CreatestringOfValues(pathFileAPI);
            this.DictValuesToNumInCsv = new Dictionary<string, int>();

            this.firstLine = Scattering.CreateDictionaryFromStringCSV(this.DictValuesToNumInCsv, this.firstLine);
          
            return 0;
        }
        /// <summary>
        /// Upload a csv file to explore
        /// </summary>
        /// <param name="PathFileCSV">The path of the file</param>
        /// <returns>
        /// 0 - success 
        /// -1 - failure
        /// </returns>
        public int LoadingCSV(string PathFileCSV)
        {

             string data = "";
            File.WriteAllText(this.fileCsvToWork, data);
            
            Scattering.AddFirstLineInCsv(PathFileCSV, this.fileCsvToWork, this.firstLine);

            ts = createTS(this.fileCsvToWork);
            this.vectorRowNow = TsGetRow(ts, 0);
            this.countRows = TsGetColSize(ts);
           
            this.countAttribute = TsGetAttributesSize(ts);

          
            return 0;
        }
        

        /// <summary>This method return number of rows of csv file Without the first line of attribute</summary>
        ///<returns>number of rows of csv file Without the first line of attribute</returns>
        public int GetNumberRows()
        {
            //return TsGetColSize(this.ts);
            return this.countRows;
        }
       
        /// <summary>
        /// return the speed of send Of sending the data
        /// Speed is milliseconds and the process stops between sending
        /// 
        /// </summary>
        /// <returns>int of Speed </returns>
        public float GetRefreshRate()
        {
            return this.speed;
        }
        //set speed of flight
        /// <summary>
        /// Sets the speed of data transmission
        /// Gets a number and converts it to the millisecond quantity to wait
        /// by dictionary
        /// </summary>
        /// <param name="speed"></param>
        public void SetRefreshRate(float speed)
        {
            if (this.speedToMilliseconds.ContainsKey(speed))
            {
                this.speed = speed;
                this.millisecondsTimeout = this.speedToMilliseconds[speed];
            }
           
        }


        /// <summary>
        /// The method converts the vector pointer to a float list
        /// </summary>
        /// <param name="p">pointer to vector of float from dll</param>
        /// <param name="size"> the size of vector</param>
        /// <returns>list of float</returns>
        private List<float> vectorFromIntPtrToList(IntPtr p  , int size)
        {

            List<float> l = new List<float>();
            for (int i = 0; i < size; i++)
            {
                l.Add(TsGetInRow(p, i));
               // Console.WriteLine(TsGetInRow(p, i));               
            }
            return l;
        }
        public List<float> GetValuesSpecificAttribute(string attribute)
        {
            int numberCol = this.DictValuesToNumInCsv[attribute];

            return vectorFromIntPtrToList(TsGetColByIndex(this.ts, numberCol), this.GetNumberRows());
        }

        /// <summary>
        /// get most cor receives a field (thats on a csv) and returns which one of the 
        ///other fields are the most corralitive from the 
        /// begining to end based
        /// on the pearson.
        /// </summary>
        /// <param name="givenIndex"></param>
        /// <returns>
        /// name of attribute most Correlative
        ///  if return -1 have problem</returns>

        public string GetMostCor(string givenIndex)
        {
            float corMost = 0;
            string nameOfMostCor = "-1";
            int atrribute1;
            //check if string in dictionary
            if (this.DictValuesToNumInCsv.ContainsKey(givenIndex))
            {
                atrribute1 = this.DictValuesToNumInCsv[givenIndex];
            }
            else
            {
                return "-1";
            }

            foreach (KeyValuePair<string, int> attribute2 in this.DictValuesToNumInCsv)
            {
                float tempCor = 0;
                //Skip the feature itself
                if (attribute2.Key.Equals(givenIndex)){
                    continue;
                }
                tempCor = pearsonFromColIndex(this.ts, atrribute1, attribute2.Value);
  
                if(Math.Abs(tempCor)>= Math.Abs(corMost))
                {
                    nameOfMostCor = attribute2.Key;
                    corMost = tempCor;
                }
            }
           
            return nameOfMostCor;
        }



        /// <summary>
        /// The function calculates the linear line between two variables by a formula
        ///In addition the function uses external functions of dll
        /// </summary>
        /// <param name="value1">string of name of first column of values</param>
        /// <param name="value2">string of name of second column of values</param>
        /// <returns>Line of reg</returns>
        public Line lineReg(string value1, string value2)
        {
           
            int x = this.DictValuesToNumInCsv[value1];
            int y = this.DictValuesToNumInCsv[value2];

            float a = covFromColIndex(this.ts ,x, y) / varFromColIndex(this.ts , x);
            float b = avgFromColIndex(this.ts, y) - a * avgFromColIndex(this.ts, x);

            return new Line(a, b);
        }
        /// <summary>
        /// GetListOfAttribute -  return list of Attributes (values) 
        /// Made by getting all the keys in the dictionary to list
        /// </summary>
        /// <returns>list of attributes</returns>
        public List<string> GetListOfAttribute()
        {

            return new List<string>(this.DictValuesToNumInCsv.Keys);

        }

        /// <summary>
        ///   given an attribute it gives you the current info on the row
        /// we are on now.
        /// </summary>
        /// <param name="atrribute">Name the attribute I am looking for its value</param>
        /// <returns>The value of the attribute in the appropriate location</returns>
        public float GetDetailNow(string atrribute)
        {

            int detail = this.DictValuesToNumInCsv[atrribute];
            return TsGetInRow(this.vectorRowNow, detail);

        }
        /*
         * returns list of anomalies based off of reg (SAD) and circle (HAD)
         */

        /// <summary>
        /// Calculate the anomalies using dll functions for regression
        /// </summary>
        /// <param name="learnNormalCsv">A path of a file for learning the algorithm</param>
        /// <returns>list of anomalies based off of reg (SAD)</returns>
        public List<int> AnomalyReg(string learnNormalCsv)
        {
            string learnNormalCsvToWork = "learnNormalCsvToWork.csv";
            //create ts to learnNormalCsv
            var lineCount = 0;
            using (var reader = File.OpenText(learnNormalCsv))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }
           



            string data = "";
            File.WriteAllText(learnNormalCsvToWork, data);

            Scattering.AddFirstLineInCsv(learnNormalCsv, learnNormalCsvToWork, this.firstLine);



            IntPtr tsNormal = createTS(learnNormalCsvToWork);
            //create list of time anomaly
            List<int> timeStepOfAnomaly = new List<int>();
            //create SimpleAnomalyDetector
            IntPtr sad = createSad();

            //The algorithm learns the normal file
            SadLearnNormal(sad, tsNormal);
            //The algorithm checks for anomalies in our file
            IntPtr vectorAnomalyReport = SadDetect(sad, this.ts);
            //size of vector reports
            int sizeOfVector = reportSize(vectorAnomalyReport);
            for(int i = 0; i< sizeOfVector;i++)
            {
                timeStepOfAnomaly.Add(reportGetAtIndex(vectorAnomalyReport, i));
            }

            SadDelete(sad);
            List<int> uniqueLst = timeStepOfAnomaly.Distinct().ToList();
            uniqueLst.Sort();
            return uniqueLst;
        }
        /// <summary>
        /// Calculate the anomalies using dll functions for Circle
        /// </summary>
        /// <param name="learnHibridCsv">A path of a file for learning the algorithm</param>
        /// <returns>list of anomalies based off of circle (HAD)</returns>
        public List<int> AnomalyCirc(string learnHibridCsv)
        {

            string learnHibridCsvToWork = "learnHibridCsvToWork.csv";
            //create ts to learnNormalCsv
            var lineCount = 0;
            using (var reader = File.OpenText(learnHibridCsv))
            {
                while (reader.ReadLine() != null)
                {
                    lineCount++;
                }
            }
            



            string data = "";
            File.WriteAllText(learnHibridCsvToWork, data);

            Scattering.AddFirstLineInCsv(learnHibridCsv, learnHibridCsvToWork, this.firstLine);

            //create ts to learnNormalCsv
            IntPtr tsNormal = createTS(learnHibridCsvToWork);
            //create list of time anomaly
            List<int> timeStepOfAnomaly = new List<int>();
            //create SimpleAnomalyDetector
            IntPtr had = createHad();

            //The algorithm learns the normal file
            HadLearnNormal(had, tsNormal);
            //The algorithm checks for anomalies in our file
            IntPtr vectorAnomalyReport = HadDetect(had, this.ts);
            //size of vector reports
            int sizeOfVector = reportSize(vectorAnomalyReport);
            for (int i = 0; i < sizeOfVector; i++)
            {
                timeStepOfAnomaly.Add(reportGetAtIndex(vectorAnomalyReport, i));
            }

            HadDelete(had);
            List<int> uniqueLst = timeStepOfAnomaly.Distinct().ToList();
            uniqueLst.Sort();
            return uniqueLst;
        }

       
    }

}


