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
    ('fed54697-5dba-4164-aefc-6a21a11a94ca', '¿ En cuántos países está posicionado Cognizant Softvision ?', 1, getdate(), null),
    ('60abd26b-2b96-463e-a4a1-1ae39449e22d', '¿ La escalabilidad es una ventaja del modelo On-premise ?', 2, getdate(), null),
    ('2576b666-5629-4c37-8082-87609e493f8a', '¿ FaaS es un tipo de se servicio del cloud computing ?', 3, getdate(), null),
    ('c432f29b-b5d4-455b-b9cc-616569ef64e1', 'Es una desventaja de serverless computing:', 4, getdate(), null),
    ('154b7e49-2694-44ee-865d-7ad1631f966d', '¿ Cuál es el patrón de aplicación usado en el ejemplo de Trivia Project ?', 5, getdate(), null)

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
    ('9630b28a-bba7-4748-b139-0b85d551c83b', 'fed54697-5dba-4164-aefc-6a21a11a94ca', '10', 1, 0, getdate(), null),
    ('e842b417-edf7-4b0f-b2c8-44017def5858', 'fed54697-5dba-4164-aefc-6a21a11a94ca', '12', 2, 1, getdate(), null),
    ('14c0110e-a375-4f46-be83-5a8a698d3600', 'fed54697-5dba-4164-aefc-6a21a11a94ca', '14', 3, 0, getdate(), null),    

    /* question 2 */
    ('2cebb2f5-282f-4e69-8ce8-26cf1c3cb449', '60abd26b-2b96-463e-a4a1-1ae39449e22d', 'Verdadero', 1, 0, getdate(), null),
    ('4873e4c8-cf96-41ca-bc94-92e2cbca7439', '60abd26b-2b96-463e-a4a1-1ae39449e22d', 'Falso', 2, 1, getdate(), null),

    /* question 3 */
    ('e4adb87d-b211-4337-b576-16d91e8f3010', '2576b666-5629-4c37-8082-87609e493f8a', 'Verdadero', 1, 1, getdate(), null),
    ('60c3638c-021a-4459-a98c-6498d1128e95', '2576b666-5629-4c37-8082-87609e493f8a', 'Falso', 3, 0, getdate(), null),

    /* question 4 */
    ('1ee712f2-9296-4872-93eb-2a482c11337b', 'c432f29b-b5d4-455b-b9cc-616569ef64e1', 'Disponible bajo demanda', 1, 0, getdate(), null),
    ('ba1eaf68-0b07-4eee-a2b6-5bb3209884c8', 'c432f29b-b5d4-455b-b9cc-616569ef64e1', 'Lock-in effect', 2, 1, getdate(), null),
    ('3280e02e-509a-4d70-8d1a-7bc255defb85', 'c432f29b-b5d4-455b-b9cc-616569ef64e1', 'Reusable', 3, 0, getdate(), null),        

    /* question 5 */
    ('d80af9a4-b535-40de-b16e-858a79bc77b6', '154b7e49-2694-44ee-865d-7ad1631f966d', 'Encadenamiento de funciones', 1, 0, getdate(), null),
    ('2d54c215-16b9-467f-872c-928d7d193dec', '154b7e49-2694-44ee-865d-7ad1631f966d', 'Supervision', 1, 0, getdate(), null),
    ('e11a2a8e-02b7-4f45-9df0-ae6a4b5addba', '154b7e49-2694-44ee-865d-7ad1631f966d', 'Interaccion humana', 1, 1, getdate(), null)
    

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
