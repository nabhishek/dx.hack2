using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WindowsIoTCorePi2FezHat
{
    /// <summary>
    /// Class to manage sensor data and attributes 
    /// </summary>
    public class ConnectTheDotsSensor
    {
        public string guid { get; set; }
        public string displayname { get; set; }
        public string organization { get; set; }
        public string location { get; set; }
        public string measurename { get; set; }
        public string unitofmeasure { get; set; }
        public string timecreated { get; set; }
        public double value { get; set; }

        public int Device_id { get; set; }
        public int Farm_id { get; set; }
        public int Plot_id { get; set; }
        public int Crop_id { get; set; }
        public double Temp { get; set; }
        public double Humidity { get; set; }
        //double Humidity;
        //int Device_id;
        //string Farm_id;
        //string Plot_id;
        //DateTime Time;
        //String Crop_name; 

        /// <summary>
        /// Default parameterless constructor needed for serialization of the objects of this class
        /// </summary>
        public ConnectTheDotsSensor()
        {
        }

        /// <summary>
        /// Construtor taking parameters guid, measurename and unitofmeasure
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="measurename"></param>
        /// <param name="unitofmeasure"></param>
        public ConnectTheDotsSensor(string guid, string measurename, string unitofmeasure)
        {
            this.guid = guid;
            this.measurename = measurename;
            this.unitofmeasure = unitofmeasure;
        }

        /// <summary>
        /// ToJson function is used to convert sensor data into a JSON string to be sent to Azure Event Hub
        /// </summary>
        /// <returns>JSon String containing all info for sensor data</returns>
        public string ToJson()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(ConnectTheDotsSensor));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, this);
            string json = Encoding.UTF8.GetString(ms.ToArray(), 0, (int)ms.Length);

            return json;
        }
    }
}
