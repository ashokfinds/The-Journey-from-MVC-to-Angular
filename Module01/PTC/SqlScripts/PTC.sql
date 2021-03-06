USE [master]
GO
/****** Object:  Database [PTC]    Script Date: 1/19/2016 9:07:34 AM ******/
CREATE DATABASE [PTC]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PTC', FILENAME = N'D:\SQLServerData\PTC.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PTC_log', FILENAME = N'D:\SQLServerData\PTC_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PTC] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PTC].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PTC] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PTC] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PTC] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PTC] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PTC] SET ARITHABORT OFF 
GO
ALTER DATABASE [PTC] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PTC] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [PTC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PTC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PTC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PTC] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PTC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PTC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PTC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PTC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PTC] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PTC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PTC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PTC] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PTC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PTC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PTC] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PTC] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PTC] SET RECOVERY FULL 
GO
ALTER DATABASE [PTC] SET  MULTI_USER 
GO
ALTER DATABASE [PTC] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PTC] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PTC] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PTC] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PTC', N'ON'
GO
USE [PTC]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 1/19/2016 9:07:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 1/19/2016 9:07:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](80) NULL,
	[IntroductionDate] [datetime] NULL,
	[Price] [money] NULL,
	[Url] [nvarchar](255) NULL,
	[CategoryId] [int] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Category] ON 

GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (1, N'Services')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (2, N'Training')
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (3, N'Information')
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (1, N'SQL Server Naming Standards', CAST(N'2016-01-01 00:00:00.000' AS DateTime), 0.0000, N'http://www.pdsa.com/downloads', 3)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (2, N'Database Development Standards', CAST(N'2016-01-01 00:00:00.000' AS DateTime), 0.0000, N'http://www.pdsa.com/downloads', 3)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (3, N'VB.NET Development Standards', CAST(N'2016-01-01 00:00:00.000' AS DateTime), 0.0000, N'http://www.pdsa.com/downloads', 3)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (4, N'C# Development Standards', CAST(N'2016-01-01 00:00:00.000' AS DateTime), 0.0000, N'http://www.pdsa.com/downloads', 3)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (5, N'VS.NET Standards', CAST(N'2016-01-01 00:00:00.000' AS DateTime), 0.0000, N'http://www.pdsa.com/downloads', 3)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (6, N'PDSA Application Security Audit', CAST(N'2016-01-01 00:00:00.000' AS DateTime), 2000.0000, N'http://www.pdsa.com/audits', 1)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (7, N'PDSA SQL Server and Database Design Audit', CAST(N'2016-01-01 00:00:00.000' AS DateTime), 2000.0000, N'http://www.pdsa.com/audits', 1)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (8, N'PDSA Mentoring', CAST(N'2016-01-01 00:00:00.000' AS DateTime), 200.0000, N'http://www.pdsa.com/mentoring', 1)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (9, N'PDSA Training', CAST(N'2016-01-01 00:00:00.000' AS DateTime), 2000.0000, N'http://www.pdsa.com/training', 1)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (10, N'Build an HTML Helper Library for ASP.NET MVC 5', CAST(N'2016-01-05 00:00:00.000' AS DateTime), 499.0000, N'http://bit.ly/1myXBwj', 2)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (11, N'Consolidating MVC Views using Single Page Techniques', CAST(N'2015-10-09 00:00:00.000' AS DateTime), 499.0000, N'http://bit.ly/1G8TeQO', 2)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (12, N'Extending Bootstrap with CSS, JavaScript and jQuery', CAST(N'2015-06-11 00:00:00.000' AS DateTime), 499.0000, N'http://bit.ly/1SNzc0i', 2)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (13, N'Build your own Bootstrap Business Application Template in MVC', CAST(N'2015-01-29 00:00:00.000' AS DateTime), 499.0000, N'http://bit.ly/1I8ZqZg', 2)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (14, N'Building Mobile Web Sites Using Web Forms, Bootstrap, and HTML5', CAST(N'2014-08-28 00:00:00.000' AS DateTime), 499.0000, N'http://bit.ly/1J2dcrj', 2)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (15, N'How to Start and Run A Consulting Business', CAST(N'2013-09-12 00:00:00.000' AS DateTime), 499.0000, N'http://bit.ly/1L8kOwd', 2)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (16, N'The Many Approaches to XML Processing in .NET Applications', CAST(N'2013-07-22 00:00:00.000' AS DateTime), 499.0000, N'http://bit.ly/1DBfUqd', 2)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (17, N'WPF for the Business Programmer', CAST(N'2009-06-12 00:00:00.000' AS DateTime), 499.0000, N'http://bit.ly/1UF858z', 2)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (18, N'WPF for the Visual Basic Programmer - Part 1', CAST(N'2013-12-16 00:00:00.000' AS DateTime), 499.0000, N'http://bit.ly/1uFxS7C', 2)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (19, N'WPF for the Visual Basic Programmer - Part 2', CAST(N'2014-02-18 00:00:00.000' AS DateTime), 499.0000, N'http://bit.ly/1MjQ9NG', 2)
GO
INSERT [dbo].[Product] ([ProductId], [ProductName], [IntroductionDate], [Price], [Url], [CategoryId]) VALUES (20, N'PDSA Application Performance Audit', CAST(N'2016-01-01 00:00:00.000' AS DateTime), 2000.0000, N'http://www.pdsa.com/audits', 1)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
USE [master]
GO
ALTER DATABASE [PTC] SET  READ_WRITE 
GO
