IF DB_ID('MillionaireGameWebDB') IS NOT NULL
	PRINT 'Error. Database already exist';
GO
CREATE DATABASE MillionaireGameWebDB
GO
USE MillionaireGameWebDB
GO
CREATE TABLE [dbo].[ExceptionLogs] (
    [Id] [int] NOT NULL IDENTITY,
    [Date] [datetime] NOT NULL,
    [Thread] [nvarchar](255) NOT NULL,
    [Message] [nvarchar](4000) NOT NULL,
    CONSTRAINT [PK_dbo.ExceptionLogs] PRIMARY KEY ([Id])
)
GO
CREATE TABLE [dbo].[UserAnswerLogs] (
    [Id] [int] NOT NULL IDENTITY,
    [QuestionNumber] [int] NOT NULL,
    [FirstAnswerCount] [int] NOT NULL,
    [SecondAnswerCount] [int] NOT NULL,
    [ThirdAnswerCount] [int] NOT NULL,
    [FourthAnswerCount] [int] NOT NULL,
    CONSTRAINT [PK_dbo.UserAnswerLogs] PRIMARY KEY ([Id])
)