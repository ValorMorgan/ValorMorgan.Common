/*-------------------------------------------------------
DO NOT EDIT THE FILE VERSION VALUE.  Add a new file with
the new version code if planning to upgrade the database.
-------------------------------------------------------*/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT ('Beginning CreditRequestToolkit database views creation.')

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

/*-------------------------------------------------------
	AllIndustries
-------------------------------------------------------*/
If OBJECT_ID('AllIndustries', 'V') IS NOT NULL
	DROP VIEW AllIndustries
GO

CREATE VIEW AllIndustries
AS (
	SELECT
		Industry.IndustryId,
		Industry.Name,

		PDRatingRatios.CDRCWeightingPercent AS [CDRC Weighting],
		PDRatingRatios.CDRCMarginWeightingPercent AS [CDRC Margin Weighting],
		PDRatingRatios.CurrentRatioWeightingPercent AS [Current Ratio Weighting],
		PDRatingRatios.OEWeightingPercent AS [OE Weighting],
		PDRatingRatios.ROAWeightingPercent AS [ROA Weighting],
		PDRatingRatios.WCAGIWeightingPercent AS [WCAGI Weighting],

		CDRCPercent.PDRating1 AS [CDRC PD 1],
		CDRCPercent.PDRating2 AS [CDRC PD 2],
		CDRCPercent.PDRating3 AS [CDRC PD 3],
		CDRCPercent.PDRating4 AS [CDRC PD 4],
		CDRCPercent.PDRating5 AS [CDRC PD 5],
		CDRCPercent.PDRating6 AS [CDRC PD 6],
		CDRCPercent.PDRating7 AS [CDRC PD 7],
		CDRCPercent.PDRating8 AS [CDRC PD 8],
		CDRCPercent.PDRating9 AS [CDRC PD 9],
		CDRCPercent.PDRating10 AS [CDRC PD 10],
		CDRCPercent.PDRating11 AS [CDRC PD 11],
		CDRCPercent.PDRating12 AS [CDRC PD 12],
		CDRCPercent.PDRating13 AS [CDRC PD 13],
		CDRCPercent.PDRating14 AS [CDRC PD 14],

		CDRCMarginPercent.PDRating1 AS [CDRC Margin PD 1],
		CDRCMarginPercent.PDRating2 AS [CDRC Margin PD 2],
		CDRCMarginPercent.PDRating3 AS [CDRC Margin PD 3],
		CDRCMarginPercent.PDRating4 AS [CDRC Margin PD 4],
		CDRCMarginPercent.PDRating5 AS [CDRC Margin PD 5],
		CDRCMarginPercent.PDRating6 AS [CDRC Margin PD 6],
		CDRCMarginPercent.PDRating7 AS [CDRC Margin PD 7],
		CDRCMarginPercent.PDRating8 AS [CDRC Margin PD 8],
		CDRCMarginPercent.PDRating9 AS [CDRC Margin PD 9],
		CDRCMarginPercent.PDRating10 AS [CDRC Margin PD 10],
		CDRCMarginPercent.PDRating11 AS [CDRC Margin PD 11],
		CDRCMarginPercent.PDRating12 AS [CDRC Margin PD 12],
		CDRCMarginPercent.PDRating13 AS [CDRC Margin PD 13],
		CDRCMarginPercent.PDRating14 AS [CDRC Margin PD 14],
	
		CurrentRatio.PDRating1 AS [Current Ratio PD 1],
		CurrentRatio.PDRating2 AS [Current Ratio PD 2],
		CurrentRatio.PDRating3 AS [Current Ratio PD 3],
		CurrentRatio.PDRating4 AS [Current Ratio PD 4],
		CurrentRatio.PDRating5 AS [Current Ratio PD 5],
		CurrentRatio.PDRating6 AS [Current Ratio PD 6],
		CurrentRatio.PDRating7 AS [Current Ratio PD 7],
		CurrentRatio.PDRating8 AS [Current Ratio PD 8],
		CurrentRatio.PDRating9 AS [Current Ratio PD 9],
		CurrentRatio.PDRating10 AS [Current Ratio PD 10],
		CurrentRatio.PDRating11 AS [Current Ratio PD 11],
		CurrentRatio.PDRating12 AS [Current Ratio PD 12],
		CurrentRatio.PDRating13 AS [Current Ratio PD 13],
		CurrentRatio.PDRating14 AS [Current Ratio PD 14],
	
		OEPercent.PDRating1 AS [OE PD 1],
		OEPercent.PDRating2 AS [OE PD 2],
		OEPercent.PDRating3 AS [OE PD 3],
		OEPercent.PDRating4 AS [OE PD 4],
		OEPercent.PDRating5 AS [OE PD 5],
		OEPercent.PDRating6 AS [OE PD 6],
		OEPercent.PDRating7 AS [OE PD 7],
		OEPercent.PDRating8 AS [OE PD 8],
		OEPercent.PDRating9 AS [OE PD 9],
		OEPercent.PDRating10 AS [OE PD 10],
		OEPercent.PDRating11 AS [OE PD 11],
		OEPercent.PDRating12 AS [OE PD 12],
		OEPercent.PDRating13 AS [OE PD 13],
		OEPercent.PDRating14 AS [OE PD 14],
	
		ROAPercent.PDRating1 AS [ROA PD 1],
		ROAPercent.PDRating2 AS [ROA PD 2],
		ROAPercent.PDRating3 AS [ROA PD 3],
		ROAPercent.PDRating4 AS [ROA PD 4],
		ROAPercent.PDRating5 AS [ROA PD 5],
		ROAPercent.PDRating6 AS [ROA PD 6],
		ROAPercent.PDRating7 AS [ROA PD 7],
		ROAPercent.PDRating8 AS [ROA PD 8],
		ROAPercent.PDRating9 AS [ROA PD 9],
		ROAPercent.PDRating10 AS [ROA PD 10],
		ROAPercent.PDRating11 AS [ROA PD 11],
		ROAPercent.PDRating12 AS [ROA PD 12],
		ROAPercent.PDRating13 AS [ROA PD 13],
		ROAPercent.PDRating14 AS [ROA PD 14],
	
		WCAGIPercent.PDRating1 AS [WCAGI PD 1],
		WCAGIPercent.PDRating2 AS [WCAGI PD 2],
		WCAGIPercent.PDRating3 AS [WCAGI PD 3],
		WCAGIPercent.PDRating4 AS [WCAGI PD 4],
		WCAGIPercent.PDRating5 AS [WCAGI PD 5],
		WCAGIPercent.PDRating6 AS [WCAGI PD 6],
		WCAGIPercent.PDRating7 AS [WCAGI PD 7],
		WCAGIPercent.PDRating8 AS [WCAGI PD 8],
		WCAGIPercent.PDRating9 AS [WCAGI PD 9],
		WCAGIPercent.PDRating10 AS [WCAGI PD 10],
		WCAGIPercent.PDRating11 AS [WCAGI PD 11],
		WCAGIPercent.PDRating12 AS [WCAGI PD 12],
		WCAGIPercent.PDRating13 AS [WCAGI PD 13],
		WCAGIPercent.PDRating14 AS [WCAGI PD 14]
	FROM Industry
		INNER JOIN PDRatingRatios
			ON Industry.PDRatingRatiosId = PDRatingRatios.PDRatingRatiosId
		INNER JOIN CDRCPercent
			ON PDRatingRatios.CDRCPercentId = CDRCPercent.CDRCPercentId
		INNER JOIN CDRCMarginPercent
			ON PDRatingRatios.CDRCMarginPercentId = CDRCMarginPercent.CDRCMarginPercentId
		INNER JOIN CurrentRatio
			ON PDRatingRatios.CurrentRatioId = CurrentRatio.CurrentRatioId
		INNER JOIN OEPercent
			ON PDRatingRatios.OEPercentId = OEPercent.OEPercentId
		INNER JOIN ROAPercent
			ON PDRatingRatios.ROAPercentId = ROAPercent.ROAPercentId
		INNER JOIN WCAGIPercent
			ON PDRatingRatios.WCAGIPercentId = WCAGIPercent.WCAGIPercentId
)
	
GO

PRINT ('AllIndustries created.')

/*-------------------------------------------------------
	IndustryCDRC
-------------------------------------------------------*/
If OBJECT_ID('IndustryCDRC', 'V') IS NOT NULL
	DROP VIEW IndustryCDRC
GO

CREATE VIEW IndustryCDRC
AS (
	SELECT
		Industry.IndustryId,
		Industry.Name,

		PDRatingRatios.CDRCWeightingPercent AS [CDRC Weighting],

		CDRCPercent.PDRating1 AS [CDRC PD 1],
		CDRCPercent.PDRating2 AS [CDRC PD 2],
		CDRCPercent.PDRating3 AS [CDRC PD 3],
		CDRCPercent.PDRating4 AS [CDRC PD 4],
		CDRCPercent.PDRating5 AS [CDRC PD 5],
		CDRCPercent.PDRating6 AS [CDRC PD 6],
		CDRCPercent.PDRating7 AS [CDRC PD 7],
		CDRCPercent.PDRating8 AS [CDRC PD 8],
		CDRCPercent.PDRating9 AS [CDRC PD 9],
		CDRCPercent.PDRating10 AS [CDRC PD 10],
		CDRCPercent.PDRating11 AS [CDRC PD 11],
		CDRCPercent.PDRating12 AS [CDRC PD 12],
		CDRCPercent.PDRating13 AS [CDRC PD 13],
		CDRCPercent.PDRating14 AS [CDRC PD 14]
	FROM Industry
		INNER JOIN PDRatingRatios
			ON Industry.PDRatingRatiosId = PDRatingRatios.PDRatingRatiosId
		INNER JOIN CDRCPercent
			ON PDRatingRatios.CDRCPercentId = CDRCPercent.CDRCPercentId
)
GO

PRINT ('IndustryCDRC created.')

/*-------------------------------------------------------
	IndustryCDRCMargin
-------------------------------------------------------*/
If OBJECT_ID('IndustryCDRCMargin', 'V') IS NOT NULL
	DROP VIEW IndustryCDRCMargin
GO

CREATE VIEW IndustryCDRCMargin
AS (
	SELECT
		Industry.IndustryId,
		Industry.Name,

		PDRatingRatios.CDRCMarginWeightingPercent AS [CDRC Margin Weighting],

		CDRCMarginPercent.PDRating1 AS [CDRC Margin PD 1],
		CDRCMarginPercent.PDRating2 AS [CDRC Margin PD 2],
		CDRCMarginPercent.PDRating3 AS [CDRC Margin PD 3],
		CDRCMarginPercent.PDRating4 AS [CDRC Margin PD 4],
		CDRCMarginPercent.PDRating5 AS [CDRC Margin PD 5],
		CDRCMarginPercent.PDRating6 AS [CDRC Margin PD 6],
		CDRCMarginPercent.PDRating7 AS [CDRC Margin PD 7],
		CDRCMarginPercent.PDRating8 AS [CDRC Margin PD 8],
		CDRCMarginPercent.PDRating9 AS [CDRC Margin PD 9],
		CDRCMarginPercent.PDRating10 AS [CDRC Margin PD 10],
		CDRCMarginPercent.PDRating11 AS [CDRC Margin PD 11],
		CDRCMarginPercent.PDRating12 AS [CDRC Margin PD 12],
		CDRCMarginPercent.PDRating13 AS [CDRC Margin PD 13],
		CDRCMarginPercent.PDRating14 AS [CDRC Margin PD 14]
	FROM Industry
		INNER JOIN PDRatingRatios
			ON Industry.PDRatingRatiosId = PDRatingRatios.PDRatingRatiosId
		INNER JOIN CDRCMarginPercent
			ON PDRatingRatios.CDRCMarginPercentId = CDRCMarginPercent.CDRCMarginPercentId
)
GO

PRINT ('IndustryCDRCMargin created.')

/*-------------------------------------------------------
	IndustryCurrentRatio
-------------------------------------------------------*/
If OBJECT_ID('IndustryCurrentRatio', 'V') IS NOT NULL
	DROP VIEW IndustryCurrentRatio
GO

CREATE VIEW IndustryCurrentRatio
AS (
	SELECT
		Industry.IndustryId,
		Industry.Name,

		PDRatingRatios.CurrentRatioWeightingPercent AS [Current Ratio Weighting],

		CurrentRatio.PDRating1 AS [Current Ratio PD 1],
		CurrentRatio.PDRating2 AS [Current Ratio PD 2],
		CurrentRatio.PDRating3 AS [Current Ratio PD 3],
		CurrentRatio.PDRating4 AS [Current Ratio PD 4],
		CurrentRatio.PDRating5 AS [Current Ratio PD 5],
		CurrentRatio.PDRating6 AS [Current Ratio PD 6],
		CurrentRatio.PDRating7 AS [Current Ratio PD 7],
		CurrentRatio.PDRating8 AS [Current Ratio PD 8],
		CurrentRatio.PDRating9 AS [Current Ratio PD 9],
		CurrentRatio.PDRating10 AS [Current Ratio PD 10],
		CurrentRatio.PDRating11 AS [Current Ratio PD 11],
		CurrentRatio.PDRating12 AS [Current Ratio PD 12],
		CurrentRatio.PDRating13 AS [Current Ratio PD 13],
		CurrentRatio.PDRating14 AS [Current Ratio PD 14]
	FROM Industry
		INNER JOIN PDRatingRatios
			ON Industry.PDRatingRatiosId = PDRatingRatios.PDRatingRatiosId
		INNER JOIN CurrentRatio
			ON PDRatingRatios.CurrentRatioId = CurrentRatio.CurrentRatioId
)
GO

PRINT ('IndustryCurrentRatio created.')

/*-------------------------------------------------------
	IndustryOE
-------------------------------------------------------*/
If OBJECT_ID('IndustryOE', 'V') IS NOT NULL
	DROP VIEW IndustryOE
GO

CREATE VIEW IndustryOE
AS (
	SELECT
		Industry.IndustryId,
		Industry.Name,

		PDRatingRatios.OEWeightingPercent AS [OE Weighting],

		OEPercent.PDRating1 AS [OE PD 1],
		OEPercent.PDRating2 AS [OE PD 2],
		OEPercent.PDRating3 AS [OE PD 3],
		OEPercent.PDRating4 AS [OE PD 4],
		OEPercent.PDRating5 AS [OE PD 5],
		OEPercent.PDRating6 AS [OE PD 6],
		OEPercent.PDRating7 AS [OE PD 7],
		OEPercent.PDRating8 AS [OE PD 8],
		OEPercent.PDRating9 AS [OE PD 9],
		OEPercent.PDRating10 AS [OE PD 10],
		OEPercent.PDRating11 AS [OE PD 11],
		OEPercent.PDRating12 AS [OE PD 12],
		OEPercent.PDRating13 AS [OE PD 13],
		OEPercent.PDRating14 AS [OE PD 14]
	FROM Industry
		INNER JOIN PDRatingRatios
			ON Industry.PDRatingRatiosId = PDRatingRatios.PDRatingRatiosId
		INNER JOIN OEPercent
			ON PDRatingRatios.OEPercentId = OEPercent.OEPercentId
)
GO

PRINT ('IndustryOE created.')

/*-------------------------------------------------------
	IndustryROA
-------------------------------------------------------*/
If OBJECT_ID('IndustryROA', 'V') IS NOT NULL
	DROP VIEW IndustryROA
GO

CREATE VIEW IndustryROA
AS (
	SELECT
		Industry.IndustryId,
		Industry.Name,

		PDRatingRatios.ROAWeightingPercent AS [ROA Weighting],

		ROAPercent.PDRating1 AS [ROA PD 1],
		ROAPercent.PDRating2 AS [ROA PD 2],
		ROAPercent.PDRating3 AS [ROA PD 3],
		ROAPercent.PDRating4 AS [ROA PD 4],
		ROAPercent.PDRating5 AS [ROA PD 5],
		ROAPercent.PDRating6 AS [ROA PD 6],
		ROAPercent.PDRating7 AS [ROA PD 7],
		ROAPercent.PDRating8 AS [ROA PD 8],
		ROAPercent.PDRating9 AS [ROA PD 9],
		ROAPercent.PDRating10 AS [ROA PD 10],
		ROAPercent.PDRating11 AS [ROA PD 11],
		ROAPercent.PDRating12 AS [ROA PD 12],
		ROAPercent.PDRating13 AS [ROA PD 13],
		ROAPercent.PDRating14 AS [ROA PD 14]
	FROM Industry
		INNER JOIN PDRatingRatios
			ON Industry.PDRatingRatiosId = PDRatingRatios.PDRatingRatiosId
		INNER JOIN ROAPercent
			ON PDRatingRatios.ROAPercentId = ROAPercent.ROAPercentId
)
GO

PRINT ('IndustryROA created.')

/*-------------------------------------------------------
	IndustryWCAGI
-------------------------------------------------------*/
If OBJECT_ID('IndustryWCAGI', 'V') IS NOT NULL
	DROP VIEW IndustryWCAGI
GO

CREATE VIEW IndustryWCAGI
AS (
	SELECT
		Industry.IndustryId,
		Industry.Name,

		PDRatingRatios.WCAGIWeightingPercent AS [WCAGI Weighting],

		WCAGIPercent.PDRating1 AS [WCAGI PD 1],
		WCAGIPercent.PDRating2 AS [WCAGI PD 2],
		WCAGIPercent.PDRating3 AS [WCAGI PD 3],
		WCAGIPercent.PDRating4 AS [WCAGI PD 4],
		WCAGIPercent.PDRating5 AS [WCAGI PD 5],
		WCAGIPercent.PDRating6 AS [WCAGI PD 6],
		WCAGIPercent.PDRating7 AS [WCAGI PD 7],
		WCAGIPercent.PDRating8 AS [WCAGI PD 8],
		WCAGIPercent.PDRating9 AS [WCAGI PD 9],
		WCAGIPercent.PDRating10 AS [WCAGI PD 10],
		WCAGIPercent.PDRating11 AS [WCAGI PD 11],
		WCAGIPercent.PDRating12 AS [WCAGI PD 12],
		WCAGIPercent.PDRating13 AS [WCAGI PD 13],
		WCAGIPercent.PDRating14 AS [WCAGI PD 14]
	FROM Industry
		INNER JOIN PDRatingRatios
			ON Industry.PDRatingRatiosId = PDRatingRatios.PDRatingRatiosId
		INNER JOIN WCAGIPercent
			ON PDRatingRatios.WCAGIPercentId = WCAGIPercent.WCAGIPercentId
)
GO

PRINT ('IndustryWCAGI created.')


/*-------------------------------------------------------
	Skip to end of script
-------------------------------------------------------*/
GOTO FINISH

/*-------------------------------------------------------
	Handle errors
-------------------------------------------------------*/
SET NOEXEC OFF
	PRINT 'ERROR OCCURED!  Any transaction will rollback.'

	IF XACT_STATE() <> 0
	BEGIN
		ROLLBACK TRANSACTION
		PRINT 'Transaction rolled back.'
	END

/*-------------------------------------------------------
	END OF SCRIPT
-------------------------------------------------------*/
FINISH:
PRINT ('CreditRequestToolkit Views successfully created.')