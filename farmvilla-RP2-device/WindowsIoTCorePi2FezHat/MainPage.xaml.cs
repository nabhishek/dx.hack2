// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WindowsIoTCorePi2FezHat
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using GHIElectronics.UWP.Shields;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        FEZHAT hat;
        DispatcherTimer telemetryTimer;
        DispatcherTimer commandsTimer;

        ConnectTheDotsHelper ctdHelper;

        /// <summary>
        /// Main page constructor
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            var deviceInfo = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();

            // Hard coding guid for sensors. Not an issue for this particular application which is meant for testing and demos
            List<ConnectTheDotsSensor> sensors = new List<ConnectTheDotsSensor> {
                new ConnectTheDotsSensor("2298a348-e2f9-4438-ab23-82a3930662ab", "Light", "L"),
                new ConnectTheDotsSensor("d93ffbab-7dff-440d-a9f0-5aa091630201", "Temperature", "C"),
            };
            
            //replace the connection string to send the data to your iot hub
            //ctdHelper = new ConnectTheDotsHelper(iotDeviceConnectionString: "HostName=farmvilla-hub.azure-devices.net;DeviceId=3;SharedAccessKey=/eZvqqgXLWWaMiztGJsN9C+1/FBLoqjFjn104Js=",
            ctdHelper = new ConnectTheDotsHelper(iotDeviceConnectionString: "replace this with your connection string",
                organization: "Farmvilla",
                location: "Bangalore",
                sensorList: sensors);
        }

        private async Task SetupHatAsync()
        {
            this.hat = await FEZHAT.CreateAsync();

            //sending data every second. change as required
            this.commandsTimer = new DispatcherTimer();
            this.commandsTimer.Interval = TimeSpan.FromMilliseconds(1000);
            this.commandsTimer.Tick += this.CommandsTimer_Tick;
            this.commandsTimer.Start();
        }

        private void TelemetryTimer_Tick(object sender, object e)
        {
            // Light Sensor
            ConnectTheDotsSensor lSensor = ctdHelper.sensors.Find(item => item.measurename == "Light");
            lSensor.value = this.hat.GetLightLevel();
            
            this.ctdHelper.SendSensorData(lSensor);
            this.LightTextBox.Text = lSensor.value.ToString("P2", CultureInfo.InvariantCulture);
            this.LightProgress.Value = lSensor.value;

            // Temperature Sensor
            var tSensor = ctdHelper.sensors.Find(item => item.measurename == "Temperature");
            tSensor.value = this.hat.GetTemperature();
            this.ctdHelper.SendSensorData(tSensor);

            this.TempTextBox.Text = tSensor.value.ToString("N2", CultureInfo.InvariantCulture);

            System.Diagnostics.Debug.WriteLine("Temperature: {0} °C, Light {1}", tSensor.value.ToString("N2", CultureInfo.InvariantCulture), lSensor.value.ToString("P2", CultureInfo.InvariantCulture));
        }

        private async void CommandsTimer_Tick(object sender, object e)
        {
            string message = await ctdHelper.ReceiveMessage();

            if (message != string.Empty)
            {
                System.Diagnostics.Debug.WriteLine("Command Received: {0}", message);
                switch (message.ToUpperInvariant())
                {
                    case "RED":
                        hat.D2.Color = new FEZHAT.Color(255, 0, 0);
                        break;
                    case "GREEN":
                        hat.D2.Color = new FEZHAT.Color(0, 255, 0);
                        break;
                    case "BLUE":
                        hat.D2.Color = new FEZHAT.Color(0, 0, 255);
                        break;
                    case "OFF":
                        hat.D2.TurnOff();
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("Unrecognized command: {0}", message);
                        break;
                }
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize FEZ HAT shield
            await SetupHatAsync();
        }
    }
}
