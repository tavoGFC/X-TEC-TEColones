/*
Author: Gustavo Fallas
Verify password and identification Admin o SCM
Return data if is correct, or return 0
*/


CREATE PROCEDURE SP_Verify_Admin_SCM
	@Identification int,
	@PasswordVerify VARCHAR(12)
AS BEGIN
	BEGIN TRY

		DECLARE @PasswordEncrypt AS VarBinary(MAX)
		DECLARE @Password AS VARCHAR(12)
		DECLARE @RETURN INT = 0
		SET @PasswordEncrypt = (SELECT  U.Password FROM [User] U
							WHERE U.Id = @Identification)

		SET @Password = DECRYPTBYPASSPHRASE('password', @PasswordEncrypt)
		IF (@Password = @PasswordVerify)
			BEGIN
				--ASCM.Admin: 1=Admin | 0=SCM
				SELECT  U.FirstName, U.LastName, U.University, U.Headquarter, U.Email, U.Photo,
						ASCM.Department, ASCM.Admin FROM [User] U
				INNER JOIN [Admin_SCM] ASCM ON U.Id = ASCM.Id
				WHERE U.Id = @Identification
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