using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;

namespace EventSenderToDevice_Farmvilla
{
    class Program
    {
        static ServiceClient serviceClient;
        static string connectionString = "<IOT HUB Connection String";
       enum Indicators {GREEN=1,YELLOW,RED }
        static void Main(string[] args)
        {
            Console.WriteLine("Send Cloud to Device Message\n");
            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

            Console.WriteLine("Press any key to send message.");
            //Logic to selct which signal should be sent

            //In this sample,we will send Green

            var message = Indicators.GREEN.ToString();
            Console.ReadLine();
            SendCloudToDeviceMessageAsync(message).Wait();
            Console.ReadLine();
        }

        private async static Task SendCloudToDeviceMessageAsync(string Message)
        {
            var commandMessage = new Message(Encoding.ASCII.GetBytes(Message)); //supports GREEN YELLOW RED . Use capitals.
            await serviceClient.SendAsync("3", commandMessage);
        }
    }
}
