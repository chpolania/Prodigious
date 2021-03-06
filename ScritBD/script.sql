USE [master]
GO
/****** Object:  Database [WebApiDb]    Script Date: 18/11/2016 11:15:13 a. m. ******/
CREATE DATABASE [WebApiDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WebApiDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\WebApiDb.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WebApiDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\WebApiDb_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WebApiDb] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebApiDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebApiDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WebApiDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WebApiDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WebApiDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WebApiDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [WebApiDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WebApiDb] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [WebApiDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WebApiDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WebApiDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WebApiDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WebApiDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WebApiDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WebApiDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WebApiDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WebApiDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WebApiDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WebApiDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WebApiDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WebApiDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WebApiDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WebApiDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WebApiDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WebApiDb] SET RECOVERY FULL 
GO
ALTER DATABASE [WebApiDb] SET  MULTI_USER 
GO
ALTER DATABASE [WebApiDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WebApiDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WebApiDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WebApiDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'WebApiDb', N'ON'
GO
USE [WebApiDb]
GO
/****** Object:  StoredProcedure [dbo].[GetProduct]    Script Date: 18/11/2016 11:15:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create  PROCEDURE [dbo].[GetProduct]
(

@pid INT
)
AS
BEGIN
SELECT * from Products where ProductId=@pid
END

GO
/****** Object:  Table [dbo].[Products]    Script Date: 18/11/2016 11:15:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ProductNumber] [nvarchar](25) NOT NULL,
	[Color] [nvarchar](15) NULL,
	[StandardCost] [money] NOT NULL,
	[ListPrice] [money] NOT NULL,
	[Size] [nvarchar](15) NULL,
	[Weight] [decimal](8, 2) NULL,
	[ProductCategoryID] [int] NULL,
	[ProductModelID] [int] NULL,
	[SellStartDate] [datetime] NOT NULL,
	[SellEndDate] [datetime] NULL,
	[DiscontinuedDate] [datetime] NULL,
	[ThumbNailPhoto] [varbinary](max) NULL,
	[ThumbnailPhotoFileName] [nvarchar](50) NULL,
	[rowguid] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Tokens]    Script Date: 18/11/2016 11:15:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tokens](
	[TokenId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AuthToken] [nvarchar](250) NOT NULL,
	[IssuedOn] [datetime] NOT NULL,
	[ExpiresOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Tokens] PRIMARY KEY CLUSTERED 
(
	[TokenId] ASC,
	[AuthToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 18/11/2016 11:15:14 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [Name], [ProductNumber], [Color], [StandardCost], [ListPrice], [Size], [Weight], [ProductCategoryID], [ProductModelID], [SellStartDate], [SellEndDate], [DiscontinuedDate], [ThumbNailPhoto], [ThumbnailPhotoFileName], [rowguid], [ModifiedDate]) VALUES (1, N'iphone1', N'1', N'1', 1.0000, 1.0000, N'1', CAST(1.00 AS Decimal(8, 2)), 1, 1, CAST(0x0000918700000000 AS DateTime), CAST(0x0000901A00000000 AS DateTime), CAST(0x0000901A00000000 AS DateTime), NULL, NULL, N'20cef166-424c-4c3a-9735-5f1f9f9a2867', CAST(0x0000901A00000000 AS DateTime))
INSERT [dbo].[Products] ([ProductID], [Name], [ProductNumber], [Color], [StandardCost], [ListPrice], [Size], [Weight], [ProductCategoryID], [ProductModelID], [SellStartDate], [SellEndDate], [DiscontinuedDate], [ThumbNailPhoto], [ThumbnailPhotoFileName], [rowguid], [ModifiedDate]) VALUES (2, N'chr', N'1', NULL, 1.0000, 1.0000, NULL, NULL, 1, NULL, CAST(0x0000A6CA00000000 AS DateTime), CAST(0x0000A59D00000000 AS DateTime), CAST(0x0000A59C00000000 AS DateTime), NULL, NULL, N'ecd1ccd9-d69b-4cce-8de6-46d90ed90191', CAST(0x0000A70B00000000 AS DateTime))
INSERT [dbo].[Products] ([ProductID], [Name], [ProductNumber], [Color], [StandardCost], [ListPrice], [Size], [Weight], [ProductCategoryID], [ProductModelID], [SellStartDate], [SellEndDate], [DiscontinuedDate], [ThumbNailPhoto], [ThumbnailPhotoFileName], [rowguid], [ModifiedDate]) VALUES (3, N'bbbc', N'1', NULL, 1.0000, 1.0000, NULL, NULL, 1, NULL, CAST(0x0000A6C100000000 AS DateTime), NULL, NULL, NULL, NULL, N'e2beedc9-fec4-4d41-9bc1-9410fbc25a95', CAST(0x0000A6CD00000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Products] OFF
SET IDENTITY_INSERT [dbo].[Tokens] ON 

INSERT [dbo].[Tokens] ([TokenId], [UserId], [AuthToken], [IssuedOn], [ExpiresOn]) VALUES (17, 1, N'4bffc06f-d8b1-4eda-b4e6-df9568dd53b1', CAST(0x0000A5B80188281E AS DateTime), CAST(0x0000A5B90004E37E AS DateTime))
INSERT [dbo].[Tokens] ([TokenId], [UserId], [AuthToken], [IssuedOn], [ExpiresOn]) VALUES (18, 1, N'50978e84-b5e2-435a-9e50-775e50bd7eb8', CAST(0x0000A5BC011FB9A9 AS DateTime), CAST(0x0000A5BC0123D859 AS DateTime))
INSERT [dbo].[Tokens] ([TokenId], [UserId], [AuthToken], [IssuedOn], [ExpiresOn]) VALUES (19, 1, N'85d5ec6d-e7e3-442c-a9f2-d5eea8574779', CAST(0x0000A5BC0120C0EB AS DateTime), CAST(0x0000A5BC0124DF9B AS DateTime))
INSERT [dbo].[Tokens] ([TokenId], [UserId], [AuthToken], [IssuedOn], [ExpiresOn]) VALUES (20, 1, N'4b451415-8ee9-4af8-a9d7-9093eb365fcb', CAST(0x0000A5BC0121F75C AS DateTime), CAST(0x0000A5BC0126160C AS DateTime))
INSERT [dbo].[Tokens] ([TokenId], [UserId], [AuthToken], [IssuedOn], [ExpiresOn]) VALUES (21, 1, N'f2b39f7e-8b69-45ee-b111-6380c6cf946b', CAST(0x0000A5BC01225C9F AS DateTime), CAST(0x0000A8970079726F AS DateTime))
INSERT [dbo].[Tokens] ([TokenId], [UserId], [AuthToken], [IssuedOn], [ExpiresOn]) VALUES (22, 1, N'2c45ea7d-4643-48de-a7db-26c0343bfefa', CAST(0x0000A5BD00E67479 AS DateTime), CAST(0x0000A5BD00EA9329 AS DateTime))
INSERT [dbo].[Tokens] ([TokenId], [UserId], [AuthToken], [IssuedOn], [ExpiresOn]) VALUES (24, 2, N'4a6ec0ad-b643-44c4-9cc8-18cb88795880', CAST(0x0000A5BC01225C9F AS DateTime), CAST(0x0000A898015C0A3F AS DateTime))
SET IDENTITY_INSERT [dbo].[Tokens] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [UserName], [Password], [Name]) VALUES (1, N'Christianp', N'Christianp', N'Christian Polania')
INSERT [dbo].[User] ([UserId], [UserName], [Password], [Name]) VALUES (2, N'api', N'api', N'API User')
SET IDENTITY_INSERT [dbo].[User] OFF
USE [master]
GO
ALTER DATABASE [WebApiDb] SET  READ_WRITE 
GO
