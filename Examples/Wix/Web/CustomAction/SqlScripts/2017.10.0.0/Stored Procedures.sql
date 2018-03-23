/*-------------------------------------------------------
DO NOT EDIT THE FILE VERSION VALUE.  Add a new file with
the new version code if planning to upgrade the database.
-------------------------------------------------------*/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT ('Beginning CreditRequestToolkit database stored procedure setup.')

/*-------------------------------------------------------
	Check Database Exists
-------------------------------------------------------*/
IF DB_ID('CreditRequestToolkit') IS NULL
BEGIN
	PRINT ('The CreditRequestToolkit database was never made!')
	SET NOEXEC ON
END

GO

/*-------------------------------------------------------
	spDeletePDRatingCurrentPDRatingValues
-------------------------------------------------------*/
IF OBJECT_ID('spDeletePDRatingCurrentPDRatingValues') IS NULL
    EXEC('CREATE PROCEDURE spDeletePDRatingCurrentPDRatingValues AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE spDeletePDRatingCurrentPDRatingValues
	@pdRatingId AS int
AS
BEGIN
	DECLARE @currentPDRatingValuesId AS int
	SET		@currentPDRatingValuesId = (SELECT CurrentPDRatingValuesId FROM PDRating WHERE PDRatingId = @pdRatingId)

	UPDATE PDRating
	SET CurrentPDRatingValuesId = NULL
	WHERE PDRatingId = @pdRatingId

	DELETE FROM CurrentPDRatingValues
	WHERE CurrentPDRatingValuesId = @currentPDRatingValuesId
END

GO

/*-------------------------------------------------------
	spDeletePDRatingHistoricalPDRatingValues
-------------------------------------------------------*/
IF OBJECT_ID('spDeletePDRatingHistoricalPDRatingValues') IS NULL
    EXEC('CREATE PROCEDURE spDeletePDRatingHistoricalPDRatingValues AS SET NOCOUNT ON;')
GO

ALTER PROCEDURE spDeletePDRatingHistoricalPDRatingValues
	@pdRatingId AS int
AS
BEGIN
	DECLARE @historicalPDRatingValuesId AS int
	SET		@historicalPDRatingValuesId = (SELECT HistoricalPDRatingValuesId FROM PDRating WHERE PDRatingId = @pdRatingId)

	UPDATE PDRating
	SET HistoricalPDRatingValuesId = NULL
	WHERE PDRatingId = @pdRatingId

	DELETE FROM HistoricalPDRatingValues
	WHERE HistoricalPDRatingValuesId = @historicalPDRatingValuesId
END

GO