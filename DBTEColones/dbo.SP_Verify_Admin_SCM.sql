/****** Object:  StoredProcedure [dbo].[SP_Verify_Admin_SCM]    Script Date: 11/18/2018 23:34:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
Author: Gustavo Fallas
Verify password and identification Admin o SCM
Return data if is correct, or return 0
*/


ALTER PROCEDURE [dbo].[SP_Verify_Admin_SCM]
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
				SELECT  ASCM.Admin FROM [Admin_SCM] ASCM
				WHERE ASCM.Id = @Identification
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
GO


