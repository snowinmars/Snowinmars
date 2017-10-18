USE [master]
GO
/****** Object:  Database [Snowinmars.DataBase]    Script Date: 18-Oct-17 23:23:44 ******/
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
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
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
/****** Object:  Table [dbo].[Authors]    Script Date: 18-Oct-17 23:23:44 ******/
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
/****** Object:  Table [dbo].[BookAuthorConnection]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BookAuthorConnection](
	[BookId] [uniqueidentifier] NOT NULL,
	[AuthorId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 18-Oct-17 23:23:44 ******/
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
	[AdditionalInfo] [nvarchar](1000) NOT NULL,
	[LiveLibUrl] [varchar](200) NULL,
	[LibRusEcUrl] [varchar](200) NULL,
	[FlibustaUrl] [varchar](200) NULL,
	[Owner] [nvarchar](200) NOT NULL,
	[IsSynchronized] [bit] NOT NULL,
	[Status] [int] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FilmAuthorConnection]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FilmAuthorConnection](
	[FilmId] [uniqueidentifier] NOT NULL,
	[AuthorId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED
(
	[FilmId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Films]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Films](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Year] [int] NULL,
	[KinopoiskUrl] [nvarchar](200) NULL,
	[Description] [nvarchar](1000) NULL,
PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 18-Oct-17 23:23:44 ******/
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
/****** Object:  StoredProcedure [dbo].[Author_Delete]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Author_Delete]
(
	@authorId UNIQUEIDENTIFIER
)
AS
BEGIN

	DELETE
	FROM [Authors]
	WHERE (AuthorId = @authorId)

END
GO
/****** Object:  StoredProcedure [dbo].[Author_Get]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Author_Get] (@authorId UNIQUEIDENTIFIER)
AS
BEGIN

	SELECT ba.BookId
		,a.AuthorId
		,a.Shortcut
		,a.GivenName
		,a.FamilyName
		,a.IsSynchronized
		,a.FullMiddleName
		,a.PseudonymGivenName
		,a.PseudonymFamilyName
		,a.PseudonymFullMiddleName
		,a.MustInformAboutWarnings
	FROM [Authors] a
		inner join [BookAuthorConnection] ba
			on a.AuthorId = ba.AuthorId
	WHERE (a.AuthorId = @authorId)

END
GO
/****** Object:  StoredProcedure [dbo].[Author_GetAll]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Author_GetAll]
AS
BEGIN

	SELECT ba.BookId
		,a.AuthorId
		,a.Shortcut
		,a.GivenName
		,a.FamilyName
		,a.IsSynchronized
		,a.FullMiddleName
		,a.PseudonymGivenName
		,a.PseudonymFamilyName
		,a.PseudonymFullMiddleName
		,a.MustInformAboutWarnings
	FROM [Authors] a
		inner join [BookAuthorConnection] ba
			on a.AuthorId = ba.AuthorId

END
GO
/****** Object:  StoredProcedure [dbo].[Author_Insert]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Author_Insert] (
	@authorId UNIQUEIDENTIFIER
	,@shortcut nvarchar(100)
	,@givenName nvarchar(100)
	,@familyName nvarchar(100)
	,@isSynchronized bit
	,@fullMiddleName nvarchar(100)
	,@pseudonymGivenName nvarchar(100)
	,@pseudonymFamilyName nvarchar(100)
	,@pseudonymFullMiddleName nvarchar(100)
	)
AS
BEGIN

	INSERT INTO [Authors] (
		AuthorId
		,Shortcut
		,GivenName
		,FamilyName
		,IsSynchronized
		,FullMiddleName
		,PseudonymGivenName
		,PseudonymFamilyName
		,PseudonymFullMiddleName
		)
	VALUES (
		@authorId
		,@shortcut
		,@givenName
		,@familyName
		,@isSynchronized
		,@fullMiddleName
		,@pseudonymGivenName
		,@pseudonymFamilyName
		,@pseudonymFullMiddleName
		)

END
GO
/****** Object:  StoredProcedure [dbo].[Author_Update]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Author_Update] (
	@authorId UNIQUEIDENTIFIER
	,@shortcut NVARCHAR(100)
	,@givenName NVARCHAR(100)
	,@familyName NVARCHAR(100)
	,@isSynchronized BIT
	,@fullMiddleName NVARCHAR(100)
	,@pseudonymGivenName NVARCHAR(100)
	,@pseudonymFamilyName NVARCHAR(100)
	,@pseudonymFullMiddleName NVARCHAR(100)
	)
AS
BEGIN

	UPDATE [Authors]
	SET GivenName = @givenName
		,Shortcut = @shortcut
		,FamilyName = @familyName
		,IsSynchronized = @isSynchronized
		,FullMiddleName = @fullMiddleName
		,PseudonymGivenName = @pseudonymGivenName
		,PseudonymFamilyName = @pseudonymFamilyName
		,PseudonymFullMiddleName = @pseudonymFullMiddleName
	WHERE (AuthorId = @authorId)

END
GO
/****** Object:  StoredProcedure [dbo].[Book_Delete]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Book_Delete]
(
	@bookId UNIQUEIDENTIFIER
)
AS
BEGIN

delete
from [Books]
where [BookId] = @bookId

END

GO
/****** Object:  StoredProcedure [dbo].[Book_Get]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Book_Get]
(
	@id UNIQUEIDENTIFIER
)
AS
BEGIN

SELECT b.[BookId]					AS BookId
	,b.[Year] 						AS Year
	,b.[Title] 						AS Title
	,b.[Owner] 						AS Owner
	,b.[Status] 					AS Status
	,b.[PageCount] 					AS PageCount
	,b.[Bookshelf] 					AS Bookshelf
	,b.[LiveLibUrl] 				AS LiveLibUrl
	,b.[LibRusEcUrl] 				AS LibRusEcUrl
	,b.[FlibustaUrl]				AS FlibustaUrl
	,b.[IsSynchronized] 			AS IsSynchronized
	,b.[AdditionalInfo] 			AS AdditionalInfo
	,b.[MustInformAboutWarnings] 	AS MustInformAboutWarnings
FROM [dbo].[Books] b
WHERE b.[BookId] = @id



SELECT a.[AuthorId] 				AS AuthorId
	,a.[FamilyName] 				AS FamilyName
	,a.[FullMiddleName] 			AS FullMiddleName
	,a.[GivenName] 					AS GivenName
	,a.[PseudonymFamilyName] 		AS PseudonymFamilyName
	,a.[PseudonymFullMiddleName] 	AS PseudonymFullMiddleName
	,a.[PseudonymGivenName] 		AS PseudonymGivenName
	,a.[Shortcut] 					AS Shortcut
	,a.[IsSynchronized] 			AS IsSynchronized
FROM [dbo].[Authors] a
	INNER JOIN [dbo].[BookAuthorConnection] ba
		ON a.[AuthorId] = ba.[AuthorId]
WHERE ba.[BookId] = @id

END

GO
/****** Object:  StoredProcedure [dbo].[Book_GetAll]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Book_GetAll]
AS
BEGIN

SELECT b.[BookId]					AS BookId
	,b.[Year] 						AS Year
	,b.[Title] 						AS Title
	,b.[Owner] 						AS Owner
	,b.[Status] 					AS Status
	,b.[PageCount] 					AS PageCount
	,b.[Bookshelf] 					AS Bookshelf
	,b.[LiveLibUrl] 				AS LiveLibUrl
	,b.[LibRusEcUrl] 				AS LibRusEcUrl
	,b.[FlibustaUrl]				AS FlibustaUrl
	,b.[IsSynchronized] 			AS IsSynchronized
	,b.[AdditionalInfo] 			AS AdditionalInfo
	,b.[MustInformAboutWarnings] 	AS MustInformAboutWarnings
FROM [dbo].[Books] b



SELECT ba.[BookId]					AS BookId
	,a.[AuthorId] 					AS AuthorId
	,a.[FamilyName] 				AS FamilyName
	,a.[FullMiddleName] 			AS FullMiddleName
	,a.[GivenName] 					AS GivenName
	,a.[PseudonymFamilyName] 		AS PseudonymFamilyName
	,a.[PseudonymFullMiddleName] 	AS PseudonymFullMiddleName
	,a.[PseudonymGivenName] 		AS PseudonymGivenName
	,a.[Shortcut] 					AS Shortcut
	,a.[IsSynchronized] 			AS IsSynchronized
FROM [dbo].[Authors] a
	INNER JOIN [dbo].[BookAuthorConnection] ba
		ON a.[AuthorId] = ba.[AuthorId]

END

GO
/****** Object:  StoredProcedure [dbo].[Book_GetAllWishlist]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Book_GetAllWishlist]
(
	@owner nvarchar(200)
)
AS
BEGIN

SELECT b.[BookId]					AS BookId
	,b.[Year] 						AS Year
	,b.[Title] 						AS Title
	,b.[Owner] 						AS Owner
	,b.[Status] 					AS Status
	,b.[PageCount] 					AS PageCount
	,b.[Bookshelf] 					AS Bookshelf
	,b.[LiveLibUrl] 				AS LiveLibUrl
	,b.[LibRusEcUrl] 				AS LibRusEcUrl
	,b.[FlibustaUrl]				AS FlibustaUrl
	,b.[IsSynchronized] 			AS IsSynchronized
	,b.[AdditionalInfo] 			AS AdditionalInfo
	,b.[MustInformAboutWarnings] 	AS MustInformAboutWarnings
FROM [dbo].[Books] b
where b.[Status] = 0 and
	b.[Owner] = @owner



SELECT ba.[BookId]					AS BookId
	,a.[AuthorId] 					AS AuthorId
	,a.[FamilyName] 				AS FamilyName
	,a.[FullMiddleName] 			AS FullMiddleName
	,a.[GivenName] 					AS GivenName
	,a.[PseudonymFamilyName] 		AS PseudonymFamilyName
	,a.[PseudonymFullMiddleName] 	AS PseudonymFullMiddleName
	,a.[PseudonymGivenName] 		AS PseudonymGivenName
	,a.[Shortcut] 					AS Shortcut
	,a.[IsSynchronized] 			AS IsSynchronized
FROM [dbo].[Authors] a
	INNER JOIN [dbo].[BookAuthorConnection] ba
		ON a.[AuthorId] = ba.[AuthorId]
	inner join [dbo].[Books] b
		on ba.[BookId] = b.[BookId]
where b.[Status] = 0 and
	b.[Owner] = @owner


END

GO
/****** Object:  StoredProcedure [dbo].[Book_GetUnsynchronized]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Book_GetUnsynchronized]
AS
BEGIN

select b.BookId
from [Books] b
where b.IsSynchronized = 0

END

GO
/****** Object:  StoredProcedure [dbo].[Book_Insert]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Book_Insert]
(
	@bookId UNIQUEIDENTIFIER
	,@year INT
	,@title NVARCHAR (200)
	,@owner nvarchar(200)
	,@status int
	,@pageCount INT
	,@bookshelf nvarchar(50)
	,@liveLibUrl varchar(200)
	,@libRusEcUrl varchar(200)
	,@flibustaUrl varchar(200)
	,@isSynchronized bit
	,@additionalInfo nvarchar(1000)
)
AS
BEGIN

INSERT INTO [Books] (
	[BookId]
	,[Year]
	,[Title]
	,[Owner]
	,[Status]
	,[PageCount]
	,[Bookshelf]
	,[LiveLibUrl]
	,[LibRusEcUrl]
	,[FlibustaUrl]
	,[IsSynchronized]
	,[AdditionalInfo]
	)
VALUES (
	@bookId
	,@year
	,@title
	,@owner
	,@status
	,@pageCount
	,@bookshelf
	,@liveLibUrl
	,@libRusEcUrl
	,@flibustaUrl
	,@isSynchronized
	,@additionalInfo
	)

END

GO
/****** Object:  StoredProcedure [dbo].[Book_Update]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Book_Update]
(
	@bookId UNIQUEIDENTIFIER
	,@year INT
	,@title NVARCHAR (200)
	,@owner nvarchar(200)
	,@status int
	,@pageCount INT
	,@bookshelf nvarchar(50)
	,@liveLibUrl varchar(200)
	,@libRusEcUrl varchar(200)
	,@flibustaUrl varchar(200)
	,@isSynchronized bit
	,@additionalInfo nvarchar(1000)
)
AS
BEGIN


	UPDATE [Books]
	SET [Title] = @title
		,[Year] = @year
		,[Owner] = @owner
		,[Status] = @status
		,[PageCount] = @pageCount
		,[Bookshelf] = @bookshelf
		,[LiveLibUrl] = @liveLibUrl
		,[LibRusEcUrl] = @libRusEcUrl
		,[FlibustaUrl] = @flibustaUrl
		,[IsSynchronized] = @isSynchronized
		,[AdditionalInfo] = @additionalInfo
	WHERE (BookId = @bookId)


END

GO
/****** Object:  StoredProcedure [dbo].[BookAuthorConnection_Delete]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BookAuthorConnection_Delete]
(
        @bookId UNIQUEIDENTIFIER
        ,@authorId UNIQUEIDENTIFIER
)
AS
BEGIN

delete
from [dbo].[BookAuthorConnection]
where
(
        [BookId] = @bookId and
        [AuthorId] = @authorId
)

END

GO
/****** Object:  StoredProcedure [dbo].[BookAuthorConnection_Get]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BookAuthorConnection_Get]
(
	@bookId UNIQUEIDENTIFIER
)
AS
BEGIN

select ba.BookId
	,ba.AuthorId
from [dbo].[BookAuthorConnection] ba
where ba.BookId = @bookId

END

GO
/****** Object:  StoredProcedure [dbo].[BookAuthorConnection_GetAll]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BookAuthorConnection_GetAll]
AS
BEGIN

select ba.BookId
,ba.AuthorId
from [dbo].[BookAuthorConnection] ba

END

GO
/****** Object:  StoredProcedure [dbo].[BookAuthorConnection_Insert]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BookAuthorConnection_Insert]
(
	@bookId UNIQUEIDENTIFIER,
	@authorId UNIQUEIDENTIFIER
)
AS
BEGIN

INSERT INTO [BookAuthorConnection] (
	BookId
	,AuthorId
	)
VALUES (
	@bookId
	,@authorId
	)

END
IF OBJECT_ID(N'[dbo].[Book_Delete]', N'P') IS NULL
BEGIN
    PRINT 'Creating empty procedure [dbo].[Book_Delete]'
    EXEC('CREATE PROCEDURE [dbo].[Book_Delete] AS SET NOCOUNT ON;')
END
GO
/****** Object:  StoredProcedure [dbo].[User_Delete]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_Delete]
(
	@id UNIQUEIDENTIFIER
)
AS
BEGIN

	delete
	from Users
	where Id = @id

END
GO
/****** Object:  StoredProcedure [dbo].[User_DeleteByUsername]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_DeleteByUsername]
(
	@username nvarchar(20)
)
AS
BEGIN

	delete
	from Users
	where Username = @username

END
GO
/****** Object:  StoredProcedure [dbo].[User_Get]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_Get]
(
	@id UNIQUEIDENTIFIER
)
AS
BEGIN

	select
		u.Id
		,u.Username
		,u.PasswordHash
		,u.Roles
		,u.Email
		,u.Salt
		,u.LanguageCode
	from Users u
	where u.Id = @id

END
GO
/****** Object:  StoredProcedure [dbo].[User_GetAll]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_GetAll]
(
	@username nvarchar(20)
)
AS
BEGIN

	select
		u.Id
		,u.Username
		,u.PasswordHash
		,u.Roles
		,u.Email
		,u.Salt
		,u.LanguageCode
	from Users u

END
GO
/****** Object:  StoredProcedure [dbo].[User_GetByUsername]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_GetByUsername]
(
	@username nvarchar(20)
)
AS
BEGIN

	select
		u.Id
		,u.Username
		,u.PasswordHash
		,u.Roles
		,u.Email
		,u.Salt
		,u.LanguageCode
	from Users u
	where u.Username = @username

END
GO
/****** Object:  StoredProcedure [dbo].[User_Insert]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_Insert]
(
	@userId UNIQUEIDENTIFIER
	,@username nvarchar(20)
	,@passwordHash nvarchar(1000)
	,@roles int
	,@email nvarchar(50)
	,@salt nvarchar(50)
	,@languageCode int
)
AS
BEGIN

	insert into Users
	(
		Id
		,Salt
		,Roles
		,Email
		,Username
		,PasswordHash
		,LanguageCode
	)
	values
	(
		@userId
		,@salt
		,@roles
		,@email
		,@username
		,@passwordHash
		,@languageCode
	)

END
GO
/****** Object:  StoredProcedure [dbo].[User_Update]    Script Date: 18-Oct-17 23:23:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[User_Update]
(
	@userId UNIQUEIDENTIFIER
	,@username nvarchar(20)
	,@roles int
	,@email nvarchar(50)
	,@languageCode int
)
AS
BEGIN
	UPDATE [Users]
	SET Username = @username
		,Roles = @roles
		,Email = @email
		,LanguageCode = @languageCode
	WHERE Id = @userId
END
GO
USE [master]
GO
ALTER DATABASE [Snowinmars.DataBase] SET  READ_WRITE
GO
