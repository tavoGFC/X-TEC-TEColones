
DECLARE @Exists INT;
DECLARE @Identification INT = 2014035396;

IF EXISTS (SELECT U.Id FROM [User] U
INNER JOIN Student S ON U.Id = S.Id
WHERE S.Identification =  @Identification)
	BEGIN
		SET @Exists = 1;
	END
	ELSE
		BEGIN
			IF EXISTS (SELECT U.Id FROM [User] U
			INNER JOIN Admin_ECA ASCM ON U.Id = ASCM.Id
			WHERE ASCM.Identification =  @Identification)
				BEGIN
					SET @Exists = 1;
				END
			ELSE
				BEGIN
					SET @Exists = 0;
				END
		END
SELECT @Exists;


DECLARE @PasswordEncrypt AS VarBinary(MAX)
DECLARE @Password AS VARCHAR(12)
DECLARE @PasswordVerify VARCHAR(12) = 'gustavo123'
DECLARE @Identification INT = 2014035394
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





DECLARE @PasswordEncrypt AS VarBinary(MAX)
DECLARE @Password AS VARCHAR(12)
DECLARE @PasswordVerify VARCHAR(12) = 'ricardo123'
DECLARE @Identification INT = 2014040801
DECLARE @RETURN INT = 0
SET @PasswordEncrypt = (SELECT  U.Password FROM [User] U
				INNER JOIN Admin_ECA ASCM ON U.Id = ASCM.Id
				WHERE ASCM.Identification = @Identification)

SET @Password = DECRYPTBYPASSPHRASE('password', @PasswordEncrypt)
IF (@Password = @PasswordVerify)
	BEGIN
		SELECT  U.FirstName, U.LastName, U.Photo, ASCM.Id, ASCM.Admin FROM [User] U
		INNER JOIN Admin_ECA ASCM ON U.Id = ASCM.Id
		WHERE ASCM.Identification = @Identification
	END
	ELSE
		SELECT @RETURN 











/*
Retornar la clave verdadera
*/
DECLARE @clave AS VarBinary(MAX)
DECLARE @pass AS VARCHAR(25)
SET @clave = (SELECT Password FROM Student)
SET @pass = DECRYPTBYPASSPHRASE('password', @clave)
SELECT  @pass
