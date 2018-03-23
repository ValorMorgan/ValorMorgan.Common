/*-------------------------------------------------------
DO NOT EDIT THE FILE VERSION VALUE.  Add a new file with
the new version code if planning to upgrade the database.
-------------------------------------------------------*/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT ('Beginning CreditRequestToolkit database security setup.')

/*-------------------------------------------------------
	Check Database Exists
-------------------------------------------------------*/
IF DB_ID('CreditRequestToolkit') IS NULL
BEGIN
	PRINT ('The CreditRequestToolkit database was never made!')
	SET NOEXEC ON
END

GO

USE [CreditRequestToolkit]
GO

/*--------------------------------------------
	Variable Setup
--------------------------------------------*/
DECLARE @domain varchar(10) = (
	SELECT DEFAULT_DOMAIN()
)

DECLARE @user varchar(50)
DECLARE @userWithDomain varchar(60)
DECLARE @userCheck int

/*--------------------------------------------
	Domain Users
--------------------------------------------*/
SET @user = 'Domain Users'
SET @userWithDomain= @domain + '\' + @user

SET @userCheck = (
	SELECT COUNT(*)
	FROM sys.database_principals
	WHERE name = @userWithDomain
)

IF (@userCheck <= 0)
BEGIN
	IF (@domain = 'DEVQA')
		CREATE USER [DEVQA\Domain Users]

	IF (@domain = 'DEVCS')
		CREATE USER [DEVCS\Domain Users]

	IF (@domain = 'GS')
		CREATE USER [GS\Domain Users]

	EXEC sp_addrolemember 'db_datareader', @userWithDomain
END

/*--------------------------------------------
	CreditAnalysts
--------------------------------------------*/
SET @user = 'CreditAnalysts'
SET @userWithDomain= @domain + '\' + @user

SET @userCheck = (
	SELECT COUNT(*)
	FROM sys.database_principals
	WHERE name = @userWithDomain
)

IF (@userCheck <= 0)
BEGIN
	IF (@domain = 'DEVQA')
	BEGIN
		CREATE USER [DEVQA\CreditAnalysts]

		GRANT EXECUTE ON spDeletePDRatingCurrentPDRatingValues
		TO [DEVQA\CreditAnalysts]
		GRANT EXECUTE ON spDeletePDRatingHistoricalPDRatingValues
		TO [DEVQA\CreditAnalysts]
	END

	IF (@domain = 'DEVCS')
	BEGIN
		CREATE USER [DEVCS\CreditAnalysts]

		GRANT EXECUTE ON spDeletePDRatingCurrentPDRatingValues
		TO [DEVCS\CreditAnalysts]
		GRANT EXECUTE ON spDeletePDRatingHistoricalPDRatingValues
		TO [DEVCS\CreditAnalysts]
	END

	IF (@domain = 'GS')
	BEGIN
		CREATE USER [GS\CreditAnalysts]

		GRANT EXECUTE ON spDeletePDRatingCurrentPDRatingValues
		TO [GS\CreditAnalysts]
		GRANT EXECUTE ON spDeletePDRatingHistoricalPDRatingValues
		TO [GS\CreditAnalysts]
	END

	EXEC sp_addrolemember 'db_datareader', @userWithDomain
	EXEC sp_addrolemember 'db_datawriter', @userWithDomain
END

/*--------------------------------------------
	FSOs
--------------------------------------------*/
SET @user = 'FSOs'
SET @userWithDomain= @domain + '\' + @user

SET @userCheck = (
	SELECT COUNT(*)
	FROM sys.database_principals
	WHERE name = @userWithDomain
)

IF (@userCheck <= 0)
BEGIN
	IF (@domain = 'DEVQA')
	BEGIN
		CREATE USER [DEVQA\FSOs]

		GRANT EXECUTE ON spDeletePDRatingCurrentPDRatingValues
		TO [DEVQA\FSOs]
		GRANT EXECUTE ON spDeletePDRatingHistoricalPDRatingValues
		TO [DEVQA\FSOs]
	END

	IF (@domain = 'DEVCS')
	BEGIN
		CREATE USER [DEVCS\FSOs]

		GRANT EXECUTE ON spDeletePDRatingCurrentPDRatingValues
		TO [DEVCS\FSOs]
		GRANT EXECUTE ON spDeletePDRatingHistoricalPDRatingValues
		TO [DEVCS\FSOs]
	END

	IF (@domain = 'GS')
	BEGIN
		CREATE USER [GS\FSOs]

		GRANT EXECUTE ON spDeletePDRatingCurrentPDRatingValues
		TO [GS\FSOs]
		GRANT EXECUTE ON spDeletePDRatingHistoricalPDRatingValues
		TO [GS\FSOs]
	END

	EXEC sp_addrolemember 'db_datareader', @userWithDomain
	EXEC sp_addrolemember 'db_datawriter', @userWithDomain
END