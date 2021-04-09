using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Diagnostics;


namespace FlightSimADVProg2_ex1.Model
{
    /// <summary>
    /// Interface for the model
    /// </summary>

    public partial class MyModel : IModel
    {


        public event PropertyChangedEventHandler PropertyChanged;

        readonly ITelnetClient telnetClientFlightGear;
        private Boolean stop;

        public Boolean Stop
        {

            set
            {
                this.stop = value;
            }

        }


        /// <summary>
        /// constractor - Initializes the dictionary of speed
        /// </summary>
        /// <param name="telnetClient"></param>
        public MyModel(ITelnetClient telnetClient)
        {
            this.telnetClientFlightGear = telnetClient;
            stop = false;

            this.speedToMillisecondAndRate = new Dictionary<float, MiliSecondAndRate>();
            speedToMillisecondAndRate.Add(0, new MiliSecondAndRate(500, 2));
            speedToMillisecondAndRate.Add((float)0.25, new MiliSecondAndRate(250, 4));
            speedToMillisecondAndRate.Add((float)0.5, new MiliSecondAndRate(200, 5));
            speedToMillisecondAndRate.Add(1, new MiliSecondAndRate(100, 10));
            speedToMillisecondAndRate.Add((float)1.5, new MiliSecondAndRate(50, 20));
            speedToMillisecondAndRate.Add((float)1.75, new MiliSecondAndRate(40, 25));
            speedToMillisecondAndRate.Add(2, new MiliSecondAndRate(20, 50));

        }
        /// <summary>
        /// Connects to flight gear using telnetClientFlightGear
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void Connect(string ip, int port)
        {
            this.ip = ip;
            this.port = port;

            this.telnetClientFlightGear.Connect(this.ip, this.port);


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
        public void start(string ip, int port)
        {

            TcpClient client = new TcpClient(ip, port);
            new Thread(delegate ()
            {

                NetworkStream stream = client.GetStream();
                while (!stop)
                {
                    string line;

                    this.arrayRowNow = ts.TsGetRow(IndexFrame);
                    // convert vectorFloat from dll to arrayFloat in c#
                    this.updateAllattributes(arrayRowNow);
                    line = String.Join(",", arrayRowNow);
                    line += "\r\n";
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(line);
                    stream.Write(data, 0, data.Length);
                    //telnetClientFlightGear.Write(line);
                    IndexFrame++;

                    Thread.Sleep(this.millisecondsTimeout);// read the data in 4Hz
                    if (IndexFrame >= this.countRows)
                    {
                        stop = true;

                    }
                }


                client.Close();
            }).Start();


        }
        public void start()
        {

            new Thread(delegate ()
            {

                while (!stop)
                {


                    this.arrayRowNow = ts.TsGetRow(IndexFrame);
                    // convert vectorFloat from dll to arrayFloat in c#
                    this.updateAllattributes(arrayRowNow);


                    IndexFrame++;

                    Thread.Sleep(this.millisecondsTimeout);// read the data in 4Hz

                    if (IndexFrame >= this.countRows)
                    {
                        stop = true;

                    }

                }
                //telnetClientFlightGear.Disconnect();


            }).Start();


        }

        /// <summary>
        /// Update the fields
        /// </summary>
        /// <param name="arrayValue"></param>
        private void updateAllattributes(float[] arrayValue)
        {
            Aileron = arrayValue[this.DictValuesToNumInCsv["aileron"]];
            Elevator = arrayValue[this.DictValuesToNumInCsv["elevator"]];
            Rudder = arrayValue[this.DictValuesToNumInCsv["rudder"]];
            flaps = arrayValue[this.DictValuesToNumInCsv["flaps"]];
            Slats = arrayValue[this.DictValuesToNumInCsv["slats"]];
            Speedbrake = arrayValue[this.DictValuesToNumInCsv["speedbrake"]];
            Throttle_1 = arrayValue[this.DictValuesToNumInCsv["throttle"]];
            Throttle_2 = arrayValue[this.DictValuesToNumInCsv["throttle2"]];
            EnginePump_1 = arrayValue[this.DictValuesToNumInCsv["engine-pump"]];
            EnginePump_2 = arrayValue[this.DictValuesToNumInCsv["engine-pump2"]];

            ElectriPump_1 = arrayValue[this.DictValuesToNumInCsv["electric-pump"]];
            ElectriPump_2 = arrayValue[this.DictValuesToNumInCsv["electric-pump2"]];
            ExternalPower = arrayValue[this.DictValuesToNumInCsv["external-power"]];
            APUGenerator = arrayValue[this.DictValuesToNumInCsv["APU-generator"]];
            LatitudeDeg = arrayValue[this.DictValuesToNumInCsv["latitude-deg"]];
            LongitudeDeg = arrayValue[this.DictValuesToNumInCsv["longitude-deg"]];
            AltitudeFt = arrayValue[this.DictValuesToNumInCsv["altitude-ft"]];
            RollDeg = arrayValue[this.DictValuesToNumInCsv["roll-deg"]];
            PitchDeg = arrayValue[this.DictValuesToNumInCsv["pitch-deg"]];
            HeadingDeg = arrayValue[this.DictValuesToNumInCsv["heading-deg"]];

            SideSlipDeg = arrayValue[this.DictValuesToNumInCsv["side-slip-deg"]];
            AirspeedKt = arrayValue[this.DictValuesToNumInCsv["airspeed-kt"]];
            Glideslope = arrayValue[this.DictValuesToNumInCsv["glideslope"]];
            VerticalSpeedFps = arrayValue[this.DictValuesToNumInCsv["vertical-speed-fps"]];
            AirspeedIndicatorIndicatedSpeedKt = arrayValue[this.DictValuesToNumInCsv["airspeed-indicator_indicated-speed-kt"]];
            AltimeterIndicatedAltitudeFt = arrayValue[this.DictValuesToNumInCsv["altimeter_indicated-altitude-ft"]];
            AltimeterPressureAltFt = arrayValue[this.DictValuesToNumInCsv["altimeter_pressure-alt-ft"]];
            AttitudeIndicatorIndicatedPitchDeg = arrayValue[this.DictValuesToNumInCsv["attitude-indicator_indicated-pitch-deg"]];
            AttitudeIndicatorIndicatedRollDeg = arrayValue[this.DictValuesToNumInCsv["attitude-indicator_indicated-roll-deg"]];
            AttitudeIndicatorInternalPitchDeg = arrayValue[this.DictValuesToNumInCsv["attitude-indicator_internal-pitch-deg"]];

            AttitudeIndicatorInternalRollDeg = arrayValue[this.DictValuesToNumInCsv["attitude-indicator_internal-roll-deg"]];
            EncoderIndicatedAltitudeFt = arrayValue[this.DictValuesToNumInCsv["encoder_indicated-altitude-ft"]];
            EncoderPressureAltFt = arrayValue[this.DictValuesToNumInCsv["encoder_pressure-alt-ft"]];
            GpsIndicatedAltitudeFt = arrayValue[this.DictValuesToNumInCsv["gps_indicated-altitude-ft"]];
            GpsIndicatedGroundSpeedKt = arrayValue[this.DictValuesToNumInCsv["gps_indicated-ground-speed-kt"]];
            GpsIndicatedVerticalSpeed = arrayValue[this.DictValuesToNumInCsv["gps_indicated-vertical-speed"]];
            IndicatedHeadingDeg = arrayValue[this.DictValuesToNumInCsv["indicated-heading-deg"]];
            MagneticCompassIndicatedHeadingDeg = arrayValue[this.DictValuesToNumInCsv["magnetic-compass_indicated-heading-deg"]];
            SlipSkidBallIndicatedSlipSkid = arrayValue[this.DictValuesToNumInCsv["slip-skid-ball_indicated-slip-skid"]];
            TurnIndicatorIndicatedTurnRate = arrayValue[this.DictValuesToNumInCsv["turn-indicator_indicated-turn-rate"]];

            VerticalSpeedIndicatorIndicatedSpeedFpm = arrayValue[this.DictValuesToNumInCsv["vertical-speed-indicator_indicated-speed-fpm"]];
            EngineRpm = arrayValue[this.DictValuesToNumInCsv["engine_rpm"]];

        }

        /// <summary>
        /// Change the speed of sending data to flight gear
        /// </summary>
        public int MillisecondsTimeout
        {
            get { return millisecondsTimeout; }
            set
            {
                millisecondsTimeout = value;
            }
        }




        private int indexFrame;
        public int IndexFrame
        {
            get
            {
                return indexFrame;
            }
            set
            {
                indexFrame = value;
                NotifyPropertyChanged("indexFrame");
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
            if (this.PropertyChanged != null)
            {
                //The question mark replaces the check null
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

    }





}