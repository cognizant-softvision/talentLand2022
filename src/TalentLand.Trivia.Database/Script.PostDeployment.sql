/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/* ------------------------------------------------------------------------------------------------------------------------------------------------- */
/* User - Admin */
DECLARE @User_Table TABLE (
    [Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[University] [nvarchar](200) NOT NULL,
	[Company] [nvarchar](200) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[ModificationDate] [datetime2](7) NULL
)

INSERT @User_Table (Id, [Name], [FirstLastName], [SecondLastName], [Email], [University], [Company], CreationDate, ModificationDate)
VALUES
    ('f7ef4956-23b2-4ae6-861e-b8d980f10bcd', 'Admin 1 2', 'admin@12@cognizant.com', 'ITESO', 'Cognizant Softvision', getdate(), null)

BEGIN TRANSACTION
    BEGIN TRY

        PRINT 'Inserting Users'
        MERGE [User] AS TARGET
        USING @User_Table as SOURCE
        ON (TARGET.ID = SOURCE.ID)
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (Id, [Name], [Email], [University], [Company], CreationDate, ModificationDate)
            VALUES (SOURCE.Id, SOURCE.[Name], SOURCE.[FirstLastName], SOURCE.[SecondLastName], SOURCE.[Email], SOURCE.[University], SOURCE.[Company], 
                SOURCE.CreationDate, SOURCE.ModificationDate)
        WHEN MATCHED THEN
            UPDATE SET 
                TARGET.[Name] = SOURCE.[Name],
                TARGET.[Email] = SOURCE.[Email],
                TARGET.[University] = SOURCE.[University],
                TARGET.[Company] = SOURCE.[Company],
                TARGET.CreationDate = SOURCE.CreationDate,
                TARGET.ModificationDate = SOURCE.ModificationDate;

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH 
        SELECT ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;
        Print ERROR_MESSAGE()
        ROLLBACK TRANSACTION
    END CATCH

/* ------------------------------------------------------------------------------------------------------------------------------------------------- */
/* Questions */
DECLARE @Question_Table TABLE (
    [Id] [uniqueidentifier] NOT NULL,
	[QuestionText] [nvarchar](500) NOT NULL,
	[Order] [int] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[ModificationDate] [datetime2](7) NULL
)

INSERT @Question_Table (Id, QuestionText, [Order], CreationDate, ModificationDate)
VALUES
    ('fed54697-5dba-4164-aefc-6a21a11a94ca', 'Que es Cloud Computing?', 1, getdate(), null),
    ('60abd26b-2b96-463e-a4a1-1ae39449e22d', 'Que es Serverless?', 2, getdate(), null),
    ('2576b666-5629-4c37-8082-87609e493f8a', 'Ventajas', 3, getdate(), null),
    ('c432f29b-b5d4-455b-b9cc-616569ef64e1', 'Desventajas', 4, getdate(), null),
    ('154b7e49-2694-44ee-865d-7ad1631f966d', 'Durable Function', 5, getdate(), null)

BEGIN TRANSACTION
    BEGIN TRY

        PRINT 'Inserting Questions'
        MERGE [Question] AS TARGET
        USING @Question_Table as SOURCE
        ON (TARGET.ID = SOURCE.ID)
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (Id, QuestionText, [Order], CreationDate, ModificationDate)
            VALUES (SOURCE.Id, SOURCE.QuestionText, SOURCE.[Order], SOURCE.CreationDate, SOURCE.ModificationDate)
        WHEN MATCHED THEN
            UPDATE SET 
                TARGET.QuestionText = SOURCE.QuestionText,
                TARGET.[Order] = SOURCE.[Order],
                TARGET.CreationDate = SOURCE.CreationDate,
                TARGET.ModificationDate = SOURCE.ModificationDate;

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH 
        SELECT ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;
        Print ERROR_MESSAGE()
        ROLLBACK TRANSACTION
    END CATCH

/* ------------------------------------------------------------------------------------------------------------------------------------------------- */
/* Answers */
DECLARE @Answer_Table TABLE (
    [Id] [uniqueidentifier] NOT NULL,
	[QuestionId] [uniqueidentifier] NOT NULL,
	[AnswerText] [nvarchar](500) NOT NULL,
	[Order] [int] NOT NULL,
	[IsCorrect] [bit] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[ModificationDate] [datetime2](7) NULL
)


INSERT @Answer_Table (Id, QuestionId, AnswerText, [Order], IsCorrect, CreationDate, ModificationDate)
VALUES
    /* question 1 */
    ('e842b417-edf7-4b0f-b2c8-44017def5858', 'fed54697-5dba-4164-aefc-6a21a11a94ca', 'Incorrecta', 1, 0, getdate(), null),
    ('14c0110e-a375-4f46-be83-5a8a698d3600', 'fed54697-5dba-4164-aefc-6a21a11a94ca', 'Incorrecta', 1, 0, getdate(), null),
    ('9630b28a-bba7-4748-b139-0b85d551c83b', 'fed54697-5dba-4164-aefc-6a21a11a94ca', 'Correcta', 1, 1, getdate(), null),

    /* question 2 */
    ('1d90e085-665e-44ec-8468-b5a43d9ffcb9', '60abd26b-2b96-463e-a4a1-1ae39449e22d', 'Correcta', 1, 1, getdate(), null),
    ('2cebb2f5-282f-4e69-8ce8-26cf1c3cb449', '60abd26b-2b96-463e-a4a1-1ae39449e22d', 'Incorrecta', 1, 0, getdate(), null),
    ('4873e4c8-cf96-41ca-bc94-92e2cbca7439', '60abd26b-2b96-463e-a4a1-1ae39449e22d', 'Incorrecta', 1, 0, getdate(), null),

    /* question 3 */
    ('00f6af3c-b077-4733-9c62-a73a4f01dc83', '2576b666-5629-4c37-8082-87609e493f8a', 'Incorrecta', 1, 0, getdate(), null),
    ('e4adb87d-b211-4337-b576-16d91e8f3010', '2576b666-5629-4c37-8082-87609e493f8a', 'Incorrecta', 1, 0, getdate(), null),
    ('60c3638c-021a-4459-a98c-6498d1128e95', '2576b666-5629-4c37-8082-87609e493f8a', 'Correcta', 1, 1, getdate(), null),

    /* question 4 */
    ('3280e02e-509a-4d70-8d1a-7bc255defb85', 'c432f29b-b5d4-455b-b9cc-616569ef64e1', 'Incorrecta', 1, 0, getdate(), null),
    ('1ee712f2-9296-4872-93eb-2a482c11337b', 'c432f29b-b5d4-455b-b9cc-616569ef64e1', 'Correcta', 1, 1, getdate(), null),
    ('ba1eaf68-0b07-4eee-a2b6-5bb3209884c8', 'c432f29b-b5d4-455b-b9cc-616569ef64e1', 'Incorrecta', 1, 0, getdate(), null),

    /* question 5 */
    ('e11a2a8e-02b7-4f45-9df0-ae6a4b5addba', '154b7e49-2694-44ee-865d-7ad1631f966d', 'Incorrecta', 1, 0, getdate(), null),
    ('2d54c215-16b9-467f-872c-928d7d193dec', '154b7e49-2694-44ee-865d-7ad1631f966d', 'Incorrecta', 1, 0, getdate(), null),
    ('d80af9a4-b535-40de-b16e-858a79bc77b6', '154b7e49-2694-44ee-865d-7ad1631f966d', 'Correcta', 1, 1, getdate(), null)
    

BEGIN TRANSACTION
    BEGIN TRY

        PRINT 'Inserting Answers'
        MERGE [Answer] AS TARGET
        USING @Answer_Table as SOURCE
        ON (TARGET.ID = SOURCE.ID)
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (Id, QuestionId, AnswerText, [Order], IsCorrect, CreationDate, ModificationDate)
            VALUES (SOURCE.Id, SOURCE.QuestionId, SOURCE.AnswerText, SOURCE.[Order], SOURCE.IsCorrect, SOURCE.CreationDate, SOURCE.ModificationDate)
        WHEN MATCHED THEN
            UPDATE SET 
                TARGET.AnswerText = SOURCE.AnswerText,
                TARGET.[Order] = SOURCE.[Order],
                TARGET.IsCorrect = SOURCE.IsCorrect,
                TARGET.CreationDate = SOURCE.CreationDate,
                TARGET.ModificationDate = SOURCE.ModificationDate;

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH 
        SELECT ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;
        Print ERROR_MESSAGE()
        ROLLBACK TRANSACTION
    END CATCH

/* ------------------------------------------------------------------------------------------------------------------------------------------------- */
/* QA - admin users */
DECLARE @QA_Table TABLE (
    [Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[QuestionId] [uniqueidentifier] NOT NULL,
	[AnswerId] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[ModificationDate] [datetime2](7) NULL
)

INSERT @QA_Table (Id, UserId, QuestionId, AnswerId, CreationDate, ModificationDate)
VALUES
    /* question & answer - 1 */
    ('141ff235-9ed2-4637-93d5-9b1a3f0f7321', 'f7ef4956-23b2-4ae6-861e-b8d980f10bcd', 'fed54697-5dba-4164-aefc-6a21a11a94ca', '9630b28a-bba7-4748-b139-0b85d551c83b', getdate(), null),

    /* question & answer - 2 */
     ('ca7de0db-7699-4d29-aa17-6dec8bf358b7', 'f7ef4956-23b2-4ae6-861e-b8d980f10bcd', '60abd26b-2b96-463e-a4a1-1ae39449e22d', '1d90e085-665e-44ec-8468-b5a43d9ffcb9', getdate(), null),

     /* question & answer - 3 */
     ('839d7afc-b69e-4c27-acd5-81c91548769b', 'f7ef4956-23b2-4ae6-861e-b8d980f10bcd', '2576b666-5629-4c37-8082-87609e493f8a', '60c3638c-021a-4459-a98c-6498d1128e95', getdate(), null),

     /* question & answer - 4 */
     ('f2c6adf7-5598-4918-a618-70121e6b0824', 'f7ef4956-23b2-4ae6-861e-b8d980f10bcd', 'c432f29b-b5d4-455b-b9cc-616569ef64e1', '1ee712f2-9296-4872-93eb-2a482c11337b', getdate(), null),

     /* question & answer - 5 */
     ('f7070bf1-0b94-4238-aa32-6f01790d20f6', 'f7ef4956-23b2-4ae6-861e-b8d980f10bcd', '154b7e49-2694-44ee-865d-7ad1631f966d', 'd80af9a4-b535-40de-b16e-858a79bc77b6', getdate(), null)

BEGIN TRANSACTION
    BEGIN TRY

        PRINT 'Inserting QA'
        MERGE [QA] AS TARGET
        USING @QA_Table as SOURCE
        ON (TARGET.ID = SOURCE.ID)
        WHEN NOT MATCHED BY TARGET THEN
            INSERT (Id, UserId, QuestionId, AnswerId, CreationDate, ModificationDate)
            VALUES (SOURCE.Id, SOURCE.UserId, SOURCE.QuestionId, SOURCE.AnswerId, SOURCE.CreationDate, SOURCE.ModificationDate)
        WHEN MATCHED THEN
            UPDATE SET 
                TARGET.UserId = SOURCE.UserId,
                TARGET.QuestionId = SOURCE.QuestionId,
                TARGET.AnswerId = SOURCE.AnswerId,
                TARGET.CreationDate = SOURCE.CreationDate,
                TARGET.ModificationDate = SOURCE.ModificationDate;

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH 
        SELECT ERROR_NUMBER() AS ErrorNumber,
            ERROR_MESSAGE() AS ErrorMessage;
        Print ERROR_MESSAGE()
        ROLLBACK TRANSACTION
    END CATCH