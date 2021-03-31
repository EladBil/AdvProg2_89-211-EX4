using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Runtime.InteropServices;


namespace model
{
    interface IModel : INotifyPropertyChanged
    {

        //Frame getFrame();
        public int LoadingAPI(string fileAPI);
        public int LoadingCSV(string fileAPI);

        void SetIndexFrame(int index);
        int GetIndexFrame();
        int GetNumberRows();
        float GetRefreshRate();
        void SetRefreshRate(float speed);
        int[,] GetRowOfPoint(string graph);
        Line lineReg(string value1, string value2);
        /*
         * given an attribute it gives you the current info on the row
         * we are on now.
         * Should work like this:
         * You have IntPtr currentRow.
         * find index of attribute (int i)
         * call from DLL:
         * return TsGetInRow(currentRow, i);
         */
        float GetDetailNow(string atrribute);

        /*
         * list of attribute 
         * */
        List<string> ListOfAttribute();
        /*
         * returns list of anomalies based off of reg (SAD) and circle (HAD)
         */

        List<int> AnomalyReg();
        List<int> AnomalyCirc();



        /*
         * get most cor receives a field (thats on a csv) and returns which one of the other fields are the most corralitive from the 
         * begining to end based
         * on the pearson.
         * */
        string GetMostCor(string givenIndex);

        //Line getRegres(

        //flight control
        float Aileron { set; get; }
        float Elevator { set; get; }
        float Rudder { set; get; }
        float Flaps { set; get; }
        float Slats { set; get; }
        float Speedbrake { set; get; }

        //engines
        float Throttle_1 { set; get; }
        float Throttle_2 { set; get; }

        //Gear

        //Hydraulics
        float EnginePump_1 { set; get; }
        float EnginePump_2 { set; get; }
        float ElectriPump_1 { set; get; }
        float ElectriPump_2 { set; get; }

        //Electric
        float ExternalPower { set; get; }
        
        float APUGenerator { set; get; }

        //Autoflight

        //Position
        double LatitudeDeg { set; get; }
        double LongitudeDeg { set; get; }
        float AltitudeFt { set; get; }

        //Orientation
        float RollDeg { set; get; }
        float PitchDeg { set; get; }
        float HeadingDeg { set; get; }
        float SideSlipDeg { set; get; }

        //Velocities
        float AirspeedKt { set; get; }
        float Glideslope { set; get; }
        float VerticalSpeedFps { set; get; }

        //Accelerations
        //Surface Positions
        //instruments
        float AirspeedIndicatorIndicatedSpeedKt { set; get; }
        float AltimeterIndicatedAltitudeFt { set; get; }
        float AltimeterPressureAltFt { set; get; }
        float AttitudeIndicatorIndicatedPitchDeg { set; get; }
        float AttitudeIndicatorIndicatedRollDeg { set; get; }
        float AttitudeIndicatorInternalPitchDeg { set; get; }
        float AttitudeIndicatorInternalRollDeg { set; get; }
        float EncoderIndicatedAltitudeFt { set; get; }
        float EncoderPressureAltFt { set; get; }
        float GpsIndicatedAltitudeFt { set; get; }
        float GpsIndicatedGroundSpeedKt { set; get; }
        float GpsIndicatedVerticalSpeed { set; get; }
        float IndicatedHeadingDeg { set; get; }
        float MagneticCompassIndicatedHeadingDeg { set; get; }
        float SlipSkidBallIndicatedSlipSkid { set; get; }
        float TurnIndicatorIndicatedTurnRate { set; get; }
        float VerticalSpeedIndicatorIndicatedSpeedFpm { set; get; }
        float EngineRpm { set; get; }



    }





    partial class Model : IModel
    {
        //the function from dll
        


      
      

        public event PropertyChangedEventHandler PropertyChanged;

        ITelnetClient telnetClientFlightGear;
        volatile Boolean stop;
        public Model(ITelnetClient telnetClient)
        {
            this.telnetClientFlightGear = telnetClient;
            stop = false;

        }
        public void Connect(string ip, int port)
        {
            int i = -1;
            while (i == -1)
            {
                i = telnetClientFlightGear.Connect(ip, port);
            }
        }
        public void Disconnect()
        {
            stop = true;
            telnetClientFlightGear.Disconnect();
        }
        public void start()
        {
            //need take rows from th eime series wait to shmoel
            // IntPtr ts = CreateTs(this.fileCsv);

            new Thread(delegate ()
            {
                while (!stop)
                {



                    int counter = 0;
                    string line;

                    // Read the file and display it line by line.  
                    System.IO.StreamReader file =
                        new System.IO.StreamReader(fileCsv);
                    while ((line = file.ReadLine()) != null)
                    {
                        //System.Console.WriteLine(line);
                        telnetClientFlightGear.Write(line);
                        counter++;
                        Thread.Sleep(millisecondsTimeout);// read the data in 4Hz

                    }

                    file.Close();


                }
                telnetClientFlightGear.Write("enough");
            }).Start();
        }

        //Change the speed of sending data to flight gear
        public int MillisecondsTimeout
        {
            get { return millisecondsTimeout; }
            set
            {
                millisecondsTimeout = value;
            }
        }
        public string NameOfFile
        {
            get { return this.fileCsv; }
            set

            {
                //Maybe this is problematic because the pointer points to the same string//////////////////////////////////////////////
                this.fileCsv = value;
            }
        }







        //flight control
        private float aileron;
        public float Aileron
        {
            get { return aileron; }
            set
            {
                aileron = value;
                NotifyPropertyChanged("aileron");
            }
        }
        private float elevator;
        public float Elevator
        {
            get { return elevator; }
            set
            {
                elevator = value;
                NotifyPropertyChanged("elevator");
            }
        }
        private float rudder;
        public float Rudder
        {
            get { return rudder; }
            set
            {
                rudder = value;
                NotifyPropertyChanged("rudder");
            }
        }
        private float flaps;
        public float Flaps
        {
            get { return flaps; }
            set
            {
                flaps = value;
                NotifyPropertyChanged("flaps");
            }
        }
        private float slats;
        public float Slats
        {
            get { return slats; }
            set
            {
                slats = value;
                NotifyPropertyChanged("slats");
            }
        }
        private float speedbrake;
        public float Speedbrake
        {
            get { return speedbrake; }
            set
            {
                speedbrake = value;
                NotifyPropertyChanged("speedbrake");
            }
        }

        //engines
        private float throttle_1;
        public float Throttle_1
        {
            get { return throttle_1; }
            set
            {
                throttle_1 = value;
                NotifyPropertyChanged("throttle_1");
            }
        }
        private float throttle_2;
        public float Throttle_2
        {
            get { return throttle_2; }
            set
            {
                throttle_2 = value;
                NotifyPropertyChanged("throttle_2");
            }
        }

        //Gear

        //Hydraulics
        private float enginePump_1;
        public float EnginePump_1
        {
            get { return enginePump_1; }
            set
            {
                enginePump_1 = value;
                NotifyPropertyChanged("enginePump_1");
            }
        }
        private float enginePump_2;
        public float EnginePump_2
        {
            get { return enginePump_2; }
            set
            {
                enginePump_2 = value;
                NotifyPropertyChanged("enginePump_2");
            }
        }
        private float electriPump_1;
        public float ElectriPump_1
        {
            get { return electriPump_1; }
            set
            {
                electriPump_1 = value;
                NotifyPropertyChanged("electriPump_1");
            }
        }
        private float electriPump_2;
        public float ElectriPump_2
        {
            get { return electriPump_2; }
            set
            {
                electriPump_2 = value;
                NotifyPropertyChanged("electriPump_2");
            }
        }

        //Electric
        private float externalPower;
        public float ExternalPower
        {
            get { return externalPower; }
            set
            {
                externalPower = value;
                NotifyPropertyChanged("externalPower");
            }
        }
        private float apuGenerator;

        public float APUGenerator
        {
            get { return apuGenerator; }
            set
            {
                apuGenerator = value;
                NotifyPropertyChanged("apuGenerator");
            }
        }

        //Autoflight

        //Position
        private double latitudeDeg;
        public double LatitudeDeg
        {
            get { return latitudeDeg; }
            set
            {
                latitudeDeg = value;
                NotifyPropertyChanged("latitudeDeg");
            }
        }
        private double longitudeDeg;

        public double LongitudeDeg
        {
            get { return longitudeDeg; }
            set
            {
                longitudeDeg = value;
                NotifyPropertyChanged("longitudeDeg");
            }
        }
        private float altitudeFt;
        public float AltitudeFt
        {
            get { return altitudeFt; }
            set
            {
                altitudeFt = value;
                NotifyPropertyChanged("altitudeFt");
            }
        }

        //Orientation
        private float rollDeg;
        public float RollDeg
        {
            get { return rollDeg; }
            set
            {
                rollDeg = value;
                NotifyPropertyChanged("rollDeg");
            }
        }
        private float pitchDeg;
        public float PitchDeg
        {
            get { return pitchDeg; }
            set
            {
                pitchDeg = value;
                NotifyPropertyChanged("pitchDeg");
            }
        }
        private float headingDeg;
        public float HeadingDeg
        {
            get { return headingDeg; }
            set
            {
                headingDeg = value;
                NotifyPropertyChanged("headingDeg");
            }
        }
        private float sideSlipDeg;
        public float SideSlipDeg
        {
            get { return sideSlipDeg; }
            set
            {
                sideSlipDeg = value;
                NotifyPropertyChanged("sideSlipDeg");
            }
        }

        //Velocities
        private float airspeedKt;
        public float AirspeedKt
        {
            get { return airspeedKt; }
            set
            {
                airspeedKt = value;
                NotifyPropertyChanged("airspeedKt");
            }
        }
        private float glideslope;
        public float Glideslope
        {
            get { return glideslope; }
            set
            {
                glideslope = value;
                NotifyPropertyChanged("glideslope");
            }
        }
        private float verticalSpeedFps;
        public float VerticalSpeedFps
        {
            get { return verticalSpeedFps; }
            set
            {
                verticalSpeedFps = value;
                NotifyPropertyChanged("verticalSpeedFps");
            }
        }

        //Accelerations
        //Surface Positions
        //instruments
        private float airspeedIndicatorIndicatedSpeedKt;
        public float AirspeedIndicatorIndicatedSpeedKt
        {
            get { return airspeedIndicatorIndicatedSpeedKt; }
            set
            {
                airspeedIndicatorIndicatedSpeedKt = value;
                NotifyPropertyChanged("airspeedIndicatorIndicatedSpeedKt");
            }
        }
        private float altimeterIndicatedAltitudeFt;
        public float AltimeterIndicatedAltitudeFt
        {
            get { return altimeterIndicatedAltitudeFt; }
            set
            {
                altimeterIndicatedAltitudeFt = value;
                NotifyPropertyChanged("altimeterIndicatedAltitudeFt");
            }
        }
        private float altimeterPressureAltFt;
        public float AltimeterPressureAltFt
        {
            get { return altimeterPressureAltFt; }
            set
            {
                altimeterPressureAltFt = value;
                NotifyPropertyChanged("altimeterPressureAltFt");
            }
        }
        private float attitudeIndicatorIndicatedPitchDeg;
        public float AttitudeIndicatorIndicatedPitchDeg
        {
            get { return attitudeIndicatorIndicatedPitchDeg; }
            set
            {
                attitudeIndicatorIndicatedPitchDeg = value;
                NotifyPropertyChanged("attitudeIndicatorIndicatedPitchDeg");
            }
        }
        private float attitudeIndicatorIndicatedRollDeg;
        public float AttitudeIndicatorIndicatedRollDeg
        {
            get { return attitudeIndicatorIndicatedRollDeg; }
            set
            {
                attitudeIndicatorIndicatedRollDeg = value;
                NotifyPropertyChanged("attitudeIndicatorIndicatedRollDeg");
            }
        }
        private float attitudeIndicatorInternalPitchDeg;
        public float AttitudeIndicatorInternalPitchDeg
        {
            get { return attitudeIndicatorInternalPitchDeg; }
            set
            {
                attitudeIndicatorInternalPitchDeg = value;
                NotifyPropertyChanged("attitudeIndicatorInternalPitchDeg");
            }
        }
        private float attitudeIndicatorInternalRollDeg;
        public float AttitudeIndicatorInternalRollDeg
        {
            get { return attitudeIndicatorInternalRollDeg; }
            set
            {
                attitudeIndicatorInternalRollDeg = value;
                NotifyPropertyChanged("attitudeIndicatorInternalRollDeg");
            }
        }
        private float encoderIndicatedAltitudeFt;
        public float EncoderIndicatedAltitudeFt
        {
            get { return encoderIndicatedAltitudeFt; }
            set
            {
                encoderIndicatedAltitudeFt = value;
                NotifyPropertyChanged("encoderIndicatedAltitudeFt");
            }
        }
        private float encoderPressureAltFt;
        public float EncoderPressureAltFt
        {
            get { return encoderPressureAltFt; }
            set
            {
                encoderPressureAltFt = value;
                NotifyPropertyChanged("encoderPressureAltFt");
            }
        }
        private float gpsIndicatedAltitudeFt;
        public float GpsIndicatedAltitudeFt
        {
            get { return gpsIndicatedAltitudeFt; }
            set
            {
                gpsIndicatedAltitudeFt = value;
                NotifyPropertyChanged("gpsIndicatedAltitudeFt");
            }
        }
        private float gpsIndicatedGroundSpeedKt;
        public float GpsIndicatedGroundSpeedKt
        {
            get { return gpsIndicatedGroundSpeedKt; }
            set
            {
                gpsIndicatedGroundSpeedKt = value;
                NotifyPropertyChanged("gpsIndicatedGroundSpeedKt");
            }
        }
        private float gpsIndicatedVerticalSpeed;
        public float GpsIndicatedVerticalSpeed
        {
            get { return gpsIndicatedVerticalSpeed; }
            set
            {
                gpsIndicatedVerticalSpeed = value;
                NotifyPropertyChanged("gpsIndicatedVerticalSpeed");
            }
        }
        private float indicatedHeadingDeg;
        public float IndicatedHeadingDeg
        {
            get { return indicatedHeadingDeg; }
            set
            {
                indicatedHeadingDeg = value;
                NotifyPropertyChanged("indicatedHeadingDeg");
            }
        }
        private float magneticCompassIndicatedHeadingDeg;
        public float MagneticCompassIndicatedHeadingDeg
        {
            get { return magneticCompassIndicatedHeadingDeg; }
            set
            {
                magneticCompassIndicatedHeadingDeg = value;
                NotifyPropertyChanged("magneticCompassIndicatedHeadingDeg");
            }
        }
        private float slipSkidBallIndicatedSlipSkid;
        public float SlipSkidBallIndicatedSlipSkid
        {
            get { return slipSkidBallIndicatedSlipSkid; }
            set
            {
                slipSkidBallIndicatedSlipSkid = value;
                NotifyPropertyChanged("slipSkidBallIndicatedSlipSkid");
            }
        }
        private float turnIndicatorIndicatedTurnRate;
        public float TurnIndicatorIndicatedTurnRate
        {
            get { return turnIndicatorIndicatedTurnRate; }
            set
            {
                turnIndicatorIndicatedTurnRate = value;
                NotifyPropertyChanged("turnIndicatorIndicatedTurnRate");
            }
        }
        private float verticalSpeedIndicatorIndicatedSpeedFpm;
        public float VerticalSpeedIndicatorIndicatedSpeedFpm
        {
            get { return verticalSpeedIndicatorIndicatedSpeedFpm; }
            set
            {
                verticalSpeedIndicatorIndicatedSpeedFpm = value;
                NotifyPropertyChanged("verticalSpeedIndicatorIndicatedSpeedFpm");
            }
        }
        private float engineRpm;
        public float EngineRpm
        {
            get { return engineRpm; }
            set
            {
                engineRpm = value;
                NotifyPropertyChanged("engineRpm");
            }
        }


        private void NotifyPropertyChanged(string propName)
        {
            //  if (this.PropertyChanged != null)
            //The question mark replaces the check null
            this?.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

    }



        
}
