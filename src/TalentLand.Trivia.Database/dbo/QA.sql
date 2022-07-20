﻿CREATE TABLE [dbo].[QA]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [QuestionId] UNIQUEIDENTIFIER NOT NULL, 
    [AnswerId] UNIQUEIDENTIFIER NULL,
    [CreationDate] DATETIME2 NOT NULL, 
    [ModificationDate] DATETIME2 NULL,
    CONSTRAINT [FK_User_QA] FOREIGN KEY (UserId) REFERENCES [User]([Id]),
    CONSTRAINT [FK_Question_QA] FOREIGN KEY (QuestionId) REFERENCES [Question]([Id]),
    CONSTRAINT [FK_Answer_QA] FOREIGN KEY (AnswerId) REFERENCES [Answer]([Id]),
)
