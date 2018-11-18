USE [XTEColones]
GO

/****** Object: SqlProcedure [dbo].[SP_Exist_User] Script Date: 11/18/2018 16:47:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*
Author: Gustavo Fallas
Verify if exist user
Return 1, 2 or 0
*/


CREATE PROCEDURE SP_Exist_User
	@Identification int
AS BEGIN
	BEGIN TRY

		DECLARE @Exists INT;

		--Verify if user exist in Student -> Return 1
		IF EXISTS (SELECT S.Id FROM [Student] S 
				WHERE S.Id =  @Identification)
			BEGIN
				SET @Exists = 1;
			END
			ELSE
				BEGIN
					--Verify if user exist in Admin_SCM -> Return 2
					IF EXISTS (SELECT A_SCM.Id FROM [Admin_SCM] A_SCM
								WHERE A_SCM.Id =  @Identification)
						BEGIN
							SET @Exists = 2;
						END

					--Return 0 if user not exist
					ELSE
						BEGIN
							SET @Exists = 0;
						END
				END
		SELECT @Exists;
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN
			RETURN 0;
	END CATCH	
END
