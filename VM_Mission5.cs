/*
 * Task 5
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using FlightSimADVProg2_ex1.Model;

namespace FlightSimADVProg2_ex1.ViewModels
{
    class VM_Mission5 : INotifyPropertyChanged
    {
        private IModel model;
        public VM_Mission5(IModel model)
        {
            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this?.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        //altimeter
        public float VM_AltimeterIndicatedAltitudeFt
        {
            get { return model.AltimeterIndicatedAltitudeFt; }
            set { model.AltimeterIndicatedAltitudeFt = value; }
        }
        //airspeed
        public float VM_AirspeedKt
        {
            get { return model.AirspeedKt; }
            set { model.AirspeedKt = value; }
        }
        //direction
        public float VM_IndicatedHeadingDeg
        {
            get { return model.IndicatedHeadingDeg; }
            set { model.IndicatedHeadingDeg = value; }
        }
        //pitch deg
        public float VM_PitchDeg
        {
            get { return model.PitchDeg; }
            set
            {
                model.PitchDeg = value;
            } 
        }
        //roll deg
        public float VM_RollDeg
        {
            get {
                return model.RollDeg; }
            set
            {
                model.RollDeg = value;
            }
        }
        //yaw
        public float VM_SideSlipDeg
        {
            get { return model.SideSlipDeg; }
            set { model.SideSlipDeg = value; }
        }
    }
}
