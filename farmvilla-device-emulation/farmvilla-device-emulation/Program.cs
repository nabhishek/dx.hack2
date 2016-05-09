using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;


namespace farmvilla_device_emulation
{
    class Program
    {
        static DeviceClient deviceClient;
        //static string iotHubUri = "farmvilla-hub.azure-devices.net"; //Add your iot hub uri here
        static string iotHubUri = "iot hub uri here";
        //static string deviceKey = "pLx1BPJQ9IW4R4TCrOCfOCJu75WFxGfaBBfkOCQ="; //add device key here
        static string deviceKey = "device key here";


        static void Main(string[] args)
        {
            Console.WriteLine("Simulated Device\n");
            deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("1", deviceKey));
        //    deviceClient2 = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("2", devicekey2))
       

            SendDeviceToCloudMessageAsync();
            ReceiveC2dAsync();
            Console.ReadLine();
        }

        private static async void SendDeviceToCloudMessageAsync()
        {

            Random rand = new Random();

            var cropid = new List<int> { 4, 3, 1, 6, 5, 10, 9, 7, 8, 2 };       // different crops identified by this on backend. watering needs may be different per cropid
            var deviceid = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };     // deviceid is to differentiate different sensor devices' id in the farm
            var plotid = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };       // each farm is split into various plots, so each plot contains a deviceid and think linking of the 3 can be controlled on server.

            while (true)
            {
                int index = rand.Next(cropid.Count);
                
                var DataPoint = new
                {
                    Device_id = deviceid[index],
                    Humidity = rand.Next(10, 100),
                    Temp = rand.Next(20, 45),
                    Farm_id = 1001,              //constant
                    Plot_id = plotid[index],                  //update plotnumber
                    Crop_id = cropid[index]                 //update rand collection

                };
                var messageString = JsonConvert.SerializeObject(DataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending Message: {1}", DateTime.Now, messageString);

                Task.Delay(5000).Wait();
                              
            }
        }

        private static async void ReceiveC2dAsync()
        {
            Console.WriteLine("\nReceiving cloud to device messages from service");
            while (true)
            {
                Message receivedMessage = await deviceClient.ReceiveAsync();
                if (receivedMessage == null) continue;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Received message: {0}", Encoding.ASCII.GetString(receivedMessage.GetBytes()));
                Console.ResetColor();

                await deviceClient.CompleteAsync(receivedMessage);
            }
        }
    }
}
