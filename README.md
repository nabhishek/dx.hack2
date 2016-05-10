# dx.hack2 - Project F.R.I.E.N.D.S

#Scenario and Problem Domain
1. In most part of rural India, water & electricity is a scarce resource. In most places, electricity is made available with a set time table for the month. They are usually provided for 2 hours during the day and 2 hours in the night. This is the true state of affairs on the ground for farmers. 

2. Second big issue is of man power. A typical farm land spread across 10 acre area will have 1 or 2 individuals as care takers and their daily job is to ensure they water the farm land. Piping is typically done at strategic locations and culverts made to ensure flow of water to the plots which require watering. Which plot to water on a specific day is purely basis their intuition of how wet/moist the land is. 

3. So decision to divert water to a specific plot depends on two factors
    a. Type of Crop
    b. Moisture level in that plot

4. At times they may end up diverting water to an area which is already moist enough to last for a few days. In addition, they also have an opportunity to store water if they feel the crops do not need watering on specific days. 

5. Imagine if they water the plots and it ends up raining in the next few hours. Scenarios like these are common and there is no scientific means for their to address this.


#Architecture:
![alt tag](https://raw.githubusercontent.com/nabhishek/dx.hack2/master/images/architecture.PNG)

Reports:
![alt tag](https://raw.githubusercontent.com/nabhishek/dx.hack2/master/images/reports.PNG)

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

#MyFarm Xamarin App
Contains the Xamarin app solution. Uses hard-coded values to display the color-coded status along with the crop name and humidity information. Required code is present in the solution as well to fetch the information from the Azure backend.

How to run this -

-> Open MainPage.xaml.cs in MyFarm (Portable) project and add the correct URL to fetch the data

-> Bind this information to the listview using the FarmData class

=========================
#ML model for Humidity prediction -

We used "Two Class decision forest" classification model to predict the humidity status for specific crop at certain day of the month. Used available historical datasets for Bangalore ( 2014 & 2015 ) as input.   
