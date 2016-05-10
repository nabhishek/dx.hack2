Drop table Farm_data;

CREATE TABLE [dbo].[Farm_data] (
    [Id]        INT          NOT NULL IDENTITY(1,1),
    [Device_ID] INT          NOT NULL,
	[Crop_ID] INT NOT NULL,
    [Plot_ID]   INT NOT NULL,
    [Farm_ID] INT NOT NULL,  
	[Temp]      DECIMAL (18) NOT NULL,  
    [Humidity]  DECIMAL (18) NOT NULL,
    [EventEnqueuedUtcTime] DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

Drop table Crop_Master;

CREATE TABLE [dbo].[Crop_Master] (
    [Crop_Id] INT NOT NULL,
    [Crop_Name] Varchar(50) NOT NULL,
	[Threshold] DECIMAL (18) NOT NULL,
    PRIMARY KEY CLUSTERED ([Crop_Id] ASC)
);

Drop table Device_Master;

CREATE TABLE [dbo].[Device_Master] (
    [Device_Id] INT NOT NULL,
    [Device_Name] Varchar(50) NOT NULL)

Drop table Farm_Master;

CREATE TABLE [dbo].[Farm_Master] (
    [Farm_Id] INT NOT NULL,
    [Farm_Name] Varchar(50) NOT NULL)

Drop table Plot_Master;
	
CREATE TABLE [dbo].[Plot_Master] (
    [Plot_Id] INT NOT NULL,
    [Plot_Name] Varchar(50) NOT NULL)

Drop table Farm_Device;

CREATE TABLE [dbo].[Farm_Device] (
    [Fd_Id] INT  NOT NULL IDENTITY(1,1),
    [Farm_Id] INT NOT NULL,
	[Crop_Id] INT NOT NULL,
	[Plot_Id] INT NOT NULL,)

Drop table Farm_DayReport;

CREATE TABLE [dbo].[Farm_DayReport] (
    [Fr_Id]     INT          ,
    [Plot_Id]   INT          NOT NULL,
    [Crop_Name] VARCHAR (50) NOT NULL,
    [Humidity]  DECIMAL (18) NOT NULL,
    [Status]    INT          NOT NULL,
    [datestamp] DATETIME     NOT NULL
);
