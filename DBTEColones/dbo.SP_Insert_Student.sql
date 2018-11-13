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
    @PhotoData VARCHAR(MAX)

AS
BEGIN
	BEGIN TRY
		
		--insert User
		DECLARE @idUser INT;
		BEGIN TRANSACTION
			INSERT INTO [User](FirstName, LastName, University, Headquarter, Email, Password, Photo) 
			VALUES(@FirstName, @LastName, @University, @Headquarter, @Email, ENCRYPTBYPASSPHRASE('password', @Password), @PhotoData);
			SET @idUser = SCOPE_IDENTITY();
		COMMIT

		--insert Student
		BEGIN TRANSACTION
			INSERT INTO [Student](Id, Identification, Description, Skills)
			VALUES (@idUser, @Identification, @Description, @Skills);
		COMMIT

		--insert PhoneNumber_Student
		BEGIN TRANSACTION
			INSERT INTO [PhoneNumbers](Id, PhoneNumber) VALUES(@idUser, @PhoneNumber);
		COMMIT

		RETURN 1;

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN
			RETURN 0;
	END CATCH	
END