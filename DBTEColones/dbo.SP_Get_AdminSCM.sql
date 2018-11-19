/****** Object:  StoredProcedure [dbo].[SP_Get_AdminSCM]    Script Date: 11/19/2018 02:28:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*
Author: Gustavo Fallas
Get data Admin_SCM
Return data if is correct, or return 0
*/


ALTER PROCEDURE [dbo].[SP_Get_AdminSCM]
	@Identification int
AS BEGIN
	BEGIN TRY
			SELECT U.FirstName, U.LastName, U.University, U.Headquarter, U.Email, U.Photo,
					   ASCM.Department, PN.PhoneNumber FROM [User] U
				INNER JOIN [Admin_SCM] ASCM ON U.Id = ASCM.Id
				LEFT JOIN [PhoneNumber] PN ON PN.Id = U.Id
				WHERE U.Id = @Identification


	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN
			RETURN 0;
	END CATCH	
END
GO


