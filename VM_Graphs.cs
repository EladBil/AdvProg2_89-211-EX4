﻿/*
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
        //chosen feature
        private string chosen;
        //most correlated feature
        private string cor;
        //colomn of chosen feature
        private double[] chosenVal;
        //colomn of corelated feature
        private double[] corVal;

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
        //returns the frame we are currently looking at
        public int VM_IndexFrame
        {
            get { return model.IndexFrame; }
            set
            {
                model.IndexFrame = value;
            }
        }
        public string VM_Chosen
        {
            get { return this.chosen; }
            set
            {
                //update the chosen feature
                chosen = value;
                //update the most correlated feature
                VM_Cor = model.GetMostCor(chosen);
                //update the row of the chosen feature
                VM_ChosenVal = FromListToArray(model.GetValuesSpecificAttribute(chosen));
                //update the row of the most correlated feature
                VM_CorVal = CorChange(model.GetValuesSpecificAttribute(VM_Cor));
                NotifyPropertyChanged("VM_Chosen");
            }
        }
        public string VM_Cor
        {
            get { return cor; }
            set
            {
                cor = value;
                if (cor.Equals("-1"))
                {
                    cor = "No Correlated Feature";
                }
                NotifyPropertyChanged("VM_Cor");
            }
        }
        public double[] VM_ChosenVal
        {
            get { return chosenVal; }
            set
            {
                chosenVal = value;
                NotifyPropertyChanged("VM_ChosenVal");
            }

        }
        public double[] VM_CorVal
        {
            get { return corVal; }
            set
            {
                corVal = value;
                NotifyPropertyChanged("VM_CorVal");
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
        //returns the number of rows in the csv
        public int VM_GetNumberRows()
        {
            return model.GetNumberRows();
        }
        //returns array of 0's
        private double[] ZeroFillArray()
        {

            int j = VM_GetNumberRows();
            double[] array = new double[j];
            for (int i = 0; i < j; i++)
            {
                array[i] = 0;
            }
            return array;
        }

        //returns an array created from list
        private double[] FromListToArray(List<float> list)
        {
            double[] array = new double[VM_GetNumberRows()];
            int i = 0;
            foreach (float value in list)
            {
                array[i] = (double)value;
                i++;
            }
            return array;
        }
        //retruns the array of most correlated feature
        private double[] CorChange(List<float> list)
        {
            //if there isnt a correlated feature return an empty array
            if (VM_Cor.Equals("No Correlated Feature"))
            {
                return ZeroFillArray();
            }
            return FromListToArray(list);
        }
    }
}