/*
Author: Gustavo Fallas
Insert new register for Admin or SCM
Return 1 if all goes fine or 0 if fails (exist)
*/


CREATE PROCEDURE SP_Insert_Admin_SCM
	@Identification VARCHAR(50),
	@Department VARCHAR(80),
	@IsAdmin bit,   -- 1:Admin  || 0:SCM

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

		--insert Admin o SCM
		BEGIN TRANSACTION
			INSERT INTO [Admin_ECA](Id, Identification, Department, Admin)
			VALUES (@idUser, @Identification, @Department, @IsAdmin);
		COMMIT		

		RETURN 1;

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN
			RETURN 0;
	END CATCH	
END