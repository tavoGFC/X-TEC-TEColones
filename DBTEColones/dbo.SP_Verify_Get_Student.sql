USE [XTEColones]
GO

/****** Object: SqlProcedure [dbo].[SP_Verify_Get_Student] Script Date: 11/18/2018 16:49:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*
Author: Gustavo Fallas
Verify password and identification student
Return data if is correct, or return 0
*/


CREATE PROCEDURE [SP_Verify_Get_Student]
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
				DECLARE @photo_byte VARCHAR(MAX);
				DECLARE @photo_binary VARBINARY(MAX);
				SET @photo_binary = (SELECT U.Photo FROM [User] U WHERE U.Id = @Identification);
				SET @photo_byte = (SELECT CAST(@photo_binary AS VARCHAR(MAX)));
						
				
				SELECT U.FirstName, U.LastName, U.University, U.Headquarter, U.Email, U.Photo,
					   S.Description, S.Skills, S.TCS, PN.PhoneNumber FROM [User] U
				INNER JOIN [Student] S ON U.Id = S.Id
				INNER JOIN [PhoneNumber] PN ON PN.Id = U.Id
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
