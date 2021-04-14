using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FlightSimADVProg2_ex1.Model
{
    /// <summary>
    /// The model interface
    ///The model is designed to be part of the mvvm architecture
    ///As part of the above architecture the model does not know who is using its functions such as view model and view
    ///For this purpose, the model actually uses the "INotifyPropertyChanged" interface, which reports automatically as soon as subscribers subscribe to it.
    ///In our project the role of the model is to carry out the logic of flight data analysis
    ///For this purpose it is literally functions whose details appear below
    /// </summary>
    public interface IModel : INotifyPropertyChanged
    {

        /// <summary>
        /// upload file API
        /// </summary>
        /// <param name="fileAPI">path</param>
        /// <returns></returns>
        int LoadingAPI(string fileAPI);
        /// <summary>
        /// upload file csv
        /// </summary>
        /// <param name="fileAPI">path</param>
        /// <returns></returns>
        int LoadingCSV(string fileAPI);

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
        List<float> GetListOfspeeds();

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
        List<int> DetectAnomaly(string learnNormalCsv);
        /// <summary>
        /// Need to connect to a source to send or read data from
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns>Value of failure (-1) or success(0)</returns>
        int Connect(string ip, int port);
        /// <summary>
        /// Disconnects
        /// </summary>
        void Disconnect();
        /// <summary>
        /// The function should start the data reading process update and send to all notify
        /// </summary>
        void start();
        /// <summary>
        /// The function should stop the start function
        /// </summary>
        void pause();

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
        string IP { set; get; }
        /*
         * properties of the features
         * For each variable of the flight we implement property for easy
         * access to the variable without creating geters and setters
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