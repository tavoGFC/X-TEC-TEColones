USE [XTEColones]
GO

/****** Object: SqlProcedure [dbo].[SP_Share_TCS] Script Date: 11/18/2018 16:49:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
Author: Gustavo Fallas
Share TCS
Return 1 if all goes fine or 0 if fails
*/


CREATE PROCEDURE SP_Share_TCS
	@Identification int,
	@IdentificationToShare int,
	@TCStoShare int
AS
BEGIN
	BEGIN TRY
		--Update Student 1
		BEGIN TRANSACTION
			UPDATE Student SET TCS = TCS - @TCStoShare
			WHERE Id = @Identification;
		COMMIT

		--Update Student 2
		BEGIN TRANSACTION			
			UPDATE Student SET TCS = TCS + @TCStoShare
			WHERe Id = @IdentificationToShare ;
		COMMIT	

		RETURN 1

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN
			RETURN 0
	END CATCH		
END
