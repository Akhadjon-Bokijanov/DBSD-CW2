CREATE TRIGGER trIsertItemAudit ON Item
	AFTER INSERT NOT FOR REPLICATION
AS 
BEGIN

	DECLARE @ItemId BIGINT

	SELECT @ItemId = ItemId FROM Inserted

	INSERT INTO ItemAudit (
		[ItemId],
		[LocalName],
		[GlobalName],
		[ItemUID],
		[IsEchangeble],
		[Image],
		[MadeOf],
		[UsageStartedAt],
		[SupplierId],  
		[UnitId],
		[StoreId],
		[Operation]
	)
	SELECT [ItemId],
		[LocalName],
		[GlobalName],
		[ItemUID],
		[IsEchangeble],
		[Image],
		[MadeOf],
		[UsageStartedAt],
		[SupplierId],  
		[UnitId],
		[StoreId],
		'INSERTED'
	FROM Item WHERE ItemId = @ItemId

END

CREATE TRIGGER trUpdateItemAudit ON Item
	AFTER UPDATE NOT FOR REPLICATION
AS 
BEGIN

	DECLARE @ItemId BIGINT

	SELECT @ItemId = ItemId FROM Inserted

	INSERT INTO ItemAudit (
		[ItemId],
		[LocalName],
		[GlobalName],
		[ItemUID],
		[IsEchangeble],
		[Image],
		[MadeOf],
		[UsageStartedAt],
		[SupplierId],  
		[UnitId],
		[StoreId],
		[Operation]
	)
	SELECT [ItemId],
		[LocalName],
		[GlobalName],
		[ItemUID],
		[IsEchangeble],
		[Image],
		[MadeOf],
		[UsageStartedAt],
		[SupplierId],  
		[UnitId],
		[StoreId],
		'UPDATED'
	FROM Item WHERE ItemId = @ItemId

END

CREATE TRIGGER trDissallowOnModification ON Item
FOR UPDATE, DELETE
AS
BEGIN
	IF EXISTS (SELECT date FROM disallowed_dates WHERE DATEDIFF(day, date, GETDATE()) = 0)
	BEGIN
		RAISERROR('DATA MODIFICATION IS NOT ALLOWED TODAY', 16, 1)
		ROLLBACK TRANSACTION;
	END
END

CREATE TRIGGER trValidationOfUsageStartedAt ON Item
FOR UPDATE, INSERT
AS BEGIN
	DECLARE @ItemUsageStart DATETIME
	SELECT @ItemUsageStart = UsageStartedAt FROM Inserted

	IF DATEDIFF(day, @ItemUsageStart, GETDATE())>31
	BEGIN
	RAISERROR('Item usage start date must not be 31 days from today', 16, 1)
	ROLLBACK TRANSACTION;
	END
	
END

CREATE TRIGGER trValidationItemAudit ON ItemAudit
INSTEAD OF UPDATE, DELETE
AS BEGIN
	RAISERROR('YOU CANNOT EDIT OR DELETE FROM ItemAudit table', 16, 1)
END

