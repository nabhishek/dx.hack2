using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MyFarm
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            

            var farmInfo = new List<FarmData> {
                new FarmData("Mango", 32, "device_1", 32, "farm_1", "plot_1", DateTime.Now, Color.FromHex("FF0000")),
                new FarmData("Banana", 34, "device_2", 32, "farm_1", "plot_2", DateTime.Now, Color.FromHex("FF0000")),
                new FarmData("Banana", 33, "device_3", 32, "farm_1", "plot_3", DateTime.Now, Color.FromHex("FF0000")),
                new FarmData("Mango", 34, "device_4", 32, "farm_1", "plot_4", DateTime.Now, Color.FromHex("FF0000")),
                new FarmData("Banana", 32, "device_5", 32, "farm_1", "plot_5", DateTime.Now, Color.FromHex("FF0000")),
                new FarmData("Banana", 33, "device_6", 32, "farm_1", "plot_6", DateTime.Now, Color.FromHex("FF0000")),
                new FarmData("Mango", 34, "device_7", 32, "farm_1", "plot_7", DateTime.Now, Color.FromHex("FF0000")),
                new FarmData("Mango", 32, "device_8", 32, "farm_1", "plot_8", DateTime.Now, Color.FromHex("FF0000")),
                new FarmData("Banana", 33, "device_9", 32, "farm_1", "plot_9", DateTime.Now, Color.FromHex("FF0000")),
                new FarmData("Mango", 32, "device_10", 32, "farm_1", "plot_10", DateTime.Now, Color.FromHex("FF0000")),
            };

            listView.ItemsSource = farmInfo;

            FarmInfo("Bangalore");
        }

        private async void FarmInfo(string location)
        {
            var request = HttpWebRequest.Create(string.Format(@"REPLACE WITH YOUR URL"));
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Debug.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Debug.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        Debug.WriteLine("Response Body: \r\n {0}", content);

                        content.Replace("[", "");
                        content.Replace("]", "");
                        string[] records = Regex.Split(content, ";");
                        foreach (string record in records)
                        {
                            string[] CropInfo = Regex.Split(record, ",");
                        }
                    }

                    Debug.WriteLine(content);
                }
            }

        }

        public class FarmData
        {
            public string Crop_name { get; set; }
            public double Humidity { get; set; }
            public string Device_id { get; set; }
            public double Temp { get; set; }
            public string Farm_id { get; set; }
            public string Plot_id { get; set; }
            public DateTime Time { get; set; }
            public Color Status { get; set; }

            public FarmData(string crop_name, int humidity, string device_id, int temp, string farm_id, string plot_id, DateTime time, Color status)
            {
                Crop_name = crop_name;
                Humidity = humidity;
                Device_id = device_id;
                Temp = temp;
                Farm_id = farm_id;
                Plot_id = plot_id;
                Time = time;
                Status = status;
            }
        }
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Location { get; set; }

            public Person(string name, int age, string location)
            {
                Name = name;
                Age = age;
                Location = location;
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
