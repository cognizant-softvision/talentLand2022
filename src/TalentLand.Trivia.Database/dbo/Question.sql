CREATE TABLE [dbo].[Question]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [QuestionText] NVARCHAR(500) NOT NULL, 
    [Order] INT NOT NULL,
    [CreationDate] DATETIME2 NOT NULL, 
    [ModificationDate] DATETIME2 NULL     
)
