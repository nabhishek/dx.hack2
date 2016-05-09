# dx.hack2
This contains the code for the IoT solution for farmers that we did for the HACK. //to be updated


#farmvilla-device-emulation 
Contains the code running as webjobs or as console app from any Windows machine to emulate posting sensor value periodically to the iot hub. for the hack we emulated multiple such devices and used 1 real device where the UWP code was runing. You can find the device code in farmvill-RP2-device folder

How to run run this-
-> You need VS 2015 or 2013 with Azure SDK installed.
-> Create an iot hub on http://portal.azure.com and replace the connetion string in the program.cs file in this solution. you will have to create a new device using either device explorer or your code. details here: https://azure.microsoft.com/en-in/documentation/articles/iot-hub-csharp-csharp-getstarted/


#farmvilla-RP2-device
Contains the UWP code to interact with the FEZ Hat and read temp value. Posts it on Azure iot hub. Getting started details: https://azure.microsoft.com/en-in/documentation/articles/iot-hub-csharp-csharp-getstarted/

How to run this-
-> You would require VS 2015 with Azure SDK and UWP tools installed + RP2 with Windows IoT Core + FEZ Hat 
-> in the solution replace the connection details as commented in the MainPage.xaml.cs file.

