using System;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;

namespace model
{
    class Scattering
    {
        /// <summary>
        /// in the map have problem of values that have the same name
        /// </summary>
        /// <param name="fileXML"></param>
        /// <returns></returns>
        public static Dictionary<string, int> CreateMapOfValues(string fileXML)
        {
            Dictionary<string, int> myDict = new Dictionary<string, int>();

            string myXmlString = File.ReadAllText(fileXML);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(myXmlString);

            XmlNodeList xnList = xml.SelectNodes("/PropertyList/generic/output/chunk");
            int i = 0;
            foreach (XmlNode xn in xnList)
            {
                string firstName = xn["name"].InnerText;
                myDict.Add(firstName, i);

                Console.WriteLine(firstName);
            }
            return myDict;

        }


        /*
         * AddFirstLineInCsv -  add new line to first list in file csv
         * Warning This function modifies the file itself!!!!!!!!!!!!!!!!!!!
         */
        public static void AddFirstLineInCsv(string filePathOld, string filePathNew, string csvLine)
        {
           
            // string filePathOld = @"C:\Test\test.csv";
            //string csvLine = "value1; value2; value3" + Environment.NewLine;
            byte[] csvLineBytes = Encoding.Default.GetBytes(csvLine);
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(csvLineBytes, 0, csvLineBytes.Length);
                using (FileStream file = new FileStream(filePathOld, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);
                }

                using (FileStream file = new FileStream(filePathNew, FileMode.Open, FileAccess.Write))
                {
                    ms.WriteTo(file);
                }
            }
        }
    }
}
