/*
 * VM for loading API and flight CSV at start of the program
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using FlightSimADVProg2_ex1.Model;

namespace FlightSimADVProg2_ex1.ViewModels
{
    class VM_Start : INotifyPropertyChanged
    {
        private IModel model;
        public VM_Start(IModel model)
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
        //load the API upon start of program
        private void VM_loadingAPI(string fileAPI)
        {
            model.LoadingAPI(fileAPI);
        }
        //load the CSV upon start of program
        private void VM_LoadingCSV(string fileCSV)
        {
            model.LoadingCSV(fileCSV);
        }
        public void VM_LetsStart(string fileCSV, string fileAPI)
        {
            VM_loadingAPI(fileAPI);
            VM_LoadingCSV(fileCSV);
            model.start();
        }
    }
}
