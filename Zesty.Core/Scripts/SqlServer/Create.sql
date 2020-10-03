USE [master]
GO
/****** Object:  Database [Zesty]    Script Date: 10/3/2020 2:00:36 AM ******/
CREATE DATABASE [Zesty]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Zesty', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Zesty.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Zesty_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Zesty_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Zesty] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Zesty].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Zesty] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Zesty] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Zesty] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Zesty] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Zesty] SET ARITHABORT OFF 
GO
ALTER DATABASE [Zesty] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Zesty] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Zesty] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Zesty] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Zesty] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Zesty] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Zesty] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Zesty] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Zesty] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Zesty] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Zesty] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Zesty] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Zesty] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Zesty] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Zesty] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Zesty] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Zesty] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Zesty] SET RECOVERY FULL 
GO
ALTER DATABASE [Zesty] SET  MULTI_USER 
GO
ALTER DATABASE [Zesty] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Zesty] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Zesty] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Zesty] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Zesty] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Zesty] SET QUERY_STORE = OFF
GO
USE [Zesty]
GO
/****** Object:  User [zestyUser]    Script Date: 10/3/2020 2:00:36 AM ******/
CREATE USER [zestyUser] FOR LOGIN [zestyUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [zestyUser]
GO
/****** Object:  Table [dbo].[Authorization]    Script Date: 10/3/2020 2:00:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Authorization](
	[UserId] [uniqueidentifier] NOT NULL,
	[DomainId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Authorization] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[DomainId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Domain]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Domain](
	[Id] [uniqueidentifier] NOT NULL,
	[ParentDomainId] [uniqueidentifier] NULL,
	[Name] [varchar](300) NOT NULL,
 CONSTRAINT [PK_Domain] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[History]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History](
	[Id] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Actor] [varchar](100) NOT NULL,
	[Resource] [varchar](300) NOT NULL,
	[Text] [varchar](max) NOT NULL,
 CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[Direction] [char](1) NOT NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resource]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resource](
	[Id] [uniqueidentifier] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[IsPublic] [bit] NOT NULL,
	[RequireToken] [bit] NOT NULL,
	[Url] [varchar](400) NOT NULL,
	[Label] [varchar](50) NULL,
	[Image] [varchar](300) NULL,
	[Title] [varchar](100) NULL,
 CONSTRAINT [PK_Resource] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceRole]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceRole](
	[ResourceId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ResourceRole] PRIMARY KEY CLUSTERED 
(
	[ResourceId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResourceType]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResourceType](
	[ResourceId] [uniqueidentifier] NOT NULL,
	[Type] [varchar](500) NOT NULL,
 CONSTRAINT [PK_ResourceType] PRIMARY KEY CLUSTERED 
(
	[ResourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Token]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Token](
	[UserId] [uniqueidentifier] NOT NULL,
	[SessionId] [varchar](50) NOT NULL,
	[Value] [varchar](100) NOT NULL,
	[IsReusable] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Deleted] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Translation]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Translation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Original] [varchar](500) NOT NULL,
	[Translated] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Translation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [varchar](200) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[Firstname] [varchar](50) NOT NULL,
	[Lastname] [varchar](50) NOT NULL,
	[ResetToken] [uniqueidentifier] NULL,
	[Deleted] [datetime] NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPassword]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPassword](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Deleted] [datetime] NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_UserPassword] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[Created] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProperties]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProperties](
	[UserId] [uniqueidentifier] NOT NULL,
	[Key] [varchar](100) NOT NULL,
	[Value] [varchar](500) NOT NULL,
 CONSTRAINT [PK_UserProperties] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Domain_Name]    Script Date: 10/3/2020 2:00:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_Domain_Name] ON [dbo].[Domain]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_History]    Script Date: 10/3/2020 2:00:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_History] ON [dbo].[History]
(
	[Actor] ASC,
	[Created] ASC,
	[Resource] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Resource_Url]    Script Date: 10/3/2020 2:00:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_Resource_Url] ON [dbo].[Resource]
(
	[Url] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Token]    Script Date: 10/3/2020 2:00:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_Token] ON [dbo].[Token]
(
	[UserId] ASC,
	[SessionId] ASC,
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_User_ResetToken]    Script Date: 10/3/2020 2:00:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_User_ResetToken] ON [dbo].[User]
(
	[ResetToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_Username]    Script Date: 10/3/2020 2:00:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_User_Username] ON [dbo].[User]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Authorization]  WITH CHECK ADD  CONSTRAINT [FK_Authorization_Domain] FOREIGN KEY([DomainId])
REFERENCES [dbo].[Domain] ([Id])
GO
ALTER TABLE [dbo].[Authorization] CHECK CONSTRAINT [FK_Authorization_Domain]
GO
ALTER TABLE [dbo].[Authorization]  WITH CHECK ADD  CONSTRAINT [FK_Authorization_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Authorization] CHECK CONSTRAINT [FK_Authorization_Role]
GO
ALTER TABLE [dbo].[Authorization]  WITH CHECK ADD  CONSTRAINT [FK_Authorization_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Authorization] CHECK CONSTRAINT [FK_Authorization_User]
GO
ALTER TABLE [dbo].[ResourceRole]  WITH CHECK ADD  CONSTRAINT [FK_ResourceRole_Resource] FOREIGN KEY([ResourceId])
REFERENCES [dbo].[Resource] ([Id])
GO
ALTER TABLE [dbo].[ResourceRole] CHECK CONSTRAINT [FK_ResourceRole_Resource]
GO
ALTER TABLE [dbo].[ResourceRole]  WITH CHECK ADD  CONSTRAINT [FK_ResourceRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[ResourceRole] CHECK CONSTRAINT [FK_ResourceRole_Role]
GO
ALTER TABLE [dbo].[Translation]  WITH CHECK ADD  CONSTRAINT [FK_Translation_Language] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[Translation] CHECK CONSTRAINT [FK_Translation_Language]
GO
ALTER TABLE [dbo].[UserPassword]  WITH CHECK ADD  CONSTRAINT [FK_UserPassword_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserPassword] CHECK CONSTRAINT [FK_UserPassword_User]
GO
ALTER TABLE [dbo].[UserProperties]  WITH CHECK ADD  CONSTRAINT [FK_UserProperties_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[UserProperties] CHECK CONSTRAINT [FK_UserProperties_User]
GO
/****** Object:  StoredProcedure [dbo].[CanAccess]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CanAccess]

	@path varchar(400),
	@userid uniqueidentifier

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select * from [Authorization] a
join [user] u on u.Id = a.UserId
join [role] r on r.Id = a.RoleId
join [ResourceRole] rr on rr.RoleId = r.Id
join [Resource] rs on rs.Id = rr.ResourceId
join [Domain] d on a.DomainId = d.Id or a.DomainId = d.ParentDomainId
where 
rs.Url = @path
and u.Id = @userid

END
GO
/****** Object:  StoredProcedure [dbo].[ChangePassword]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ChangePassword]

	@userid uniqueidentifier,
	@previousPassword varchar(100),
	@newPassword varchar(100)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
	IF EXISTS (SELECT * FROM [UserPassword] WHERE [UserId] = @userid AND [Password] = @previousPassword AND [Deleted] IS NULL) 
	BEGIN

		update [UserPassword] set Deleted = GETDATE() where [UserId] = @userid AND [Deleted] is null AND [Password] = @previousPassword;

		insert into [UserPassword] ([Id], [UserId], [Password], [Created]) VALUES (newid(), @userid, @newPassword, GETDATE());

		SELECT 'OK' as Status
	END
	ELSE
	BEGIN
		SELECT 'KO' as Status
	END






END
GO
/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[CreateUser]

	@username varchar(200),
	@email varchar(200),
	@firstname varchar(50),
	@lastname varchar(50),
	@password varchar(100)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


INSERT INTO [dbo].[User] ([Id],[Username],[Email],[Firstname],[Lastname],[ResetToken],[Deleted],[Created]) VALUES (newid(),@username,@email,@firstname,@lastname,null,null,getdate());
INSERT INTO [dbo].[UserPassword] ([Id],[UserId],[Password],[Deleted],[Created]) VALUES (newid(),(select id from [user] where [username] = @username),@password,null,getdate());



END
GO
/****** Object:  StoredProcedure [dbo].[DeleteExpiredTokens]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteExpiredTokens]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


DELETE FROM [dbo].[Token] WHERE DATEDIFF(minute, Created, GETDATE()) > 10;

END
GO
/****** Object:  StoredProcedure [dbo].[GetDomains]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDomains]

	@username varchar(100)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
select d.[Id], d.[ParentDomainId], d.[Name] from 
[Authorization] a
join [Domain] d on d.Id = a.DomainId 
join [User] u on u.Id = a.UserId
where 
u.Username = @username


END


GO
/****** Object:  StoredProcedure [dbo].[GetDomainsList]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDomainsList]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT [Id]
      ,[ParentDomainId]
      ,[Name]
  FROM [dbo].[Domain]


END
GO
/****** Object:  StoredProcedure [dbo].[GetLanguages]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetLanguages]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


SELECT [Id]
      ,[Name]
      ,[Description]
      ,[Direction]
  FROM [dbo].[Language]
ORDER BY [Id];

END
GO
/****** Object:  StoredProcedure [dbo].[GetProperties]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetProperties]

	@userid uniqueidentifier

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT [Key],[Value]
FROM [Zesty].[dbo].[UserProperties]
WHERE
[UserId] = @userid



END
GO
/****** Object:  StoredProcedure [dbo].[GetResources]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetResources]

	@username varchar(200),
	@domainName varchar(300)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


select 

	rs.Id,
	rs.ParentId,
	rs.IsPublic,
	rs.RequireToken,
	rs.[Url],
	rs.[Label],
	rs.[Title],
	rs.[Image]

from 
[user] u
join [Authorization] a on a.UserId = u.Id
join [Domain] d on d.Id = a.DomainId
join [Role] r on r.Id = a.RoleId
right join [ResourceRole] rr on rr.RoleId = r.Id
right join [Resource] rs on rs.Id = rr.ResourceId

where 
(u.Username = @username
and d.[name] = @domainName)
OR rs.IsPublic = 1

END
GO
/****** Object:  StoredProcedure [dbo].[GetResourceType]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetResourceType]
	
	@resourceName varchar(300)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
SELECT 
	rt.[Type]
FROM 
	[dbo].[Resource] r
	join [dbo].[ResourceType] rt on rt.ResourceId = r.Id
WHERE
	r.Url = @resourceName

  

END
GO
/****** Object:  StoredProcedure [dbo].[GetRoles]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetRoles]

	@username varchar(100),
	@domain varchar(100)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


select r.[Name] from 
[Authorization] a
join [Domain] d on d.Id = a.DomainId
join [Role] r on r.Id = a.RoleId
join [User] u on u.Id = a.UserId
where 
u.Username = @username
and d.[Name] = @domain


END
GO
/****** Object:  StoredProcedure [dbo].[GetTranslations]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTranslations]

	@language varchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;



SELECT 
	t.[Id]
	,t.[LanguageId]
	,t.[Original]
	,t.[Translated]
FROM 
	[dbo].[Translation] t
	join [dbo].[Language] l on l.Id = t.LanguageId
WHERE 
l.[Name] = @language


END
GO
/****** Object:  StoredProcedure [dbo].[GetUserByResetToken]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetUserByResetToken]

	@token uniqueidentifier

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
select 
	u.Id,
	u.Username,
	u.Email,
	u.Firstname,
	u.Lastname
from [user] u
where 
u.ResetToken = @token
and u.ResetToken is not null
and u.Deleted is null;



END
GO
/****** Object:  StoredProcedure [dbo].[IsPublicResource]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[IsPublicResource]

	@path varchar(300)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	IF EXISTS (select * from [Resource] where [url] = @path and IsPublic = 1) 
	BEGIN
	   SELECT 'Y' as IsPublic
	END
	ELSE
	BEGIN
		SELECT 'N' as IsPublic
	END


	

END
GO
/****** Object:  StoredProcedure [dbo].[IsValid]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[IsValid]

	@userid uniqueidentifier,
	@sessionid varchar(100),
	@token varchar(100)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;



	IF EXISTS (SELECT [UserId],[SessionId],[Value],[Created] FROM [dbo].[Token] WHERE [UserId] = @userid AND [SessionId] = @sessionid AND [Value] = @token AND [Deleted] IS NULL AND DATEDIFF(minute, Created, GETDATE()) <= 10) 
	BEGIN
		UPDATE [Token] SET [Deleted] = 1 WHERE [Value] = @token AND [IsReusable] = 0;

		SELECT 'Y' as IsValid
	END
	ELSE
	BEGIN
		SELECT 'N' as IsValid
	END




END
GO
/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Login]

	@username varchar(300),
	@password varchar(100)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
select 
	u.Id,
	u.Username,
	u.Email,
	u.Firstname,
	u.Lastname,
	p.Created
from [user] u
join [UserPassword] p on u.Id = p.UserId
join [Authorization] a on a.UserId = u.Id
where 
[username] = @username
and p.Password = @password
and p.Deleted is null
and u.Deleted is null;



END
GO
/****** Object:  StoredProcedure [dbo].[RequireToken]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RequireToken]

	@path varchar(400)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	IF EXISTS (select * from [Resource] where [url] = @path and RequireToken = 1) 
	BEGIN
	   SELECT 'Y' as IsRequired
	END
	ELSE
	BEGIN
		SELECT 'N' as IsRequired
	END



END
GO
/****** Object:  StoredProcedure [dbo].[ResetPassword]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ResetPassword]

	@token uniqueidentifier,
	@password varchar(100)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
	IF EXISTS (SELECT * FROM [User] WHERE [ResetToken] IS NOT NULL AND [ResetToken] = @token AND [Deleted] IS NULL) 
	BEGIN

		update [UserPassword] set Deleted = GETDATE() where [UserId] = (SELECT [Id] FROM [User] WHERE [ResetToken] = @token) AND [Deleted] is null;

		insert into [UserPassword] ([Id], [UserId], [Password], [Created]) VALUES (newid(), (SELECT [Id] FROM [User] WHERE [ResetToken] = @token), @password, GETDATE());

		update [User] set [ResetToken] = null where [ResetToken] = @token;
 
		SELECT 'OK' as Status
	END
	ELSE
	BEGIN
		SELECT 'KO' as Status
	END






END

GO
/****** Object:  StoredProcedure [dbo].[SaveHistory]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SaveHistory]


			@userid as uniqueidentifier,
			@actor as varchar(100),
			@resource as varchar(200),
			@text as varchar(MAX)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


INSERT INTO [dbo].[History]
           ([Id]
           ,[Created]
           ,[UserId]
           ,[Actor]
           ,[Resource]
           ,[Text])
     VALUES
           (newid()
           ,getdate()
           ,@userid
           ,@actor
           ,@resource
           ,@text)



END
GO
/****** Object:  StoredProcedure [dbo].[SaveToken]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SaveToken]

	@userid uniqueidentifier,
	@sessionid varchar(100),
	@value varchar(100),
	@reusable bit

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


INSERT INTO [dbo].[Token]
           ([UserId]
           ,[SessionId]
           ,[Value]
		   ,[IsReusable]
           ,[Created])
     VALUES
           (@userid
           ,@sessionid
           ,@value
		   ,@reusable
           ,getdate());


END
GO
/****** Object:  StoredProcedure [dbo].[SetResetToken]    Script Date: 10/3/2020 2:00:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SetResetToken]

	@email varchar(300)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @token as uniqueidentifier;

	set @token = newid();


	update [user] set ResetToken = @token WHERE [Deleted] is null and [Email] = @email;

	select @token;
END
GO
USE [master]
GO
ALTER DATABASE [Zesty] SET  READ_WRITE 
GO
