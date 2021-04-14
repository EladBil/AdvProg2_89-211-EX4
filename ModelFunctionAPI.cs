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
using System.Net.Sockets;

namespace FlightSimADVProg2_ex1.Model
{
    class MiliSecondAndRate
    {
        /// <summary>
        /// MiliSeconds: Several milliseconds of the delay to the start function
        /// Rate: Number of times per second sent to fg
        /// </summary>
        /// <param name="MiliSeconds">Several milliseconds of the delay to the start function</param>
        /// <param name="Rate"> Number of times per second sent to fg</param>
        public MiliSecondAndRate(int MiliSeconds, int Rate)
        {
            this.MiliSeconds = MiliSeconds;
            this.Rate = Rate;
        }
        public int MiliSeconds { get; set; }
        public int Rate { get; set; }

       
    }
    /// <summary>
    /// For convenience we have divided the class of the model into two separate parts
    /// One section is responsible for maintaining the latest flight variables and updating them on listeners in the file "ModelFunctionBasic"
    /// And part two is responsible for the functions of the model, implemented in the file "ModelFunctionAPI"
    /// 
    /// Part of the Function class model
    /// In this part of the model class all the logic of the model as a whole is realized
    /// Create timeseries
    /// Receive a settings file
    /// Connecting to flight gear
    /// Complex calculations for certain requests made on flight data
    /// As well as running and controlling the rate of change of flight call data
    /// </summary>
    partial class MyModel : IModel
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
        private string fileCSV = "";
        /// <summary>
        /// Holds the number of rows (without the attribute names) of the csv file
        /// </summary>
        private int countRows = 0;

        private ITelnetClient telnetClientFlightGear;
        
        /// <summary>
        /// Holds the IP of the flight gear
        /// </summary>
        private string ip = "127.0.0.1";
       
        /// <summary>
        /// property of ip
        /// </summary>
        public string IP
        {
            get { return this.ip; }
            set
            {
                this.ip = new string(value);          
            }
        }
        /// <summary>
        /// Holds the port of the flight gear
        /// </summary>
        private int port = 0;
        /// <summary>
        /// Holds the pointer to the current vector (line) that we use in the file
        /// At first it is initialized to line 0
        /// </summary>
        private float[] arrayRowNow;
        /// <summary>
        /// The dictionary "speedToMilliseconds" links the number of speeds to the number of milliseconds 
        /// that the thread will rest between each pull of a row from timeseries
        /// </summary>
        private Dictionary<float, MiliSecondAndRate> speedToMillisecondAndRate;


        /// <summary>
        /// "DictValuesToNumInCsv" - map between the values and their column number in the csv file
        /// </summary>
        private Dictionary<string, int> DictValuesToNumInCsv;


        /// <summary>
        /// Pointer to timeseries that the current file holder holds
        /// </summary>
        private TimeSeriesModel ts;
        /// <summary>
        /// The stop variable determines whether the "start()" function will stop or continue
        /// </summary>
        private Boolean stop;
        /// <summary>
        /// property for stop
        /// </summary>
        public Boolean Stop
        {
            set
            {
                this.stop = value;
            }

        }



        /// <summary>This method uppload file API and create dictionary 
        /// Which maps between values and numbers.
        /// Uses the "Scattering.CreatestringOfValues" function to extract the data from the file and insert it into the string
        /// Uses the "Scattering.CreateDictionaryFromStringCSV" function  to create dictionary from string
        /// </summary>
        /// <param name="pathFileAPI">the path of file API.</param>
        /// <returns> 
        /// 0 - success 
        /// -1 - failure
        /// </returns>
        ///
        public int LoadingAPI(string pathFileAPI)
        {
            try
            {
                this.fileAPI = pathFileAPI;
                this.firstLine = Scattering.CreatestringOfValues(pathFileAPI);
                this.DictValuesToNumInCsv = new Dictionary<string, int>();
                //this variable Will be inserted correctly into our csv files within the attributes bar
                this.firstLine = Scattering.CreateDictionaryFromStringCSV(this.DictValuesToNumInCsv, this.firstLine);
            }
            catch
            {
                Console.WriteLine("Failed to load API file");
                this.fileAPI = "";
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// Upload a csv file to explore
        /// Uses the "Scattering.AddFirstLineInCsv" function to write the line of attributes into the file
        /// In addition creates a pointer to timeseries
        /// </summary>
        /// <param name="PathFileCSV">The path of the file</param>
        /// <returns>
        /// 0 - success 
        /// -1 - failure
        /// </returns>
        public int LoadingCSV(string PathFileCSV)
        {
            if (this.fileAPI.Equals(""))
            {
                Console.WriteLine("An API file must be uploaded");
                return -1;
            }
            try
            {
                //Create a new file that we can work on
                //The reason is that we do not want to destroy the file that the user gave us
                this.fileCSV = PathFileCSV;
                string data = "";
                File.WriteAllText(this.fileCsvToWork, data);

                Scattering.AddFirstLineInCsv(PathFileCSV, this.fileCsvToWork, this.firstLine);
                //create timeseries
                this.ts = new TimeSeriesModel(this.fileCsvToWork);//ticreateTS(this.fileCsvToWork);
                //Updates the number of rows
                this.countRows = this.ts.AmountLines();
                return 0;
            }
            catch
            {
                this.fileCSV = "";
                Console.WriteLine("Failed to load csv file");
                return -1;

            }
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
            if (this.speedToMillisecondAndRate.ContainsKey(speed))
            {
                this.speed = speed;
                this.millisecondsTimeout = this.speedToMillisecondAndRate[speed].MiliSeconds;
            }

        }


        /// <summary>
        /// The method converts the vector pointer to a float list
        /// </summary>
        /// <param name="p">pointer to vector of float from dll</param>
        /// <param name="size"> the size of vector</param>
        /// <returns>list of float</returns>

        public List<float> GetValuesSpecificAttribute(string attribute)
        {
            int numberCol = this.DictValuesToNumInCsv[attribute];

            return ts.GetValuesSpecificAttribute(numberCol);
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
                if (attribute2.Key.Equals(givenIndex))
                {
                    continue;
                }
                tempCor = ts.pearsonFromColIndex(atrribute1, attribute2.Value);
                if (Math.Abs(tempCor) >= Math.Abs(corMost))
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


            return ts.lineReg(x, y);
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
            return this.arrayRowNow[detail];

        }



        /// <summary>
        /// Calculate the anomalies using dll functions 
        /// </summary>
        /// <param name="learnCSV">A path of a file for learning the algorithm</param>
        /// <returns>list of anomalies </returns>
        public List<int> DetectAnomaly(string learnCSV)
        {
            if (this.fileCSV.Equals(""))
            {
                Console.WriteLine("The appropriate files must be uploaded to boot the model");
                return new List<int>();
            }
            string learnHibridCsvToWork = "learnAdCsvToWork.csv";
            //create ts to learnNormalCsv
            /* var lineCount = 0;
             using (var reader = File.OpenText(learnCSV))
             {
                 while (reader.ReadLine() != null)
                 {
                     lineCount++;
                 }
             }*/
            //Create a new file to work on
            string data = "";
            File.WriteAllText(learnHibridCsvToWork, data);
            // Add the first row we created from the settings file while loading the csv
            Scattering.AddFirstLineInCsv(learnCSV, learnHibridCsvToWork, this.firstLine);
            //create timeseries
            TimeSeriesModel tsNormal = new TimeSeriesModel(learnHibridCsvToWork);

            //create AnomalyDetector
            AnomalyAd ad = new AnomalyAd();




            //The algorithm learns the normal file
            ad.LearnNormal(tsNormal);
            //The algorithm checks for anomalies in our file
            List<int> l = ad.Detect(this.ts);
            ad.DestroyAnomaly();
            return l;
        }
        /// <summary>
        /// Returns the list of relative speeds from the dictionary
        /// </summary>
        /// <returns>list of relative speeds</returns>
        public List<float> GetListOfspeeds()
        {

            return new List<float>(this.speedToMillisecondAndRate.Keys);
         
        }

        /// <summary>
        /// Connects to flight gear using telnetClientFlightGear
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public int Connect(string ip, int port)
        {
            this.ip = ip;
            this.port = port;

            this.telnetClientFlightGear.Connect(this.ip, this.port);
            return 0;

        }
        /// <summary>
        /// disconnect from flight gear
        /// </summary>
        public void Disconnect()
        {
            stop = true;
            telnetClientFlightGear.Disconnect();
        }
        /// <summary>
        /// Initiates a process by which it takes a line from the ts and sends
        /// it to flight gear and updates all the fields accordingly
        /// </summary>
        public void start()
        {
            //Check that the csv file is loaded
            if (this.fileCSV.Equals(""))
            {
                Console.WriteLine("The appropriate files must be uploaded");
                return;
            }
            //Test that we are connected to flight gear
            //If we are not connected we will activate the start2 function
            if (!this.telnetClientFlightGear.isConnected())
            {
                start2();
                return;
            }
            //Change the stop
            this.stop = false;
            //Checking on which line we are in should not be an exception
            if (IndexFrame >= this.countRows)
            {
                stop = true;

            }
            try
            {

                //Creating a thread for parallel work with the model
                //In order for both the functions of the model and the sender to work smoothly    
                new Thread(delegate ()
                {

                    while (!stop)
                    {
                        string line;
                        //Obtaining the relevant set of variables to index frame
                        this.arrayRowNow = ts.TsGetRow(IndexFrame);
                        // convert vectorFloat from dll to arrayFloat in c#
                        this.updateAllattributes(arrayRowNow);
                        line = String.Join(",", arrayRowNow);
                        //add \r\n to send to flight gear
                        line += "\r\n";
                        //write to telnetClientFlightGear
                        this.telnetClientFlightGear.Write(line);
                        //Add 1 to the frame index
                        IndexFrame++;
                        //Adherence to the pace required by the user
                        Thread.Sleep(this.millisecondsTimeout);// read the data in 4Hz
                        //Check if we have reached the end of the frames
                        if (IndexFrame >= this.countRows)
                        {
                            stop = true;

                        }
                    }

                }).Start();


            }
            catch (SocketException)
            {
                this.start2();
            }


        }
        /// <summary>
        /// This function is just like the start function but without a socket connection
        /// Used as a backup when the socket falls or is not connected and we still want to wash the app but without flight gear
        /// </summary>
        public void start2()
        {
            
            if (this.fileCSV.Equals(""))
            {
                Console.WriteLine("The appropriate files must be uploaded");
                return;
            }
            this.stop = false;
            if (IndexFrame >= this.countRows)
            {
                stop = true;

            }
            new Thread(delegate ()
            {
                while (!stop)
                {


                    this.arrayRowNow = ts.TsGetRow(IndexFrame);
                    // convert vectorFloat from dll to arrayFloat in c#
                    this.updateAllattributes(arrayRowNow);
                    string line;
                    line = String.Join(",", arrayRowNow);
                    line += "\r\n";



                    IndexFrame++;

                    Thread.Sleep(this.millisecondsTimeout);// read the data in 4Hz

                    if (IndexFrame >= this.countRows)
                    {
                        stop = true;

                    }

                }
               


            }).Start();


        }

    }

}

