USE CreditRequestToolkit
GO

/*=====================================
	Delinquency
=====================================*/

IF ((SELECT COUNT(*) FROM Delinquency) <= 0)
BEGIN
	PRINT ('Populating Delinquency')

	BEGIN TRAN

	INSERT INTO Delinquency
	VALUES ('Not Delinquent', 0.00),
		   ('1 X 30 Days', 0.50),
		   ('2 X 30 Days', 0.75),
		   ('3 X 30 Days', 1.00),
		   ('1 X 60 Days', 1.00),
		   ('2 X 60 Days', 1.50),
		   ('3 X 60 Days', 2.00)

	COMMIT TRAN
END
ELSE
	PRINT ('Delinquency already populated. Skipped.')

/*=====================================
	Industry (and underlying tables)
=====================================*/

IF ((SELECT COUNT(*) FROM CDRCPercent) <= 0)
BEGIN
	PRINT ('Populating CDRCPercent')

	BEGIN TRAN

	INSERT INTO CDRCPercent
	(PDRating1, PDRating2, PDRating3, PDRating4, PDRating5, PDRating6, PDRating7, PDRating8, PDRating9, PDRating10, PDRating11, PDRating12, PDRating13, PDRating14)
	VALUES
	(  NULL,   NULL,   NULL, 150.00, 140.00, 125.00, 120.00, 115.00, 105.00, 100.00,  90.00,   0.00,   NULL,   NULL), -- Beef Feedlot
	(  NULL,   NULL,   NULL, 150.00, 140.00, 125.00, 120.00, 115.00, 105.00, 100.00,  90.00,   0.00,   NULL,   NULL), -- Cash Crop
	(  NULL,   NULL,   NULL, 150.00, 140.00, 125.00, 115.00, 110.00, 105.00, 100.00,  90.00,   0.00,   NULL,   NULL), -- Contract Pultry/Swine
	(  NULL,   NULL,   NULL, 160.00, 140.00, 125.00, 120.00, 110.00, 105.00, 100.00,  90.00,   0.00,   NULL,   NULL), -- Cow Calf
	(  NULL,   NULL,   NULL, 150.00, 140.00, 130.00, 120.00, 115.00, 105.00, 100.00,  90.00,   0.00,   NULL,   NULL), -- Dairy
	(  NULL,   NULL,   NULL, 150.00, 140.00, 125.00, 115.00, 110.00, 105.00, 100.00,  90.00,   0.00,   NULL,   NULL), -- Fruit
	(  NULL,   NULL,   NULL, 150.00, 140.00, 125.00, 115.00, 110.00, 105.00, 100.00,  90.00,   0.00,   NULL,   NULL), -- General Farm
	(  NULL,   NULL,   NULL, 150.00, 140.00, 125.00, 115.00, 110.00, 105.00, 100.00,  90.00,   0.00,   NULL,   NULL), -- GreenHouse
	(  NULL,   NULL,   NULL, 175.00, 160.00, 145.00, 130.00, 115.00, 110.00, 105.00,  90.00,   0.00,   NULL,   NULL), -- Large Dairy
	(  NULL,   NULL,   NULL, 175.00, 140.00, 130.00, 115.00, 110.00, 105.00, 100.00,  90.00,   0.00,   NULL,   NULL), -- PT Farmer/Secondary Owner
	(  NULL,   NULL,   NULL, 150.00, 140.00, 125.00, 115.00, 110.00, 105.00, 100.00,  90.00,   0.00,   NULL,   NULL), -- Swine Independent
	(  NULL,   NULL,   NULL, 150.00, 135.00, 125.00, 115.00, 110.00, 105.00, 100.00,  90.00,   0.00,   NULL,   NULL), -- Timber
	(  NULL,   NULL,   NULL, 150.00, 140.00, 125.00, 115.00, 110.00, 105.00, 100.00,  90.00,   0.00,   NULL,   NULL)  -- Vegetables

	COMMIT TRAN
END
ELSE
	PRINT ('CDRCPercent already populated. Skipped.')

IF ((SELECT COUNT(*) FROM CDRCMarginPercent) <= 0)
BEGIN
	PRINT ('Populating CDRCMarginPercent')

	BEGIN TRAN

	INSERT INTO CDRCMarginPercent
	(PDRating1, PDRating2, PDRating3, PDRating4, PDRating5, PDRating6, PDRating7, PDRating8, PDRating9, PDRating10, PDRating11, PDRating12, PDRating13, PDRating14)
	VALUES
	(  NULL,   NULL,   NULL,  15.00,  12.00,  10.00,   8.00,   5.00,   3.00,   1.00,   1.00, -50.00,   NULL,   NULL), -- Beef Feedlot
	(  NULL,   NULL,   NULL,  15.00,  12.50,  10.00,   7.50,   5.00,   0.00,  -5.00,  -5.00, -50.00,   NULL,   NULL), -- Cash Crop
	(  NULL,   NULL,   NULL,  25.00,  20.00,  17.00,  15.00,  12.00,  10.00,   5.00,   5.00, -50.00,   NULL,   NULL), -- Contract Pultry/Swine
	(  NULL,   NULL,   NULL,  20.00,  15.00,  10.00,   8.00,   5.00,   3.00,   0.00,   0.00, -50.00,   NULL,   NULL), -- Cow Calf
	(  NULL,   NULL,   NULL,  20.00,  15.00,  10.00,   8.00,   5.00,   3.00,  -5.00,  -5.00, -50.00,   NULL,   NULL), -- Dairy
	(  NULL,   NULL,   NULL,  15.00,  12.50,  10.00,   7.50,   5.00,   0.00,  -5.00,  -5.00, -50.00,   NULL,   NULL), -- Fruit
	(  NULL,   NULL,   NULL,  15.00,  12.50,  10.00,   7.50,   5.00,   0.00,  -5.00,  -5.00, -50.00,   NULL,   NULL), -- General Farm
	(  NULL,   NULL,   NULL,  20.00,  15.00,  10.00,   8.00,   6.00,   4.00,   2.00,   2.00, -50.00,   NULL,   NULL), -- GreenHouse
	(  NULL,   NULL,   NULL,  20.00,  15.00,  10.00,   8.00,   6.00,   4.00,   2.00,   2.00, -50.00,   NULL,   NULL), -- Large Dairy
	(  NULL,   NULL,   NULL,  20.00,  15.00,  12.50,   5.00,   2.50,   0.00,  -5.00,  -5.00, -50.00,   NULL,   NULL), -- PT Farmer/Secondary Owner
	(  NULL,   NULL,   NULL,  20.00,  15.00,  10.00,   5.00,   3.00,   2.00,   0.00,   0.00, -50.00,   NULL,   NULL), -- Swine Independent
	(  NULL,   NULL,   NULL,  15.00,  12.00,  10.00,   8.00,   6.00,   4.00,   2.00,   2.00, -50.00,   NULL,   NULL), -- Timber
	(  NULL,   NULL,   NULL,  15.00,  12.50,  10.00,   7.50,   5.00,   0.00,  -5.00,  -5.00, -50.00,   NULL,   NULL)  -- Vegetables

	COMMIT TRAN
END
ELSE
	PRINT ('CDRCMarginPercent already populated. Skipped.')

IF ((SELECT COUNT(*) FROM CurrentRatio) <= 0)
BEGIN
	PRINT ('Populating CurrentRatio')

	BEGIN TRAN

	INSERT INTO CurrentRatio
	(PDRating1, PDRating2, PDRating3, PDRating4, PDRating5, PDRating6, PDRating7, PDRating8, PDRating9, PDRating10, PDRating11, PDRating12, PDRating13, PDRating14)
	VALUES
	(  NULL,   NULL,   NULL,   3.00,   2.00,   1.50,   1.25,   1.10,   1.00,   0.80,   0.50,   0.00,   NULL,   NULL), -- Beef Feedlot
	(  NULL,   NULL,   NULL,   2.50,   2.00,   1.75,   1.50,   1.25,   1.10,   0.80,   0.50,   0.00,   NULL,   NULL), -- Cash Crop
	(  NULL,   NULL,   NULL,   1.60,   1.50,   1.40,   1.30,   1.20,   1.10,   1.00,   0.50,   0.00,   NULL,   NULL), -- Contract Pultry/Swine
	(  NULL,   NULL,   NULL,   2.00,   1.75,   1.50,   1.25,   1.15,   1.05,   1.00,   0.70,   0.00,   NULL,   NULL), -- Cow Calf
	(  NULL,   NULL,   NULL,   1.65,   1.50,   1.35,   1.20,   1.05,   1.00,   0.95,   0.80,   0.00,   NULL,   NULL), -- Dairy
	(  NULL,   NULL,   NULL,   2.50,   1.75,   1.25,   1.00,   0.80,   0.50,   0.30,   0.10,   0.00,   NULL,   NULL), -- Fruit
	(  NULL,   NULL,   NULL,   2.50,   2.00,   1.75,   1.50,   1.25,   1.10,   0.80,   0.50,   0.00,   NULL,   NULL), -- General Farm
	(  NULL,   NULL,   NULL,   1.75,   1.50,   1.25,   1.10,   1.05,   1.00,   0.90,   0.75,   0.00,   NULL,   NULL), -- GreenHouse
	(  NULL,   NULL,   NULL,   1.75,   1.50,   1.25,   1.10,   1.05,   1.00,   0.90,   0.75,   0.00,   NULL,   NULL), -- Large Dairy
	(  NULL,   NULL,   NULL,   2.00,   1.50,   1.25,   1.00,   0.80,   0.70,   0.60,   0.50,   0.00,   NULL,   NULL), -- PT Farmer/Secondary Owner
	(  NULL,   NULL,   NULL,   3.00,   2.50,   2.00,   1.50,   1.30,   1.10,   0.90,   0.50,   0.00,   NULL,   NULL), -- Swine Independent
	(  NULL,   NULL,   NULL,   1.50,   1.40,   1.30,   1.20,   1.10,   1.00,   0.90,   0.50,   0.00,   NULL,   NULL), -- Timber
	(  NULL,   NULL,   NULL,   2.50,   2.00,   1.75,   1.50,   1.25,   1.10,   0.80,   0.50,   0.00,   NULL,   NULL)  -- Vegetables

	COMMIT TRAN
END
ELSE
	PRINT ('CurrentRatio already populated. Skipped.')

IF ((SELECT COUNT(*) FROM OEPercent) <= 0)
BEGIN
	PRINT ('Populating OEPercent')

	BEGIN TRAN

	INSERT INTO OEPercent
	(PDRating1, PDRating2, PDRating3, PDRating4, PDRating5, PDRating6, PDRating7, PDRating8, PDRating9, PDRating10, PDRating11, PDRating12, PDRating13, PDRating14)
	VALUES
	(  NULL,   NULL,   NULL,  70.00,  60.00,  55.00,  50.00,  45.00,  40.00,  35.00,  30.00,   0.00,   NULL,   NULL), -- Beef Feedlot
	(  NULL,   NULL,   NULL,  80.00,  75.00,  65.00,  55.00,  45.00,  40.00,  35.00,  30.00,   0.00,   NULL,   NULL), -- Cash Crop
	(  NULL,   NULL,   NULL,  60.00,  50.00,  45.00,  40.00,  35.00,  25.00,  20.00,  15.00,   0.00,   NULL,   NULL), -- Contract Pultry/Swine
	(  NULL,   NULL,   NULL,  80.00,  75.00,  70.00,  65.00,  50.00,  45.00,  40.00,  30.00,   0.00,   NULL,   NULL), -- Cow Calf
	(  NULL,   NULL,   NULL,  75.00,  65.00,  55.00,  50.00,  45.00,  40.00,  30.00,  20.00,   0.00,   NULL,   NULL), -- Dairy
	(  NULL,   NULL,   NULL,  80.00,  75.00,  65.00,  55.00,  50.00,  45.00,  40.00,  30.00,   0.00,   NULL,   NULL), -- Fruit
	(  NULL,   NULL,   NULL,  80.00,  75.00,  65.00,  55.00,  50.00,  45.00,  40.00,  30.00,   0.00,   NULL,   NULL), -- General Farm
	(  NULL,   NULL,   NULL,  70.00,  65.00,  55.00,  50.00,  45.00,  40.00,  30.00,  25.00,   0.00,   NULL,   NULL), -- GreenHouse
	(  NULL,   NULL,   NULL,  65.00,  55.00,  50.00,  45.00,  40.00,  35.00,  30.00,  20.00,   0.00,   NULL,   NULL), -- Large Dairy
	(  NULL,   NULL,   NULL,  55.00,  50.00,  45.00,  40.00,  35.00,  30.00,  25.00,  20.00,   0.00,   NULL,   NULL), -- PT Farmer/Secondary Owner
	(  NULL,   NULL,   NULL,  80.00,  75.00,  70.00,  60.00,  50.00,  40.00,  30.00,  25.00,   0.00,   NULL,   NULL), -- Swine Independent
	(  NULL,   NULL,   NULL,  70.00,  65.00,  55.00,  50.00,  45.00,  40.00,  30.00,  25.00,   0.00,   NULL,   NULL), -- Timber
	(  NULL,   NULL,   NULL,  80.00,  75.00,  65.00,  55.00,  50.00,  45.00,  40.00,  30.00,   0.00,   NULL,   NULL)  -- Vegetables

	COMMIT TRAN
END
ELSE
	PRINT ('OEPercent already populated. Skipped.')

IF ((SELECT COUNT(*) FROM ROAPercent) <= 0)
BEGIN
	PRINT ('Populating ROAPercent')

	BEGIN TRAN

	INSERT INTO ROAPercent
	(PDRating1, PDRating2, PDRating3, PDRating4, PDRating5, PDRating6, PDRating7, PDRating8, PDRating9, PDRating10, PDRating11, PDRating12, PDRating13, PDRating14)
	VALUES
	(  NULL,   NULL,   NULL,  10.00,   8.00,   7.00,   6.00,   4.00,   2.00,   1.00,  -2.00,-100.00,   NULL,   NULL), -- Beef Feedlot
	(  NULL,   NULL,   NULL,   9.00,   7.00,   6.00,   5.00,   3.00,   2.00,   1.00,  -2.00,-100.00,   NULL,   NULL), -- Cash Crop
	(  NULL,   NULL,   NULL,  14.00,  12.00,  10.00,   8.00,   6.00,   4.00,   2.00,   0.00,-100.00,   NULL,   NULL), -- Contract Pultry/Swine
	(  NULL,   NULL,   NULL,   7.00,   5.00,   4.00,   3.00,   2.00,   1.00,   0.00,  -1.00,-100.00,   NULL,   NULL), -- Cow Calf
	(  NULL,   NULL,   NULL,  11.00,   9.00,   7.00,   6.00,   4.00,   3.00,   0.00,   0.00,-100.00,   NULL,   NULL), -- Dairy
	(  NULL,   NULL,   NULL,   9.00,   7.00,   6.00,   5.00,   3.00,   2.00,   1.00,  -2.00,-100.00,   NULL,   NULL), -- Fruit
	(  NULL,   NULL,   NULL,   9.00,   7.00,   6.00,   5.00,   3.00,   2.00,   1.00,  -2.00,-100.00,   NULL,   NULL), -- General Farm
	(  NULL,   NULL,   NULL,  15.00,  12.00,   9.00,   7.00,   4.00,   3.00,   2.00,   0.00,-100.00,   NULL,   NULL), -- GreenHouse
	(  NULL,   NULL,   NULL,  15.00,  12.00,   9.00,   7.00,   4.00,   3.00,   2.00,   0.00,-100.00,   NULL,   NULL), -- Large Dairy
	(  NULL,   NULL,   NULL,  15.00,  12.00,   9.00,   7.00,   4.00,   3.00,   2.00,   0.00,-100.00,   NULL,   NULL), -- PT Farmer/Secondary Owner
	(  NULL,   NULL,   NULL,  13.00,  11.00,   9.00,   7.00,   5.00,   3.00,   2.00,   0.00,-100.00,   NULL,   NULL), -- Swine Independent
	(  NULL,   NULL,   NULL,   9.00,   7.00,   6.00,   5.00,   3.00,   2.00,   1.00,  -2.00,-100.00,   NULL,   NULL), -- Timber
	(  NULL,   NULL,   NULL,   9.00,   7.00,   6.00,   5.00,   3.00,   2.00,   1.00,  -2.00,-100.00,   NULL,   NULL)  -- Vegetables

	COMMIT TRAN
END
ELSE
	PRINT ('ROAPercent already populated. Skipped.')

IF ((SELECT COUNT(*) FROM WCAGIPercent) <= 0)
BEGIN
	PRINT ('Populating WCAGIPercent')

	BEGIN TRAN

	INSERT INTO WCAGIPercent
	(PDRating1, PDRating2, PDRating3, PDRating4, PDRating5, PDRating6, PDRating7, PDRating8, PDRating9, PDRating10, PDRating11, PDRating12, PDRating13, PDRating14)
	VALUES
	(  NULL,   NULL,   NULL,  50.00,  25.00,  20.00,  15.00,  10.00,   5.00,  -5.00, -25.00,-100.00,   NULL,   NULL), -- Beef Feedlot
	(  NULL,   NULL,   NULL,  40.00,  35.00,  25.00,  20.00,  15.00,   5.00,  -5.00, -25.00,-100.00,   NULL,   NULL), -- Cash Crop
	(  NULL,   NULL,   NULL,   NULL,   NULL,   NULL,   NULL,   NULL,   NULL,   NULL,   NULL,   NULL,   NULL,   NULL), -- Contract Pultry/Swine
	(  NULL,   NULL,   NULL,  30.00,  25.00,  20.00,  15.00,  10.00,   5.00,   0.00, -10.00,-100.00,   NULL,   NULL), -- Cow Calf
	(  NULL,   NULL,   NULL,  24.00,  20.00,  16.00,  12.00,   8.00,   4.00,   0.00,  -4.00,-100.00,   NULL,   NULL), -- Dairy
	(  NULL,   NULL,   NULL,  40.00,  35.00,  25.00,  20.00,  15.00,   5.00,  -5.00, -25.00,-100.00,   NULL,   NULL), -- Fruit
	(  NULL,   NULL,   NULL,  40.00,  35.00,  25.00,  20.00,  15.00,   5.00,  -5.00, -25.00,-100.00,   NULL,   NULL), -- General Farm
	(  NULL,   NULL,   NULL,  30.00,  20.00,  10.00,   5.00,   2.00,   0.00,  -3.00, -10.00,-100.00,   NULL,   NULL), -- GreenHouse
	(  NULL,   NULL,   NULL,  25.00,  20.00,  15.00,  10.00,   5.00,   0.00,  -3.00, -10.00,-100.00,   NULL,   NULL), -- Large Dairy
	(  NULL,   NULL,   NULL,  25.00,  20.00,  15.00,  10.00,   5.00,   0.00,  -3.00, -10.00,-100.00,   NULL,   NULL), -- PT Farmer/Secondary Owner
	(  NULL,   NULL,   NULL,  30.00,  25.00,  20.00,  15.00,  10.00,   5.00,   0.00, -10.00,-100.00,   NULL,   NULL), -- Swine Independent
	(  NULL,   NULL,   NULL,  15.00,  10.00,   5.00,   3.00,   2.00,   0.00,  -5.00, -25.00,-100.00,   NULL,   NULL), -- Timber
	(  NULL,   NULL,   NULL,  40.00,  35.00,  25.00,  20.00,  15.00,   5.00,  -5.00, -25.00,-100.00,   NULL,   NULL)  -- Vegetables

	COMMIT TRAN
END
ELSE
	PRINT ('WCAGIPercent already populated. Skipped.')

IF ((SELECT COUNT(*) FROM PDRatingRatios) <= 0)
BEGIN
	PRINT ('Populating PDRatingRatios')

	BEGIN TRAN

	INSERT INTO PDRatingRatios
	(CDRCPercentId, CDRCMarginPercentId, CurrentRatioId, OEPercentId, ROAPercentId, WCAGIPercentId,
	CDRCWeightingPercent, CDRCMarginWeightingPercent, CurrentRatioWeightingPercent, OEWeightingPercent, ROAWeightingPercent, WCAGIWeightingPercent, ManagementWeightingPercent)
	VALUES
	(1, 1, 1, 1, 1, 1,			35.0,  5.0, 10.0, 35.0,  5.0, 10.0,  0.0), -- Beef Feedlot
	(2, 2, 2, 2, 2, 2,			35.0,  5.0, 10.0, 35.0,  5.0, 10.0,  0.0), -- Cash Crop
	(3, 3, 3, 3, 3, 3,			40.0,  5.0, 10.0, 40.0,  5.0,  0.0,  0.0), -- Contract Pultry/Swine
	(4, 4, 4, 4, 4, 4,			35.0,  5.0, 10.0, 35.0,  5.0, 10.0,  0.0), -- Cow Calf
	(5, 5, 5, 5, 5, 5,			35.0,  5.0, 10.0, 35.0,  5.0, 10.0,  0.0), -- Dairy
	(6, 6, 6, 6, 6, 6,			35.0,  5.0, 10.0, 35.0,  5.0, 10.0,  0.0), -- Fruit
	(7, 7, 7, 7, 7, 7,			35.0,  5.0, 10.0, 35.0,  5.0, 10.0,  0.0), -- General Farm
	(8, 8, 8, 8, 8, 8,			35.0,  5.0, 10.0, 35.0,  5.0, 10.0,  0.0), -- GreenHouse
	(9, 9, 9, 9, 9, 9,			35.0,  5.0, 10.0, 35.0,  5.0, 10.0,  0.0), -- Large Dairy
	(10, 10, 10, 10, 10, 10,	45.0,  5.0, 10.0, 40.0,  0.0,  0.0,  0.0), -- PT Farmer/Secondary Owner
	(11, 11, 11, 11, 11, 11,	35.0,  5.0, 10.0, 35.0,  5.0, 10.0,  0.0), -- Swine Independent
	(12, 12, 12, 12, 12, 12,	35.0,  5.0, 10.0, 35.0,  5.0, 10.0,  0.0), -- Timber
	(13, 13, 13, 13, 13, 13,	35.0,  5.0, 10.0, 35.0,  5.0, 10.0,  0.0)  -- Vegetables

	COMMIT TRAN
END
ELSE
	PRINT ('PDRatingRatios already populated. Skipped.')

IF ((SELECT COUNT(*) FROM Industry) <= 0)
BEGIN
	PRINT ('Populating Industry')

	BEGIN TRAN

	INSERT INTO Industry
	VALUES
	(1, 'Beef Feedlot'),
	(2, 'Cash Crop'),
	(3, 'Contract Poultry/Swine'),
	(4, 'Cow Calf'),
	(5, 'Dairy'),
	(6, 'Fruit'),
	(7, 'General Farm'),
	(8, 'GreenHouse'),
	(9, 'Large Dairy'),
	(10, 'PT Farmer/Secondary Owner'),
	(11, 'Swine Independent'),
	(12, 'Timber'),
	(13, 'Vegetables')

	COMMIT TRAN
END
ELSE
	PRINT ('Industry already populated. Skipped.')