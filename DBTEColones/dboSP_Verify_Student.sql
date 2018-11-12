/*
Author: Gustavo Fallas
Verify password and identification student
Return data if is correct, or return 0
*/


CREATE PROCEDURE SP_Verify_Student
	@Identification int,
	@PasswordVerify VARCHAR(12)
AS BEGIN
	BEGIN TRY

		DECLARE @PasswordEncrypt AS VarBinary(MAX)
		DECLARE @Password AS VARCHAR(12)
		DECLARE @RETURN INT = 0
		SET @PasswordEncrypt = (SELECT  U.Password FROM [User] U
						INNER JOIN Student S ON U.Id = S.Id
						WHERE S.Identification = @Identification)

		SET @Password = DECRYPTBYPASSPHRASE('password', @PasswordEncrypt)
		IF (@Password = @PasswordVerify)
			BEGIN
				SELECT  U.FirstName, U.LastName, U.Photo, S.Id FROM [User] U
				INNER JOIN Student S ON U.Id = S.Id
				WHERE S.Identification = @Identification
			END
			ELSE
				SELECT @RETURN 

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN
			RETURN 0;
	END CATCH	
END