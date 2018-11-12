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
		IF EXISTS (SELECT U.Id FROM [User] U
		INNER JOIN Student S ON U.Id = S.Id
		WHERE S.Identification =  @Identification)
			BEGIN
				SET @Exists = 1;
			END
			ELSE
				BEGIN
					--Verify if user exist in Admin_SCM -> Return 2
					IF EXISTS (SELECT U.Id FROM [User] U
					INNER JOIN Admin_ECA ASCM ON U.Id = ASCM.Id
					WHERE ASCM.Identification =  @Identification)
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