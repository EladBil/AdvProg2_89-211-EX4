/*
 * mission 6, 7, 8.
 * Person, list of stuff, lin_reg
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using FlightSimADVProg2_ex1.Model;

namespace AP2
{
    class VM_Graphs : INotifyPropertyChanged
    {
        private IModel model;

        public VM_Graphs(IModel model)
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
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        //returns list of attributes
        public List<string> VM_Attributes
        {
            get { return model.GetListOfAttribute(); }
        }
        //returns colomn of attribute
        public List<float> VM_GetValuesSpecificAttribute(string attribute)
        {
            return model.GetValuesSpecificAttribute(attribute);
        }
        //returns most correlated to this feature
        public string VM_GetMostCor(string givenIndex)
        {
            return model.GetMostCor(givenIndex);
        }
        //returns linear reg line between two features
        public Line VM_lineReg(string chosen, string correlated)
        {
            return model.lineReg(chosen, correlated);
        }
    }
}
