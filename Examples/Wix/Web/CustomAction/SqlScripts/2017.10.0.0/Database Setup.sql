/*-------------------------------------------------------
DO NOT EDIT THE FILE VERSION VALUE.  Add a new file with
the new version code if planning to upgrade the database.
-------------------------------------------------------*/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

PRINT ('Beginning CreditRequestToolkit database creation.')

/*-------------------------------------------------------
	Create the database
-------------------------------------------------------*/
IF DB_ID('CreditRequestToolkit') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit database.')
	BEGIN TRY
		CREATE DATABASE [CreditRequestToolkit]
		CONTAINMENT = NONE
		WITH
			FILESTREAM (
				NON_TRANSACTED_ACCESS = READ_ONLY
			)
		;
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit database already exists.  Skipped.')

GO

USE [CreditRequestToolkit]
GO

/*-------------------------------------------------------
	Create the CreditActionEForm table
-------------------------------------------------------*/
IF OBJECT_ID('CreditActionEForm', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.CreditActionEForm table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[CreditActionEForm] (
			TransactionId		bigint			NOT NULL,
			CIF					int				NOT NULL,
			EFormStatus			varchar(max)	NOT NULL	DEFAULT(0),
			FSOName				varchar(max)	NULL,
			CreatedOn			datetime		NOT NULL	DEFAULT(GETDATE()),
			CreatedBy			varchar(max)	NULL,
			UpdatedOn			datetime		NULL,
			UpdatedBy			varchar(max)	NULL,
			SubmittedOn			datetime		NULL,
			SubmittedBy			varchar(max)	NULL,
			CONSTRAINT PK_CreditActionEForm PRIMARY KEY (TransactionId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.CreditActionEForm table already created.  Skipped.')

GO


/*-------------------------------------------------------
	Create the CreditPresentation table
-------------------------------------------------------*/
IF OBJECT_ID('CreditPresentation', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.CreditPresentation table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[CreditPresentation] (
			CreditPresentationId	int				NOT NULL	IDENTITY(1,1),
			TransactionId			bigint			NOT NULL,
			FileLocation			varchar(max)	NOT NULL,
			UploadedOn				datetime		NOT NULL	DEFAULT(GETDATE()),
			UploadedBy				varchar(max)	NULL,
			CONSTRAINT PK_CreditPresentation PRIMARY KEY (CreditPresentationId),
			CONSTRAINT FK_CreditPresentation_CreditActionEForm FOREIGN KEY (TransactionId)
				REFERENCES [CreditActionEForm](TransactionId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.CreditPresentation table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the CDRCPercent table
-------------------------------------------------------*/
IF OBJECT_ID('CDRCPercent', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.CDRCPercent table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[CDRCPercent] (
			CDRCPercentId	int				NOT NULL	IDENTITY(1,1),
			PDRating1		decimal(5,2)	NULL,
			PDRating2		decimal(5,2)	NULL,
			PDRating3		decimal(5,2)	NULL,
			PDRating4		decimal(5,2)	NULL,
			PDRating5		decimal(5,2)	NULL,
			PDRating6		decimal(5,2)	NULL,
			PDRating7		decimal(5,2)	NULL,
			PDRating8		decimal(5,2)	NULL,
			PDRating9		decimal(5,2)	NULL,
			PDRating10		decimal(5,2)	NULL,
			PDRating11		decimal(5,2)	NULL,
			PDRating12		decimal(5,2)	NULL,
			PDRating13		decimal(5,2)	NULL,
			PDRating14		decimal(5,2)	NULL,
			CONSTRAINT PK_CDRCPercent PRIMARY KEY (CDRCPercentId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.CDRCPercent table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the CDRCMarginPercent table
-------------------------------------------------------*/
IF OBJECT_ID('CDRCMarginPercent', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.CDRCMarginPercent table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[CDRCMarginPercent] (
			CDRCMarginPercentId	int				NOT NULL	IDENTITY(1,1),
			PDRating1			decimal(5,2)	NULL,
			PDRating2			decimal(5,2)	NULL,
			PDRating3			decimal(5,2)	NULL,
			PDRating4			decimal(5,2)	NULL,
			PDRating5			decimal(5,2)	NULL,
			PDRating6			decimal(5,2)	NULL,
			PDRating7			decimal(5,2)	NULL,
			PDRating8			decimal(5,2)	NULL,
			PDRating9			decimal(5,2)	NULL,
			PDRating10			decimal(5,2)	NULL,
			PDRating11			decimal(5,2)	NULL,
			PDRating12			decimal(5,2)	NULL,
			PDRating13			decimal(5,2)	NULL,
			PDRating14			decimal(5,2)	NULL,
			CONSTRAINT PK_CDRCMarginPercent PRIMARY KEY (CDRCMarginPercentId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.CDRCMarginPercent table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the CurrentRatio table
-------------------------------------------------------*/
IF OBJECT_ID('CurrentRatio', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.CurrentRatio table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[CurrentRatio] (
			CurrentRatioId	int				NOT NULL	IDENTITY(1,1),
			PDRating1		decimal(5,2)	NULL,
			PDRating2		decimal(5,2)	NULL,
			PDRating3		decimal(5,2)	NULL,
			PDRating4		decimal(5,2)	NULL,
			PDRating5		decimal(5,2)	NULL,
			PDRating6		decimal(5,2)	NULL,
			PDRating7		decimal(5,2)	NULL,
			PDRating8		decimal(5,2)	NULL,
			PDRating9		decimal(5,2)	NULL,
			PDRating10		decimal(5,2)	NULL,
			PDRating11		decimal(5,2)	NULL,
			PDRating12		decimal(5,2)	NULL,
			PDRating13		decimal(5,2)	NULL,
			PDRating14		decimal(5,2)	NULL,
			CONSTRAINT PK_CurrentRatio PRIMARY KEY (CurrentRatioId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.CurrentRatioPercent table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the OEPercent table
-------------------------------------------------------*/
IF OBJECT_ID('OEPercent', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.OEPercent table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[OEPercent] (
			OEPercentId		int				NOT NULL	IDENTITY(1,1),
			PDRating1		decimal(5,2)	NULL,
			PDRating2		decimal(5,2)	NULL,
			PDRating3		decimal(5,2)	NULL,
			PDRating4		decimal(5,2)	NULL,
			PDRating5		decimal(5,2)	NULL,
			PDRating6		decimal(5,2)	NULL,
			PDRating7		decimal(5,2)	NULL,
			PDRating8		decimal(5,2)	NULL,
			PDRating9		decimal(5,2)	NULL,
			PDRating10		decimal(5,2)	NULL,
			PDRating11		decimal(5,2)	NULL,
			PDRating12		decimal(5,2)	NULL,
			PDRating13		decimal(5,2)	NULL,
			PDRating14		decimal(5,2)	NULL,
			CONSTRAINT PK_OEPercent PRIMARY KEY (OEPercentId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.OEPercent table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the ROAPercent table
-------------------------------------------------------*/
IF OBJECT_ID('ROAPercent', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.ROAPercent table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[ROAPercent] (
			ROAPercentId	int				NOT NULL	IDENTITY(1,1),
			PDRating1		decimal(5,2)	NULL,
			PDRating2		decimal(5,2)	NULL,
			PDRating3		decimal(5,2)	NULL,
			PDRating4		decimal(5,2)	NULL,
			PDRating5		decimal(5,2)	NULL,
			PDRating6		decimal(5,2)	NULL,
			PDRating7		decimal(5,2)	NULL,
			PDRating8		decimal(5,2)	NULL,
			PDRating9		decimal(5,2)	NULL,
			PDRating10		decimal(5,2)	NULL,
			PDRating11		decimal(5,2)	NULL,
			PDRating12		decimal(5,2)	NULL,
			PDRating13		decimal(5,2)	NULL,
			PDRating14		decimal(5,2)	NULL,
			CONSTRAINT PK_ROAPercent PRIMARY KEY (ROAPercentId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.ROAPercent table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the WCAGIPercent table
-------------------------------------------------------*/
IF OBJECT_ID('WCAGIPercent', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.WCAGIPercent table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[WCAGIPercent] (
			WCAGIPercentId	int				NOT NULL	IDENTITY(1,1),
			PDRating1		decimal(5,2)	NULL,
			PDRating2		decimal(5,2)	NULL,
			PDRating3		decimal(5,2)	NULL,
			PDRating4		decimal(5,2)	NULL,
			PDRating5		decimal(5,2)	NULL,
			PDRating6		decimal(5,2)	NULL,
			PDRating7		decimal(5,2)	NULL,
			PDRating8		decimal(5,2)	NULL,
			PDRating9		decimal(5,2)	NULL,
			PDRating10		decimal(5,2)	NULL,
			PDRating11		decimal(5,2)	NULL,
			PDRating12		decimal(5,2)	NULL,
			PDRating13		decimal(5,2)	NULL,
			PDRating14		decimal(5,2)	NULL,
			CONSTRAINT PK_WCAGIPercent PRIMARY KEY (WCAGIPercentId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.WCAGIPercent table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the PDRatingRatios table
-------------------------------------------------------*/
IF OBJECT_ID('PDRatingRatios', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.PDRatingRatios table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[PDRatingRatios] (
			PDRatingRatiosId				int				NOT NULL	IDENTITY(1,1),
			CDRCPercentId					int				NOT NULL,
			CDRCMarginPercentId				int				NOT NULL,
			CurrentRatioId					int				NOT NULL,
			OEPercentId						int				NOT NULL,
			ROAPercentId					int				NOT NULL,
			WCAGIPercentId					int				NOT NULL,
			CDRCWeightingPercent			decimal(5,2)	NOT NULL,
			CDRCMarginWeightingPercent		decimal(5,2)	NOT NULL,
			CurrentRatioWeightingPercent	decimal(5,2)	NOT NULL,
			OEWeightingPercent				decimal(5,2)	NOT NULL,
			ROAWeightingPercent				decimal(5,2)	NOT NULL,
			WCAGIWeightingPercent			decimal(5,2)	NOT NULL,
			ManagementWeightingPercent		decimal(5,2)	NOT NULL,
			CONSTRAINT PK_PDRatingRatios PRIMARY KEY (PDRatingRatiosId),
			CONSTRAINT FK_PDRatingRatios_CDRCPercent FOREIGN KEY (CDRCPercentId)
				REFERENCES [CDRCPercent](CDRCPercentId),
			CONSTRAINT FK_PDRatingRatios_CDRCMarginPercent FOREIGN KEY (CDRCMarginPercentId)
				REFERENCES [CDRCMarginPercent](CDRCMarginPercentId),
			CONSTRAINT FK_PDRatingRatios_CurrentRatio FOREIGN KEY (CurrentRatioId)
				REFERENCES [CurrentRatio](CurrentRatioId),
			CONSTRAINT FK_PDRatingRatios_OEPercent FOREIGN KEY (OEPercentId)
				REFERENCES [OEPercent](OEPercentId),
			CONSTRAINT FK_PDRatingRatios_ROAPercent FOREIGN KEY (ROAPercentId)
				REFERENCES [ROAPercent](ROAPercentId),
			CONSTRAINT FK_PDRatingRatios_WCAGIPercent FOREIGN KEY (WCAGIPercentId)
				REFERENCES [WCAGIPercent](WCAGIPercentId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.PDRatingRatios table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the Industry table
-------------------------------------------------------*/
IF OBJECT_ID('Industry', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.Industry table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[Industry] (
			IndustryId			int				NOT NULL	IDENTITY(1,1),
			PDRatingRatiosId	int				NOT NULL,
			Name				varchar(50)		NOT NULL,
			CONSTRAINT PK_Industry PRIMARY KEY (IndustryId),
			CONSTRAINT FK_Industry_PDRatingRatios FOREIGN KEY (PDRatingRatiosId)
				REFERENCES [PDRatingRatios](PDRatingRatiosId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.Industry table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the CurrentPDRatingValues table
-------------------------------------------------------*/
IF OBJECT_ID('CurrentPDRatingValues', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.CurrentPDRatingValues table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[CurrentPDRatingValues] (
			CurrentPDRatingValuesId		int				NOT NULL	IDENTITY(1,1),
			CDRCPercent					decimal(5,2)	NULL,
			CDRCMarginPercent			decimal(5,2)	NULL,
			Ratio						decimal(5,2)	NULL,
			OEPercent					decimal(5,2)	NULL,
			ROAPercent					decimal(5,2)	NULL,
			WCAGIPercent				decimal(5,2)	NULL,
			NetIncome					money			NULL,
			TotalAGI					money			NULL,
			TotalAssets					money			NULL,
			TotalLiabilities			money			NULL,
			TotalDepreciationExpense	money			NULL,
			TotalNonFarmIncome			money			NULL,
			TotalFamilyLivingExpense	money			NULL,
			TotalIncomeTaxExpense		money			NULL,
			TotalInteresteExpense		money			NULL,
			TotalInterestTermDebt		money			NULL,
			TotalPrincipalTermDebt		money			NULL,
			CONSTRAINT PK_CurrentPDRatingValues PRIMARY KEY (CurrentPDRatingValuesId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.CurrentPDRatingValues table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the HistoricalPDRatingValues table
-------------------------------------------------------*/
IF OBJECT_ID('HistoricalPDRatingValues', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.HistoricalPDRatingValues table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[HistoricalPDRatingValues] (
			HistoricalPDRatingValuesId	int				NOT NULL	IDENTITY(1,1),
			CDRCPercent					decimal(5,2)	NULL,
			CDRCMarginPercent			decimal(5,2)	NULL,
			Ratio						decimal(5,2)	NULL,
			OEPercent					decimal(5,2)	NULL,
			ROAPercent					decimal(5,2)	NULL,
			WCAGIPercent				decimal(5,2)	NULL,
			NetIncome					money			NULL,
			TotalAGI					money			NULL,
			TotalAssets					money			NULL,
			TotalLiabilities			money			NULL,
			TotalDepreciationExpense	money			NULL,
			TotalNonFarmIncome			money			NULL,
			TotalFamilyLivingExpense	money			NULL,
			TotalIncomeTaxExpense		money			NULL,
			TotalInteresteExpense		money			NULL,
			TotalInterestTermDebt		money			NULL,
			TotalPrincipalTermDebt		money			NULL,
			CONSTRAINT PK_HistoricalPDRatingValues PRIMARY KEY (HistoricalPDRatingValuesId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.HistoricalPDRatingValues table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the Delinquency table
-------------------------------------------------------*/
IF OBJECT_ID('Delinquency', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.Delinquency table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[Delinquency] (
			DelinquencyId		int				NOT NULL	IDENTITY(1,1),
			TimeOfDelinquency	varchar(50)		NOT NULL,
			PDRatingAdjustment	decimal(4,2)	NOT NULL,
			CONSTRAINT PK_Delinquency PRIMARY KEY (DelinquencyId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.Delinquency table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the PDRating table
-------------------------------------------------------*/
IF OBJECT_ID('PDRating', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.PDRating table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[PDRating] (
			PDRatingId					int				NOT NULL	IDENTITY(1,1),
			TransactionId				bigint			NOT NULL,
			IndustryId					int				NULL,
			CurrentPDRatingValuesId		int				NULL,
			HistoricalPDRatingValuesId	int				NULL,
			GreenStoneDelinquencyId		int				NULL,
			NetScoreDelinquencyId		int				NULL,
			OtherDelinquencyId			int				NULL,
			YearWeighting				decimal(5,2)	NULL,
			Expansion					bit				NULL,
			SubjectiveFactors			decimal(4,2)	NULL,
			Comments					varchar(max)	NULL,
			CreatedOn					datetime		NOT NULL	DEFAULT(GETDATE()),
			CreatedBy					varchar(max)	NULL,
			UpdatedOn					datetime		NULL,
			UpdatedBy					varchar(max)	NULL,
			CONSTRAINT PK_PDRating PRIMARY KEY (PDRatingId),
			CONSTRAINT FK_PDRating_CreditActionEForm FOREIGN KEY (TransactionId)
				REFERENCES [CreditActionEForm](TransactionId),
			CONSTRAINT FK_PDRating_Industry FOREIGN KEY (IndustryId)
				REFERENCES [Industry](IndustryId),
			CONSTRAINT FK_PDRating_CurrentPDRatingValues FOREIGN KEY (CurrentPDRatingValuesId)
				REFERENCES [CurrentPDRatingValues](CurrentPDRatingValuesId),
			CONSTRAINT FK_PDRating_HistoricalPDRatingValues FOREIGN KEY (HistoricalPDRatingValuesId)
				REFERENCES [HistoricalPDRatingValues](HistoricalPDRatingValuesId),
			CONSTRAINT FK_PDRating_Delinquency_GreenStone FOREIGN KEY (GreenStoneDelinquencyId)
				REFERENCES [Delinquency](DelinquencyId),
			CONSTRAINT FK_PDRating_Delinquency_NetScore FOREIGN KEY (NetScoreDelinquencyId)
				REFERENCES [Delinquency](DelinquencyId),
			CONSTRAINT FK_PDRating_Delinquency_Other FOREIGN KEY (OtherDelinquencyId)
				REFERENCES [Delinquency](DelinquencyId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.PDRating table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the Loan table
-------------------------------------------------------*/
IF OBJECT_ID('Loan', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.Loan table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[Loan] (
			LoanNumber		bigint			NOT NULL,
			TransactionId	bigint			NOT NULL,
			Amount			money			NOT NULL,
			AddedOn			datetime		NOT NULL	DEFAULT(GETDATE()),
			AddedBy			varchar(max)	NULL,
			CONSTRAINT PK_Loan PRIMARY KEY (LoanNumber),
			CONSTRAINT FK_Loan_CreditActionEForm FOREIGN KEY (TransactionId)
				REFERENCES [CreditActionEForm](TransactionId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.Loan table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the LGDRecord table
-------------------------------------------------------*/
IF OBJECT_ID('LGDRecord', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.LGDRecord table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[LGDRecord] (
			LGDRecordId					int				NOT NULL	IDENTITY(1,1),
			TransactionId				bigint			NOT NULL,
			LoanNumber					bigint			NOT NULL,
			GovernmentGuarantee			bit				NULL,
			GovernmentGuaranteePercent	decimal(5,2)	NULL,
			UnsecuredManuallyAnalyzed	bit				NULL,
			ScoredOperating				bit				NULL,
			LoanAV						decimal(5,2)	NULL,
			CreatedOn					datetime		NOT NULL	DEFAULT(GETDATE()),
			CreatedBy					varchar(max)	NULL,
			UpdatedOn					datetime		NULL,
			UpdatedBy					varchar(max)	NULL,
			CONSTRAINT PK_LGDRecord PRIMARY KEY (LGDRecordId),
			CONSTRAINT FK_LGDRecord_CreditActionEForm FOREIGN KEY (TransactionId)
				REFERENCES [CreditActionEForm](TransactionId),
			CONSTRAINT FK_LGDRecord_Loan FOREIGN KEY (LoanNumber)
				REFERENCES [Loan](LoanNumber)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.LGDRecord table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the TierFactorCategory table
-------------------------------------------------------*/
IF OBJECT_ID('TierFactorCategory', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.TierFactorCategory table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[TierFactorCategory] (
			TierFactorCategoryId	int			NOT NULL	IDENTITY(1,1),
			Name					varchar(50)	NOT NULL,
			MaxScore				smallint	NOT NULL,
			CONSTRAINT PK_TierFactorCategory PRIMARY KEY (TierFactorCategoryId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.TierFactorCategory table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the ManagementFactor table
-------------------------------------------------------*/
IF OBJECT_ID('ManagementFactor', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.ManagementFactor table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[ManagementFactor] (
			ManagementFactorId		int			NOT NULL	IDENTITY(1,1),
			Factor					varchar(50)	NOT NULL,
			Score					smallint	NOT NULL,
			CONSTRAINT PK_ManagementFactor PRIMARY KEY (ManagementFactorId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.ManagementFactor table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the CDRCFactor table
-------------------------------------------------------*/
IF OBJECT_ID('CDRCFactor', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.CDRCFactor table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[CDRCFactor] (
			CDRCFactorId	int			NOT NULL	IDENTITY(1,1),
			Factor			varchar(50)	NOT NULL,
			Score			smallint	NOT NULL,
			CONSTRAINT PK_CDRCFactor PRIMARY KEY (CDRCFactorId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.CDRCFactor table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the AppraisalFactor table
-------------------------------------------------------*/
IF OBJECT_ID('AppraisalFactor', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.AppraisalFactor table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[AppraisalFactor] (
			AppraisalFactorId	int			NOT NULL	IDENTITY(1,1),
			Factor				varchar(50)	NOT NULL,
			Score				smallint	NOT NULL,
			CONSTRAINT PK_AppraisalFactor PRIMARY KEY (AppraisalFactorId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.AppraisalFactor table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the TierPricing table
-------------------------------------------------------*/
IF OBJECT_ID('TierPricing', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.TierPricing table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[TierPricing] (
			TierPricingId		int				NOT NULL	IDENTITY(1,1),
			TransactionId		bigint			NOT NULL,
			LoanNumber			bigint			NOT NULL,
			CreatedOn			datetime		NOT NULL	DEFAULT(GETDATE()),
			CreatedBy			varchar(max)	NULL,
			UpdatedOn			datetime		NULL,
			UpdatedBy			varchar(max)	NULL,
			CONSTRAINT PK_TierPricing PRIMARY KEY (TierPricingId),
			CONSTRAINT FK_TierPricing_CreditActionEForm FOREIGN KEY (TransactionId)
				REFERENCES [CreditActionEForm](TransactionId),
			CONSTRAINT FK_TierPricing_Loan FOREIGN KEY (LoanNumber)
				REFERENCES [Loan](LoanNumber)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.TierPricing table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create the TierPricingFactor table
-------------------------------------------------------*/
IF OBJECT_ID('TierPricingFactor', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.TierPricingFactor table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[TierPricingFactor] (
			TierPricingFactorId		int				NOT NULL	IDENTITY(1,1),
			TierPricingId			int				NOT NULL,
			TierFactorCategoryId	int				NULL,
			AppraisalFactorId		int				NULL,
			CDRCFactorId			int				NULL,
			ManagementFactorId		int				NULL,
			NetWorth				decimal(5,2)	NULL,
			NetWorthScore			smallint		NULL,
			WCAGI					decimal(5,2)	NULL,
			WCAGIScore				smallint		NULL,
			CONSTRAINT PK_TierFactor PRIMARY KEY (TierPricingFactorId),
			CONSTRAINT FK_TierPricingFactor_TierPricing FOREIGN KEY (TierPricingId)
				REFERENCES [TierPricing](TierPricingId),
			CONSTRAINT FK_TierPricingFactor_TierFactorCategory FOREIGN KEY (TierFactorCategoryId)
				REFERENCES [TierFactorCategory](TierFactorCategoryId),
			CONSTRAINT FK_TierPricingFactor_ManagementFactor FOREIGN KEY (ManagementFactorId)
				REFERENCES [ManagementFactor](ManagementFactorId),
			CONSTRAINT FK_TierPricingFactor_CDRCFactor FOREIGN KEY (CDRCFactorId)
				REFERENCES [CDRCFactor](CDRCFactorId),
			CONSTRAINT FK_TierPricingFactor_AppraisalFactor FOREIGN KEY (AppraisalFactorId)
				REFERENCES [AppraisalFactor](AppraisalFactorId)
		)

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.TierFactor table already created.  Skipped.')

GO

/*-------------------------------------------------------
	Create & Populate the DatabaseVersion table
-------------------------------------------------------*/
IF OBJECT_ID('DatabaseVersion', 'U') IS NULL
BEGIN
	PRINT ('Creating CreditRequestToolkit.dbo.DatabaseVersion table.')
	BEGIN TRY
		BEGIN TRANSACTION

		CREATE TABLE [CreditRequestToolkit].[dbo].[DatabaseVersion](
			[MajorVersion] [int]		NOT NULL	CONSTRAINT [DF_DatabaseVersion_MajorVersion] DEFAULT (0),
			[MinorVersion] [int]		NOT NULL	CONSTRAINT [DF_DatabaseVersion_MinorVersion] DEFAULT (0),
			[BuildVersion] [int]		NOT NULL	CONSTRAINT [DF_DatabaseVersion_BuildVersion] DEFAULT (0),
			[RevisionVersion] [int]		NOT NULL	CONSTRAINT [DF_DatabaseVersion_RevisionVersion] DEFAULT (0),
			[InstallDate] [datetime]	NOT NULL,
			[Comments] [text]			NOT NULL,
			CONSTRAINT [PK_DatabaseVersion] PRIMARY KEY CLUSTERED (
				[MajorVersion] ASC,
				[MinorVersion] ASC,
				[BuildVersion] ASC,
				[RevisionVersion] ASC
			) WITH (
				PAD_INDEX  = OFF,
				STATISTICS_NORECOMPUTE  = OFF,
				IGNORE_DUP_KEY = OFF,
				ALLOW_ROW_LOCKS  = ON,
				ALLOW_PAGE_LOCKS  = ON,
				FILLFACTOR = 90
			) ON [PRIMARY]
		) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

		INSERT INTO [CreditRequestToolkit].[dbo].[DatabaseVersion]
		VALUES (2017, 10, 0, 0, GETDATE(), 'Initial Install')

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		PRINT (ERROR_MESSAGE())
		ROLLBACK TRANSACTION
		SET NOEXEC ON
	END CATCH
END
ELSE
	PRINT ('CreditRequestToolkit.dbo.DatabaseVersion table already created.  Skipped.')

GO

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
PRINT ('CreditRequestToolkit Database successfully created.')