using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using FlightSimADVProg2_ex1.Model;


namespace AP2
{
    class VM_AnomalyDetector : INotifyPropertyChanged
    {
        private IModel model;
        public VM_AnomalyDetector(IModel model)
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
        //returns list of timesteps of regular anomalies
        public List<int> VM_AnomalyAd(string learnNormalCsv)
        {
            return model.AnomalyAd(learnNormalCsv);
        }
    }
}
