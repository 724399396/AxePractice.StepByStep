-->> NOTE: THIS SCRIPT MUST BE RUN IN SQLCMD MODE INSIDE SQL SERVER MANAGEMENT STUDIO. <<--
:on error exit

SET NOCOUNT OFF;
GO

PRINT CONVERT(varchar(1000), @@VERSION);
GO

PRINT '';
PRINT 'Started - ' + CONVERT(varchar, GETDATE(), 121);
GO

USE [master];
GO
-- ****************************************
-- Drop Database
-- ****************************************
PRINT '';
PRINT '*** Dropping Database';
GO

IF EXISTS (SELECT [name] FROM [master].[sys].[databases] WHERE [name] = N'AwesomeDb')
    DROP DATABASE [AwesomeDb];

-- If the database has any other open connections close the network connection.
IF @@ERROR = 3702 
    RAISERROR('[AwesomeDb] database cannot be dropped because there are still other open connections', 127, 127) WITH NOWAIT, LOG;
GO


-- ****************************************
-- Create Database
-- ****************************************
PRINT '';
PRINT '*** Creating Database';
GO

CREATE DATABASE [AwesomeDb]
GO

PRINT '';
PRINT '*** Checking for AwesomeDb Database';

/* CHECK FOR DATABASE IF IT DOESN'T EXISTS, DO NOT RUN THE REST OF THE SCRIPT */
IF NOT EXISTS (SELECT TOP 1 1 FROM sys.databases WHERE name = N'AwesomeDb')
BEGIN
PRINT 'AwesomeDb Database does not exist.  Make sure that the script is being run in SQLCMD mode and that the variables have been correctly set.';
SET NOEXEC ON;
END
GO

ALTER DATABASE [AwesomeDb] 
SET RECOVERY SIMPLE, 
    ANSI_NULLS ON, 
    ANSI_PADDING ON, 
    ANSI_WARNINGS ON, 
    ARITHABORT ON, 
    CONCAT_NULL_YIELDS_NULL ON, 
    QUOTED_IDENTIFIER ON, 
    NUMERIC_ROUNDABORT OFF, 
    PAGE_VERIFY CHECKSUM, 
    ALLOW_SNAPSHOT_ISOLATION OFF;
GO

USE [AwesomeDb];
GO

-- ******************************************************
-- Create tables
-- ******************************************************

PRINT '';
PRINT '*** Creating Tables';
GO

CREATE TABLE [dbo].[person](
	[PersonID] [UNIQUEIDENTIFIER] NOT NULL,
	[NAME] NVARCHAR(64) NOT NULL,
	[IsForQuery] [BIT] NOT NULL
) ON [PRIMARY];
GO

INSERT INTO [dbo].[person] VALUES 
    ('e395b6fc-14ff-47da-819e-526d6c9896d3', 'A', 1),
    ('dfbfd41d-e7c6-4709-9bf0-bd490d227b8f', 'B', 1),
    ('dfbfd41d-e7c6-4709-9bf0-bd490d227b8e', 'C', 1);
GO

CREATE TABLE [dbo].[competency](
	[CompetencyId] [UNIQUEIDENTIFIER] NOT NULL,
	[TITLE] varchar(50) NOT NULL,
	[IsForQuery] [BIT] NOT NULL
) ON [PRIMARY];
GO

INSERT INTO [dbo].[competency] VALUES 
    ('e395b6fc-14ff-47da-819e-526d6c9896d0', 'csharp', 1),
    ('dfbfd41d-e7c6-4709-9bf0-bd490d227b80', 'java', 1);
GO

CREATE TABLE [dbo].[person_x_competency](
    [ParentID] [UNIQUEIDENTIFIER] NOT NULL,
    [CompetencyId] [UNIQUEIDENTIFIER] NOT NULL,
    [IsForQuery] [BIT] NOT NULL DEFAULT 0
) ON [PRIMARY];
GO

INSERT INTO [dbo].[person_x_competency] VALUES 
    ('e395b6fc-14ff-47da-819e-526d6c9896d3', 'e395b6fc-14ff-47da-819e-526d6c9896d0', 1),
    ('e395b6fc-14ff-47da-819e-526d6c9896d3', 'dfbfd41d-e7c6-4709-9bf0-bd490d227b80', 1),
    ('dfbfd41d-e7c6-4709-9bf0-bd490d227b8f', 'e395b6fc-14ff-47da-819e-526d6c9896d0', 1),
    ('dfbfd41d-e7c6-4709-9bf0-bd490d227b8e', 'dfbfd41d-e7c6-4709-9bf0-bd490d227b80', 1);
GO

-- ****************************************
-- Shrink Database
-- ****************************************
PRINT '';
PRINT '*** Shrinking Database';
GO

DBCC SHRINKDATABASE ([AwesomeDb]);
GO


USE [master];
GO

PRINT 'Finished - ' + CONVERT(varchar, GETDATE(), 121);
GO


SET NOEXEC OFF