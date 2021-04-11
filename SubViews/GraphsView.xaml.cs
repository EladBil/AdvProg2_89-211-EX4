using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Collections.Generic;
using System;

using FlightSimADVProg2_ex1.Model;
using FlightSimADVProg2_ex1.ViewModels;

namespace FlightSimADVProg2_ex1.SubViews
{
    /// <summary>
    /// Interaction logic for GraphsView.xaml
    /// </summary>
    public partial class GraphsView : UserControl
    {
        const string MAINGRAPHNAME = "Value/Frame";
        const string INDEXFRAMEPROPERTY = "VM_indexFrame";
        const string DEFAULT_DIDNT_CHOOSE = "Default: PleaseChoose!";
        const int DEFAULT_MAX_RENDER = 30;
        const int DEFAULT_MIN_RENDER = 0;

        int NumOfFrames;
        double[] ArrayOfFrameNumbers;
        double[] DefaultArray;
        bool Onlyonce = true;
        bool InitializedChosenParam;


        public GraphsView()
        {
            InitializeComponent();
            this.InitializedChosenParam = false;
        }


        private GraphsViewModel gvm;
        public GraphsViewModel GivenGraphsViewModel
        {
            get { return gvm; }
            set
            {
                if (Onlyonce)
                {
                    this.gvm = value;
                    LoadMyModelSequence();
                }
            }
        }


        private void LoadMyModelSequence()
        {
            Onlyonce = false;
            DataContext = this.gvm;
            this.gvm.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
                {
                    PropertyChangedManage(e.PropertyName);
                };
        }



        private void PropertyChangedManage(string name)
        {
            if (name.Equals(GraphsViewModel.ATTRIBUTS_NAMES_PROPERTY_NAME))
            {
                InitDefaultGraphScreen();
                return;
            }
            if (name.Equals(GraphsViewModel.CORRELATIVE_ATTRIBUTE_VLAUES_PROPERTY_NAME))
            {
                UpdateGraphValues();
                this.InitializedChosenParam = true;
                return;
            }
            if (name.Equals(GraphsViewModel.FRAME_INDEX_PROPERTY_NAME))
            {
                if (this.InitializedChosenParam && gvm.VM_IndexFrame % 5 == 0)
                {
                    UpdateGraphValues();
                }
            }
        }


        private void InitDefaultGraphScreen()
        {
            this.NumOfFrames = this.gvm.GetOfNumberRows();
            this.DefaultArray = new double[this.NumOfFrames];
            this.ArrayOfFrameNumbers = new double[this.NumOfFrames];
            for (int i = 0; i < this.NumOfFrames; i++)
            {
                this.DefaultArray[i] = 0;
                this.ArrayOfFrameNumbers[i] = i;
            }
 
            ParamAndCorrelativeGraph.plt.PlotSignal(DefaultArray,
                minRenderIndex:DEFAULT_MIN_RENDER, maxRenderIndex:DEFAULT_MAX_RENDER, label: DEFAULT_DIDNT_CHOOSE);
            ParamAndCorrelativeGraph.plt.PlotSignal(DefaultArray,
                minRenderIndex:DEFAULT_MIN_RENDER, maxRenderIndex: DEFAULT_MAX_RENDER, label: DEFAULT_DIDNT_CHOOSE);

            ParamAndCorrelativeGraph.plt.Legend();
            ParamAndCorrelativeGraph.plt.Style(ScottPlot.Style.Blue3);
            ParamAndCorrelativeGraph.plt.AxisAuto();
        }


        private void UpdateGraphValues()
        {
            ParamAndCorrelativeGraph.plt.Clear();
            int MinRender = 0;
            if (this.gvm.VM_IndexFrame > 30)
            {
                MinRender = this.gvm.VM_IndexFrame - 30;
            }
            ParamAndCorrelativeGraph.plt.PlotSignal(this.gvm.VM_ChosenAttributeValues, minRenderIndex: MinRender,
                maxRenderIndex: this.gvm.VM_IndexFrame, label: this.gvm.VM_ChosenAttributeName);
            ParamAndCorrelativeGraph.plt.PlotSignal(this.gvm.VM_CorrelativeAttributeValues, minRenderIndex: MinRender,
                maxRenderIndex: this.gvm.VM_IndexFrame, label: this.gvm.VM_CorrelativeAttributeName);
            
            this.Dispatcher.Invoke((Action)(() =>
            {
                ParamAndCorrelativeGraph.Render();
            }));
        }


        private void FlightParameterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string muchwow = (sender as ListBox).SelectedItem.ToString();
            this.gvm.VM_ChosenAttributeName = muchwow;
        }

        private void ParamAndCoorelativeGraph_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
