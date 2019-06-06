Create Database ETLManager;

Go

USE [ETLManager]
GO

/****** Object: Table [dbo].[ETLManager] Script Date: 6/6/2019 11:20:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ETLManager] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [FileLocation] NVARCHAR (MAX) NOT NULL,
    [ParseType]    NVARCHAR (50)  NOT NULL,
    [DestSQLDB]    NVARCHAR (50)  NOT NULL,
    [DestSQLTable] NVARCHAR (50)  NOT NULL,
    [ParseString]  NVARCHAR (MAX) NOT NULL,
    [DestSQLProc]  NVARCHAR (50)  NOT NULL
);

Go

CREATE PROCEDURE [dbo].[LoadConfig]
	@ParseID int = 0

AS
	SELECT	[Name] 
			,FileLocation 
			,ParseType
			,DestSQLDB
			,DestSQLTable
			,ParseString 
			,DestSQLProc
	FROM ETLManager
	WHERE ID = @ParseID;
RETURN 0

Go



