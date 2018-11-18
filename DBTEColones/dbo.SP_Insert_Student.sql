USE [XTEColones]
GO

/****** Object: SqlProcedure [dbo].[SP_Insert_Student] Script Date: 11/18/2018 16:48:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
Author: Gustavo Fallas
Insert new register for student
Return 1 if all goes fine or 0 if fails (exist)
*/


CREATE PROCEDURE SP_Insert_Student
	@Identification VARCHAR(50),
	@Description VARCHAR(300),
	@Skills VARCHAR(300) ,
	@PhoneNumber VARCHAR(50),

	@FirstName VARCHAR(50),
    @LastName VARCHAR(100),
	@University VARCHAR(50) ,
    @Headquarter VARCHAR(50) ,
    @Email VARCHAR(100),    
    @Password VARCHAR(12),    
    @PhotoData VARBINARY(MAX)

AS
BEGIN
	BEGIN TRY
		/*DECLARE @var_Photo VARBINARY(MAX);
		SET @var_Photo = (SELECT CAST(@PhotoData AS varbinary(MAX)));*/

		--insert User
		BEGIN TRANSACTION
			INSERT INTO [User](Id, FirstName, LastName, University, Headquarter, Email, Password, Photo) 
			VALUES(@Identification, @FirstName, @LastName, @University, @Headquarter, @Email, ENCRYPTBYPASSPHRASE('password', @Password), @PhotoData);
		COMMIT

		--insert Student
		BEGIN TRANSACTION
			INSERT INTO [Student](Id, Description, Skills, TCS)
			VALUES (@Identification, @Description, @Skills, 0);
		COMMIT

		--insert PhoneNumber_Student
		BEGIN TRANSACTION
			INSERT INTO [PhoneNumber](Id, PhoneNumber) VALUES(@Identification, @PhoneNumber);
		COMMIT

		RETURN 1;

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN
			RETURN 0;
	END CATCH	
END
