USE [master]
GO
/****** Object:  Database [SurgePOC]    Script Date: 4/28/2015 3:21:40 PM ******/
CREATE DATABASE [SurgePOC] ON  PRIMARY 
( NAME = N'SurgePOC', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SurgePOC.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SurgePOC_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\SurgePOC_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SurgePOC] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SurgePOC].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SurgePOC] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SurgePOC] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SurgePOC] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SurgePOC] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SurgePOC] SET ARITHABORT OFF 
GO
ALTER DATABASE [SurgePOC] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SurgePOC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SurgePOC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SurgePOC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SurgePOC] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SurgePOC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SurgePOC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SurgePOC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SurgePOC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SurgePOC] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SurgePOC] SET AUTO_UPDATE_STATISTICS_ASYNC ON 
GO
ALTER DATABASE [SurgePOC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SurgePOC] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SurgePOC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SurgePOC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SurgePOC] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SurgePOC] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SurgePOC] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SurgePOC] SET  MULTI_USER 
GO
ALTER DATABASE [SurgePOC] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SurgePOC] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'SurgePOC', N'ON'
GO
USE [SurgePOC]
GO
/****** Object:  User [surgepoc]    Script Date: 4/28/2015 3:21:44 PM ******/
CREATE USER [surgepoc] FOR LOGIN [surgepoc] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  DatabaseRole [gd_execprocs]    Script Date: 4/28/2015 3:21:45 PM ******/
CREATE ROLE [gd_execprocs]
GO
ALTER ROLE [db_owner] ADD MEMBER [surgepoc]
GO
/****** Object:  FullTextCatalog [surgepoc]    Script Date: 4/28/2015 3:21:46 PM ******/
CREATE FULLTEXT CATALOG [surgepoc]WITH ACCENT_SENSITIVITY = ON
AS DEFAULT

GO
/****** Object:  Table [dbo].[Hospital]    Script Date: 4/28/2015 3:21:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Hospital](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HospitalAdmin]    Script Date: 4/28/2015 3:21:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HospitalAdmin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
	[HospitalId] [int] NULL,
	[ProfilePic] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Hospitalist]    Script Date: 4/28/2015 3:21:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Hospitalist](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
	[HospitalId] [int] NULL,
	[ProfilePic] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HospitalStaff]    Script Date: 4/28/2015 3:21:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HospitalStaff](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
	[HospitalId] [int] NULL,
	[ProfilePic] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 4/28/2015 3:21:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Jobs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](max) NULL,
	[JobDesc] [varchar](max) NULL,
	[JobStartDate] [date] NULL,
	[JobStartTime] [time](7) NULL,
	[JobEndTime] [time](7) NULL,
	[Budget] [money] NULL,
	[ForHospital] [bit] NULL,
	[JobStatus] [varchar](30) NULL,
	[CreatorId] [int] NULL,
	[CreatorType] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SurgeStaff]    Script Date: 4/28/2015 3:21:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SurgeStaff](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
	[ProfilePic] [varchar](max) NULL,
	[Rating] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TagJob]    Script Date: 4/28/2015 3:21:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TagJob](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NULL,
	[ProviderId] [int] NULL,
	[BidAmount] [money] NULL,
	[IsAwarded] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TagProvider]    Script Date: 4/28/2015 3:21:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TagProvider](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NULL,
	[ProviderId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Hospital] ON 

GO
INSERT [dbo].[Hospital] ([ID], [Name]) VALUES (1, N'Appolo Hospitals')
GO
SET IDENTITY_INSERT [dbo].[Hospital] OFF
GO
SET IDENTITY_INSERT [dbo].[HospitalAdmin] ON 

GO
INSERT [dbo].[HospitalAdmin] ([ID], [Name], [HospitalId], [ProfilePic]) VALUES (1, N'Donna Williams', 1, N'admin2.jpg')
GO
SET IDENTITY_INSERT [dbo].[HospitalAdmin] OFF
GO
SET IDENTITY_INSERT [dbo].[Hospitalist] ON 

GO
INSERT [dbo].[Hospitalist] ([ID], [Name], [HospitalId], [ProfilePic]) VALUES (1, N'Dr. Denita Daplin', 1, N'admin1.jpg')
GO
SET IDENTITY_INSERT [dbo].[Hospitalist] OFF
GO
SET IDENTITY_INSERT [dbo].[HospitalStaff] ON 

GO
INSERT [dbo].[HospitalStaff] ([ID], [Name], [HospitalId], [ProfilePic]) VALUES (1, N'Rachel Jones', 1, N'admin3.jpg')
GO
SET IDENTITY_INSERT [dbo].[HospitalStaff] OFF
GO
SET IDENTITY_INSERT [dbo].[Jobs] ON 

GO
INSERT [dbo].[Jobs] ([ID], [Title], [JobDesc], [JobStartDate], [JobStartTime], [JobEndTime], [Budget], [ForHospital], [JobStatus], [CreatorId], [CreatorType]) VALUES (1, N'Test Task 1', N'Looking for Test task 1', CAST(N'2015-04-27' AS Date), CAST(N'12:38:00' AS Time), CAST(N'14:38:00' AS Time), 500.0000, 1, N'Submitted', 1, N'Hospitalist')
GO
INSERT [dbo].[Jobs] ([ID], [Title], [JobDesc], [JobStartDate], [JobStartTime], [JobEndTime], [Budget], [ForHospital], [JobStatus], [CreatorId], [CreatorType]) VALUES (2, N'Test Task 2', N'Looking for test task 2', CAST(N'2015-04-27' AS Date), CAST(N'12:39:00' AS Time), CAST(N'15:39:00' AS Time), 450.0000, 1, N'Completed', 1, N'Hospitalist')
GO
INSERT [dbo].[Jobs] ([ID], [Title], [JobDesc], [JobStartDate], [JobStartTime], [JobEndTime], [Budget], [ForHospital], [JobStatus], [CreatorId], [CreatorType]) VALUES (3, N'Test Task 3', N'Looking for test task 3', CAST(N'2015-04-27' AS Date), CAST(N'12:39:00' AS Time), CAST(N'16:39:00' AS Time), 350.0000, 1, N'Inprogress', 1, N'Hospitalist')
GO
INSERT [dbo].[Jobs] ([ID], [Title], [JobDesc], [JobStartDate], [JobStartTime], [JobEndTime], [Budget], [ForHospital], [JobStatus], [CreatorId], [CreatorType]) VALUES (4, N'Test Task 4', N'Looking for test task 4', CAST(N'2015-04-27' AS Date), CAST(N'13:23:00' AS Time), CAST(N'15:23:00' AS Time), 500.0000, 1, N'Completed', 1, N'Hospitalist')
GO
INSERT [dbo].[Jobs] ([ID], [Title], [JobDesc], [JobStartDate], [JobStartTime], [JobEndTime], [Budget], [ForHospital], [JobStatus], [CreatorId], [CreatorType]) VALUES (5, N'Test Task 5', N'Looking for test task 5', CAST(N'2015-04-27' AS Date), CAST(N'13:23:00' AS Time), CAST(N'16:23:00' AS Time), 450.0000, 1, N'Completed', 1, N'Hospitalist')
GO
INSERT [dbo].[Jobs] ([ID], [Title], [JobDesc], [JobStartDate], [JobStartTime], [JobEndTime], [Budget], [ForHospital], [JobStatus], [CreatorId], [CreatorType]) VALUES (6, N'Test Task 6', N'Looking for test task 6', CAST(N'2015-04-27' AS Date), CAST(N'13:54:00' AS Time), CAST(N'15:54:00' AS Time), 450.0000, 1, N'Completed', 1, N'Hospitalist')
GO
INSERT [dbo].[Jobs] ([ID], [Title], [JobDesc], [JobStartDate], [JobStartTime], [JobEndTime], [Budget], [ForHospital], [JobStatus], [CreatorId], [CreatorType]) VALUES (7, N'Test Task 7', N'Looking for test task 7', CAST(N'2015-04-27' AS Date), CAST(N'14:43:00' AS Time), CAST(N'17:43:00' AS Time), 450.0000, 1, N'Inprogress', 1, N'Hospitalist')
GO
INSERT [dbo].[Jobs] ([ID], [Title], [JobDesc], [JobStartDate], [JobStartTime], [JobEndTime], [Budget], [ForHospital], [JobStatus], [CreatorId], [CreatorType]) VALUES (8, N'Test Task 10', N'Looking for test task 19', CAST(N'2015-04-27' AS Date), CAST(N'16:51:00' AS Time), CAST(N'18:51:00' AS Time), 450.0000, 1, N'New', 1, N'Hospitalist')
GO
INSERT [dbo].[Jobs] ([ID], [Title], [JobDesc], [JobStartDate], [JobStartTime], [JobEndTime], [Budget], [ForHospital], [JobStatus], [CreatorId], [CreatorType]) VALUES (9, N'Test Task 8', N'Looking for test task 8', CAST(N'2015-04-27' AS Date), CAST(N'22:16:00' AS Time), CAST(N'22:16:00' AS Time), 500.0000, 1, N'Completed', 1, N'Hospitalist')
GO
INSERT [dbo].[Jobs] ([ID], [Title], [JobDesc], [JobStartDate], [JobStartTime], [JobEndTime], [Budget], [ForHospital], [JobStatus], [CreatorId], [CreatorType]) VALUES (10, N'Test Task 9', N'Looking for test task 9', CAST(N'2015-04-27' AS Date), CAST(N'23:51:00' AS Time), CAST(N'23:51:00' AS Time), 340.0000, 1, N'New', 1, N'Hospitalist')
GO
INSERT [dbo].[Jobs] ([ID], [Title], [JobDesc], [JobStartDate], [JobStartTime], [JobEndTime], [Budget], [ForHospital], [JobStatus], [CreatorId], [CreatorType]) VALUES (11, N'Test Task 11', N'Looking for test task 11', CAST(N'2015-04-28' AS Date), CAST(N'08:58:00' AS Time), CAST(N'08:58:00' AS Time), 500.0000, 1, N'Awarded', 1, N'Hospitalist')
GO
INSERT [dbo].[Jobs] ([ID], [Title], [JobDesc], [JobStartDate], [JobStartTime], [JobEndTime], [Budget], [ForHospital], [JobStatus], [CreatorId], [CreatorType]) VALUES (12, N'Test Task 12', N'Looking for test task 12', CAST(N'2015-04-28' AS Date), CAST(N'13:31:00' AS Time), CAST(N'15:31:00' AS Time), 450.0000, 1, N'New', 1, N'Hospitalist')
GO
SET IDENTITY_INSERT [dbo].[Jobs] OFF
GO
SET IDENTITY_INSERT [dbo].[SurgeStaff] ON 

GO
INSERT [dbo].[SurgeStaff] ([ID], [Name], [ProfilePic], [Rating]) VALUES (1, N'Dr. Harvey', N'doctor1.jpg', 5)
GO
INSERT [dbo].[SurgeStaff] ([ID], [Name], [ProfilePic], [Rating]) VALUES (2, N'Dr. Mike', N'doctor2.jpg', 5)
GO
INSERT [dbo].[SurgeStaff] ([ID], [Name], [ProfilePic], [Rating]) VALUES (3, N'Dr. Louis', N'doctor3.jpg', 4)
GO
INSERT [dbo].[SurgeStaff] ([ID], [Name], [ProfilePic], [Rating]) VALUES (4, N'Dr. Jessica', N'doctor4.jpg', 5)
GO
INSERT [dbo].[SurgeStaff] ([ID], [Name], [ProfilePic], [Rating]) VALUES (5, N'Dr. Cameron', N'doctor5.jpg', 3)
GO
SET IDENTITY_INSERT [dbo].[SurgeStaff] OFF
GO
SET IDENTITY_INSERT [dbo].[TagJob] ON 

GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (1, 1, 1, 400.0000, 1)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (2, 1, 2, 350.0000, 0)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (3, 1, 4, 500.0000, 0)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (4, 3, 2, 300.0000, 1)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (5, 3, 4, 280.0000, 0)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (6, 3, 2, 400.0000, 1)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (7, 2, 4, 300.0000, 1)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (8, 4, 4, 400.0000, 0)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (9, 5, 4, 400.0000, 1)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (10, 5, 3, 420.0000, 0)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (11, 4, 2, 400.0000, 0)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (12, 4, 1, 380.0000, 1)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (13, 6, 1, 400.0000, 1)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (14, 7, 1, 350.0000, 1)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (15, 7, 3, 300.0000, 0)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (16, 5, 4, 500.0000, 1)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (17, 9, 2, 340.0000, 0)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (18, 9, 4, 280.0000, 1)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (19, 11, 1, 450.0000, 1)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (20, 11, 3, 300.0000, 1)
GO
INSERT [dbo].[TagJob] ([ID], [JobId], [ProviderId], [BidAmount], [IsAwarded]) VALUES (21, 8, 1, 400.0000, 0)
GO
SET IDENTITY_INSERT [dbo].[TagJob] OFF
GO
SET IDENTITY_INSERT [dbo].[TagProvider] ON 

GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (2, 1, 2)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (3, 2, 4)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (4, 3, 4)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (5, 3, 2)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (6, 4, 1)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (7, 4, 2)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (8, 4, 4)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (9, 5, 4)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (10, 5, 3)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (11, 6, 5)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (12, 6, 2)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (13, 5, 1)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (14, 7, 1)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (15, 7, 3)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (16, 8, 1)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (17, 8, 4)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (18, 9, 4)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (19, 9, 2)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (20, 10, 1)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (21, 10, 2)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (22, 11, 1)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (23, 11, 4)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (24, 11, 2)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (25, 12, 1)
GO
INSERT [dbo].[TagProvider] ([ID], [JobId], [ProviderId]) VALUES (26, 12, 4)
GO
SET IDENTITY_INSERT [dbo].[TagProvider] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAppUsers]    Script Date: 4/28/2015 3:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetAppUsers]
AS
BEGIN
	SELECT ID, Name, ProfilePic, 'Hospitalist' AS Role FROM Hospitalist
	UNION
	SELECT ID, Name, ProfilePic, 'Hospital Admin' AS Role FROM HospitalAdmin
	UNION
	SELECT ID, Name, ProfilePic, 'Hospital Staff' AS Role FROM HospitalStaff
	UNION 
	SELECT ID, Name, ProfilePic, 'Provider' AS Role FROM SurgeStaff
	ORDER BY Role
END

GO
/****** Object:  StoredProcedure [dbo].[SP_GetSetJobs]    Script Date: 4/28/2015 3:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetSetJobs] 
	 @id			INT = NULL,
	 @title			VARCHAR(MAX) = NULL,
	 @jobDesc		VARCHAR(MAX) = NULL,
	 @jobStartDate	DATE = NULL,
	 @jobSTartTime	TIME = NULL,
	 @jobEndTime	TIME = NULL,
	 @budget		MONEY = NULL,
	 @forHospital	BIT = NULL,
	 @jobStatus		VARCHAR(30) = NULL,
	 @creatorId		INT = NULL,
	 @creatorType   VARCHAR(30) = NULL,
	 @adminId		INT = NULL,
	 @hospitalistId INT = NULL,
	 @staffId		INT = NULL,
	 @providerId	INT = NULL,
	 @ptype			INT
AS
BEGIN
	SET NOCOUNT ON;
	
	-- FOR CREATING NEW JOB
    IF(@ptype = 1)
    BEGIN
		INSERT INTO Jobs VALUES (@title, @jobDesc, @jobStartDate, @jobStartTime, @jobEndTime, @budget, @forHospital, 'New', @creatorId, @creatorType)
		SELECT SCOPE_IDENTITY() AS NewJobId
    END
    
    --FOR UPDATE STATUS
    ELSE IF(@ptype = 2)
    BEGIN
		UPDATE jobs
		SET jobStatus = @jobStatus
		WHERE id = @id
    END
    
    --To Retrieve specific job details
    ELSE IF(@ptype = 3)
    BEGIN 
		SELECT ID, Title, JobDesc, JobStartDate, JobStartTime, JobEndTime, CAST(Budget AS INTEGER) AS BUDGET, ForHospital, JobStatus, CreatorId, CreatorType FROM Jobs WHERE id = @ID
    END
    
    --To retrieve all jobs for Admin
    ELSE IF(@ptype = 4)
    BEGIN
		SELECT * FROM jobs ORDER BY jobStatus, ID
    END

	--To retrieve jobs for provider view
	ELSE IF(@ptype = 5)
	BEGIN 
		--SELECT * FROM jobs WHERE ID IN
		--(SELECT jobID FROM tagjob WHERE providerid = @providerid)
		--OR
		--ID IN
		--(SELECT jobId FROM tagprovider WHERE providerid = @providerid AND jobid NOT IN (SELECT jobid FROM tagjob WHERE providerid = @providerid))
		--ORDER BY jobStatus, ID

		--To retrieve job awarded, invited, in progress, completed and new 
		SELECT * FROM jobs WHERE jobStatus = 'Awarded' AND ID IN(
		SELECT jobid FROM TagJob WHERE providerId = @providerId)
		UNION
		SELECT ID, Title, JobDesc, JobStartDate, JobStartTime, JobEndTime, Budget, ForHospital, 'Invited' AS ['jobStatus], CreatorId, CreatorType FROM jobs WHERE jobStatus = 'NEW' 
		AND ID IN (
		SELECT jobId FROM TagProvider WHERE providerId = @providerId)
		UNION
		SELECT * FROM jobs WHERE jobStatus = 'Inprogress' AND ID IN(
		SELECT jobId FROM TagJob WHERE providerId = @providerId AND isawarded = 1)
		UNION 
		SELECT * FROM jobs WHERE jobStatus = 'Completed' AND ID IN(
		SELECT jobId FROM TagJob WHERE providerId = @providerId)
		UNION
		SELECT * FROM jobs WHERE jobstatus = 'New'
		ORDER BY jobStatus
	END

END

--EXEC SP_GetSetJobs @id = 1, @ptype = 3
--EXEC SP_GetSetJobs @providerId = 1, @ptype = 5

--SELECT * FROM JOBS
--SELECT * FROM TAGJOB
--SELECT * FROM TAGPROVIDER
--INSERT INTO TagJob VALUES (3, 1, 450, 0)
--INSERT INTO TagJob VALUES (4, 2, 420, 0)
--INSERT INTO TagJob VALUES (5, 1, 250, 0)

GO
/****** Object:  StoredProcedure [dbo].[SP_GetSetTagJobs]    Script Date: 4/28/2015 3:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetSetTagJobs]
	@id				INT = NULL,
	@jobId			INT	= NULL,
	@providerId		INT = NULL,
	@bidAmount		MONEY = NULL,
	@isAwarded		BIT = NULL,
	@jobStatus		VARCHAR(30) = NULL,
	@ptype			INT
AS
BEGIN

	SET NOCOUNT ON;

    -- To tag job by providers with their bid amount
    IF(@ptype = 1)
    BEGIN
		INSERT INTO TagJob VALUES (@jobId, @providerId, @bidAmount,0)
    END
    
    --To Award a job to a provider
    ELSE IF(@ptype = 2)
    BEGIN
		UPDATE TagJob SET isAwarded = 1 WHERE jobId = @jobId AND providerId = @providerId
		UPDATE Jobs SET JobStatus = 'Awarded' WHERE ID = @jobId
    END
    
    --To get list of providers tagged to a job
    ELSE IF(@ptype = 3)
    BEGIN
		SELECT SS.ID, SS.Name, SS.ProfilePic, RES.ProviderID,  CAST(RES.BidAmount AS integer) AS BidAmount, SS.Rating, RES.IsAwarded FROM
		(SELECT * FROM TagJob WHERE jobId = @jobId) AS RES
		INNER JOIN
		SurgeStaff AS SS
		ON RES.ProviderId = SS.ID
    END

	--To get provider details based on job id
	ELSE IF(@ptype = 4)
	BEGIN
	    SELECT SS.ID, SS.Name, SS.ProfilePic,  RES.ProviderID, CAST(RES.BidAmount AS integer) AS BidAmount, SS.Rating, RES.IsAwarded FROM
		(SELECT * FROM TagJob WHERE jobId = @jobId AND providerId = @providerId) AS RES
		INNER JOIN
		SurgeStaff AS SS
		ON RES.ProviderId = SS.ID
	END

	--To Update Status of a Job
	ELSE IF(@ptype = 5)
	BEGIN
		UPDATE Jobs SET jobStatus = @jobStatus WHERE id = @jobId
	END

END

--EXEC SP_GetSetTagJobs @jobid = 1, @ptype = 3
--EXEC SP_GetSetTagJobs @jobid = 1, @providerId = 3, @ptype = 4
--SELECT * FROM Jobs WHERE ID = 1
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSetTagProviders]    Script Date: 4/28/2015 3:21:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetSetTagProviders]
	@ID				INT = NULL,
	@jobId			INT = NULL,
	@providerId		INT = NULL,
	@ptype			INT
AS
BEGIN

	SET NOCOUNT ON;

    -- To Tag a provider on to a job
    IF(@ptype = 1)
    BEGIN
		IF ((SELECT COUNT(1) FROM TagProvider WHERE jobid = @jobId AND providerid = @providerId) = 0)
		BEGIN
			INSERT INTO TagProvider VALUES (@jobId, @providerId)
		END
	END
    
    --To retrieve all providers who are tagged on to a job
    ELSE IF(@ptype = 2)
    BEGIN
		SELECT * FROM TagProvider WHERE jobid = @jobId
    END

	--To retrieve all providers who are mapped to a hospital
	ELSE IF(@ptype = 3)
	BEGIN
		SELECT * FROM SurgeStaff
	END
END


GO
USE [master]
GO
ALTER DATABASE [SurgePOC] SET  READ_WRITE 
GO
