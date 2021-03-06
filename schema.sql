USE [master]
GO
/****** Object:  Database [Snowinmars.DataBase]    Script Date: 21-May-17 09:24:05 AM ******/
CREATE DATABASE [Snowinmars.DataBase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Snowinmars.DataBase', FILENAME = N'C:\Users\i\Snowinmars.DataBase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Snowinmars.DataBase_log', FILENAME = N'C:\Users\i\Snowinmars.DataBase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Snowinmars.DataBase] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Snowinmars.DataBase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Snowinmars.DataBase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET ARITHABORT OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Snowinmars.DataBase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Snowinmars.DataBase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Snowinmars.DataBase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Snowinmars.DataBase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Snowinmars.DataBase] SET  MULTI_USER 
GO
ALTER DATABASE [Snowinmars.DataBase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Snowinmars.DataBase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Snowinmars.DataBase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Snowinmars.DataBase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Snowinmars.DataBase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Snowinmars.DataBase] SET QUERY_STORE = OFF
GO
USE [Snowinmars.DataBase]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Snowinmars.DataBase]
GO
/****** Object:  Table [dbo].[Authors]    Script Date: 21-May-17 09:24:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authors](
	[AuthorId] [uniqueidentifier] NOT NULL,
	[GivenName] [nvarchar](100) NOT NULL,
	[FamilyName] [nvarchar](100) NULL,
	[Shortcut] [nvarchar](100) NOT NULL,
	[FullMiddleName] [nchar](100) NULL,
	[PseudonymGivenName] [nchar](100) NULL,
	[PseudonymFullMiddleName] [nchar](100) NULL,
	[PseudonymFamilyName] [nchar](100) NULL,
	[MustInformAboutWarnings] [bit] NOT NULL,
	[IsSynchronized] [bit] NOT NULL,
 CONSTRAINT [PK_Authors] PRIMARY KEY CLUSTERED 
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BookAuthorConnection]    Script Date: 21-May-17 09:24:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookAuthorConnection](
	[BookId] [uniqueidentifier] NOT NULL,
	[AuthorId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Books]    Script Date: 21-May-17 09:24:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[BookId] [uniqueidentifier] NOT NULL,
	[PageCount] [int] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Year] [int] NULL,
	[AuthorShortcuts] [nvarchar](1000) NULL,
	[MustInformAboutWarnings] [bit] NOT NULL,
	[Bookshelf] [nvarchar](50) NULL,
	[AdditionalInfo] [nvarchar](1000) NULL,
	[LiveLibUrl] [varchar](200) NULL,
	[LibRusEcUrl] [varchar](200) NULL,
	[FlibustaUrl] [varchar](200) NULL,
	[Owner] [nvarchar](200) NOT NULL,
	[IsSynchronized] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 21-May-17 09:24:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](20) NOT NULL,
	[PasswordHash] [nvarchar](1000) NOT NULL,
	[Roles] [int] NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Salt] [nvarchar](1000) NOT NULL,
	[LanguageCode] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Authors] ADD  DEFAULT ((1)) FOR [MustInformAboutWarnings]
GO
ALTER TABLE [dbo].[Authors] ADD  DEFAULT ((0)) FOR [IsSynchronized]
GO
ALTER TABLE [dbo].[Books] ADD  DEFAULT ((1)) FOR [MustInformAboutWarnings]
GO
ALTER TABLE [dbo].[Books] ADD  DEFAULT ((0)) FOR [IsSynchronized]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [LanguageCode]
GO
USE [master]
GO
ALTER DATABASE [Snowinmars.DataBase] SET  READ_WRITE 
GO
