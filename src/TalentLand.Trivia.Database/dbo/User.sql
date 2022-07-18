CREATE TABLE [dbo].[User]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(300) NOT NULL, 
    [Email] NVARCHAR(150) NOT NULL,
    [University] NVARCHAR(200) NOT NULL,
    [Company] NVARCHAR(200) NOT NULL,
    [CreationDate] DATETIME2 NOT NULL, 
    [ModificationDate] DATETIME2 NULL     
)
