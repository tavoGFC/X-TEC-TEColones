USE [XTEColones]
GO

/****** Object: SqlProcedure [dbo].[SP_Insert_Log_Assign] Script Date: 11/18/2018 16:48:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*
Author: Gustavo Fallas
Insert new register for log assign tcs to benefit
Return 1 if all goes fine or 0 if fails (exist)
*/


CREATE PROCEDURE SP_Insert_Log_Assign
	@Identification INT,
	@Benefit VARCHAR(50),
	@TCS INT,
	@CS FLOAT,
	@ExRtoDate FLOAT

AS
BEGIN
	BEGIN TRY	
		BEGIN TRANSACTION
			INSERT INTO Log_Assign(IdUser, Benefit, TCS, CS, DateAssign, ExchangeRateToDate) 
			VALUES(@Identification, @Benefit, @TCS, @CS, CONVERT(date, GETDATE()), @ExRtoDate);
		COMMIT

		RETURN 1;

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRAN
			RETURN 0;
	END CATCH	
END
