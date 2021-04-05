using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace model
{
    interface ITimeSeries
    {

    }
    class TimeSeriesModel: ITimeSeries
    {

        [DllImport("AP2Libraries.dll")]
        public static extern void TsDelete(IntPtr ts);


        [DllImport("AP2Libraries.dll")]
        public static extern float covFromColIndex(IntPtr ts, int col1, int col2);


        [DllImport("AP2Libraries.dll")]
        public static extern float varFromColIndex(IntPtr ts, int col);

        [DllImport("AP2Libraries.dll")]
        public static extern float avgFromColIndex(IntPtr ts, int col);

        [DllImport("AP2Libraries.dll")]
        public static extern int TsGetColSize(IntPtr ts);
        [DllImport("AP2Libraries.dll")]
        public static extern float pearsonFromColIndex(IntPtr ts, int col1, int col2);
        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr TsGetColByIndex(IntPtr ts, int i);

        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr createTS(string CSVfileName);
        [DllImport("AP2Libraries.dll")]
        public static extern IntPtr TsGetRow(IntPtr ts, int j);
        [DllImport("AP2Libraries.dll")]
        public static extern float TsGetInRow(IntPtr row, int i);
        [DllImport("AP2Libraries.dll")]
        public static extern int TsGetAttributesSize(IntPtr ts);
        [DllImport("AP2Libraries.dll")]
        public static extern void TsDeleteRow(IntPtr row);

        IntPtr tsP;
        IntPtr rowNow;
        private int amountAttribute = 0;
     
        private int amountLines = 0;
        public TimeSeriesModel(string path)
        {
            this.tsP = createTS(path);
            this.rowNow = TsGetRow(this.tsP , 0);
            this.amountAttribute = TsGetAttributesSize(this.tsP);
            this.amountLines = TsGetColSize(this.tsP);
            


        }

        public int AmountLines()
        {
            return this.amountLines;
        }

        public float[] TsGetRow(int indexFrame)
        {
            float[] arrayValue = new float[this.AmountAttribute()];
           
            TsDeleteRow(this.rowNow);
            this.rowNow = TsGetRow(this.tsP, indexFrame);

            int i;
            for (i = 0; i < this.AmountAttribute(); i++)
            {
                arrayValue[i] = TsGetInRow(this.rowNow, i);
            }
            return arrayValue;
        }
       

        public int AmountAttribute()
        {
            return this.amountAttribute;
        }

        private List<float> vectorFromIntPtrToList(IntPtr p, int size)
        {

            List<float> l = new List<float>();
            for (int i = 0; i < size; i++)
            {
                l.Add(TsGetInRow(p, i));
                // Console.WriteLine(TsGetInRow(p, i));               
            }
            return l;
        }
        public List<float> GetValuesSpecificAttribute(int numberCol)
        {
          
            IntPtr intPtr = TsGetColByIndex(this.tsP, numberCol);
            List<float> l = vectorFromIntPtrToList(intPtr, this.AmountLines());
            TsDeleteRow(intPtr);
            return l;
        }
        public float TsGetInRow(int indexAttribute)
        {
            return TsGetInRow(rowNow, indexAttribute);
        }
        
        public float varFromColIndex(int indexCol)
        {
            return varFromColIndex(this.tsP, indexCol);
        }
        public float avgFromColIndex(int indexCol)
        {
            return avgFromColIndex(this.tsP, indexCol);
        }
        public float covFromColIndex(int indexCol1 , int indexCol2)
        {
            return covFromColIndex(this.tsP, indexCol1, indexCol2);
        }

        public Line lineReg(int x, int y)
        {
            float a = covFromColIndex(x,y) / varFromColIndex(x);
            float b = avgFromColIndex(y) - a * avgFromColIndex(x);

            return new Line(a, b);
        }
        public float pearsonFromColIndex(int indexCol1, int indexCol2)
        {
            return pearsonFromColIndex(this.tsP, indexCol1, indexCol2);
        }
        public IntPtr GetIntP()
        {
            return this.tsP;
        }
        public void destroyTs()
        {
            TsDelete(this.tsP);
        }

    }
}
