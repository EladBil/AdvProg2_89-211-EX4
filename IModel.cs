using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlightSimADVProg2_ex1.Model
{
    interface IModel : INotifyPropertyChanged
    {

        /// <summary>
        /// upload file API
        /// </summary>
        /// <param name="fileAPI">path</param>
        /// <returns></returns>
        public int LoadingAPI(string fileAPI);
        /// <summary>
        /// upload file csv
        /// </summary>
        /// <param name="fileAPI">path</param>
        /// <returns></returns>
        public int LoadingCSV(string fileAPI);

        /// <summary>
        /// Get Number Rows 
        /// </summary>
        /// <returns></returns>
        int GetNumberRows();
        /// <summary>
        /// get current speed
        /// </summary>
        /// <returns>return the speed</returns>
        float GetRefreshRate();
        /// <summary>
        /// Sets the desired speed
        /// </summary>
        /// <param name="speed"></param>
        void SetRefreshRate(float speed);

        /// <summary>
        /// Given a feature returns all its values in order
        /// </summary>
        /// <param name="attribute">name of attribute</param>
        /// <returns>List of attribute values </returns>
        List<float> GetValuesSpecificAttribute(string attribute);
        /// <summary>
        /// line regers of 2 attributes
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns>line reg</returns>
        Line lineReg(string value1, string value2);


        /// <summary>
        /// given an attribute it gives you the current info on the row
        /// we are on now.
        /// </summary>
        /// <param name="atrribute"></param>
        /// <returns></returns>
        float GetDetailNow(string atrribute);

        /// <summary>
        /// return list of attributes
        /// </summary>
        /// <returns></returns>
        List<string> GetListOfAttribute();


        /// <summary>
        /// returns list of anomalies based off of circle (HAD)
        /// </summary>
        /// <param name="learnNormalCsv"></param>
        /// <returns></returns>
        List<int> AnomalyAd(string learnNormalCsv);

        public void start();
        public void start(string ip, int port);

        /*
         * get most cor receives a field (thats on a csv) and returns which one of the other fields are the most corralitive from the 
         * begining to end based
         * on the pearson.
         * */
        /// <summary>
        /// get most cor receives a field (thats on a csv) and returns which one of the other fields are the most corralitive from the 
        /// begining to end based
        /// on the pearson.
        /// </summary>
        /// <param name="givenIndex"></param>
        /// <returns></returns>
        string GetMostCor(string givenIndex);


        int IndexFrame { get; set; }

        /*
         * properties of the features
         * 
         */

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

}
