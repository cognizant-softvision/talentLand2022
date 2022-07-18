CREATE TABLE [dbo].[Answer]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [QuestionId] UNIQUEIDENTIFIER NOT NULL,
    [AnswerText] NVARCHAR(500) NOT NULL, 
    [Order] INT NOT NULL,
    [IsCorrect] BIT NOT NULL,
    [CreationDate] DATETIME2 NOT NULL, 
    [ModificationDate] DATETIME2 NULL,
    CONSTRAINT [FK_Answer_Question] FOREIGN KEY (QuestionId) REFERENCES [Question]([Id]),
)
