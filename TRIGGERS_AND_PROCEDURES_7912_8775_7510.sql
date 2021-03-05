--TRIGGERS AND PROCEDURES
use [DBSD_CW2_7510_8775_7912] EXEC sp_changedbowner 'sa';

--CREATE ITEM PROCEDURE
CREATE PROCEDURE SP_Item_Add(
	@GlobalName varchar(500),
	@LocalName varchar(500),
	@UnitId int,
	@StoreId int,
	@MadeOf varchar(500),
	@SupplierId int,
	@UsageStartedAt datetime,
	@Image varbinary(max),
	@IsEchangeble bit,
	@ItemUID int
) 
AS 
	BEGIN
		INSERT INTO Item(
			GlobalName,
			LocalName,
			UnitId,
			StoreId,
			MadeOf,
			SupplierId,
			UsageStartedAt,
			Image,
			IsEchangeble,
			ItemUID
		) VALUES(
			@GlobalName,
			@LocalName,
			@UnitId,
			@StoreId,
			@MadeOf,
			@SupplierId,
			@UsageStartedAt,
			@Image,
			@IsEchangeble,
			@ItemUID
		)
	END
--END OF CREATE ITEM PROCEDURE

Exec SP_ITEM_ALL_SELECT @SortIndex=5, @SortCase='DESC'


--DROP SP_ITEM_ALL_SELECT
-- --DROP PROCEDURE SP_ITEM_ALL_SELECT;

--CREATE PROCUDERE FOR SELECTING ITEMS
--JOINING 5 TABLES
--ALLOWING PAGINATION
--ALLOWING SORTING BY 5 COLUMNS
--FILTER BY 5 FIELDS
CREATE PROCEDURE SP_ITEM_ALL_SELECT(
	@PageNumber int = 1,
	@PageSize int = 5,
	--SORT 
	@SortIndex int=5,
	@SortCase varchar(5)='ASC',
	-- 1 @SortIsExchangable varchar(5),
	-- 2 @SortUsageStartedAt varchar(5),
	-- 3 @SortSupplierName varchar(5),
	-- 4 @SortStoreName varchar(5),
	-- 5 @SortItemUID varchar(5),
	--SEARCH
	@LocalName varchar(500)='',
	@GlobalName varchar(500)='',
	@ItemUID varchar(50)='',
	@MadeOf varchar(500)='',
	@SupplierName varchar(500)='',
	@StoreName varchar(500)=''
)
AS
	BEGIN
	
	SELECT 
		i.*, 
		st.Name as "StoreName", 
		sup.Name as "SupplierName",
		u.Name as UnitName,
		COUNT(trl.ItemTransactionListId) OVER(PARTITION BY i.ItemId) as "TransactionCount",
		COUNT(ipc.ChildId) OVER (PARTITION BY i.ItemId) as "NumParent",
		AVG(ipc.ChildAmount) OVER (PARTITION BY i.ItemId) as "AvgAmountUsagePerParent"
	FROM Item i
	INNER JOIN Unit u ON i.UnitId = u.UnitId
	INNER JOIN Store st ON i.StoreId = st.StoreId
	INNER JOIN Supplier sup ON i.SupplierId = sup.SupplierId
	--EVEN IF THERE IS NOT TRANSACTIONS OR PARENT CHILD BINDED Item Should be Selected
	LEFT JOIN ItemTransactionList trl ON trl.ItemId = i.ItemId
	LEFT JOIN ItemParentChildBind ipc ON ipc.ChildId = i.ItemId 

	WHERE 
		LocalName LIKE '%'+@LocalName+'%' AND
		GlobalName LIKE '%'+@GlobalName+'%' AND
		ItemUID LIKE '%'+@ItemUID+'%' AND
		MadeOf LIKE '%'+@MadeOf+'%' AND
		sup.Name LIKE '%'+@SupplierName+'%' AND 
		st.Name LIKE '%'+@StoreName+'%'

	ORDER BY 
		CASE WHEN @SortIndex=1 AND @SortCase='DESC' THEN IsEchangeble END DESC,
		CASE WHEN @SortIndex=2 AND @SortCase='DESC' THEN UsageStartedAt END DESC,
		CASE WHEN @SortIndex=3 AND @SortCase='DESC' THEN sup.Name END DESC,
		CASE WHEN @SortIndex=4 AND @SortCase='DESC' THEN st.Name END DESC,
		CASE WHEN @SortIndex=5 AND @SortCase='DESC' THEN ItemUID END DESC,

		CASE WHEN @SortIndex=1 AND @SortCase='ASC' THEN IsEchangeble END,
		CASE WHEN @SortIndex=2 AND @SortCase='ASC' THEN UsageStartedAt END,
		CASE WHEN @SortIndex=3 AND @SortCase='ASC' THEN sup.Name END,
		CASE WHEN @SortIndex=4 AND @SortCase='ASC' THEN st.Name END,
		CASE WHEN @SortIndex=5 AND @SortCase='ASC' THEN ItemUID END
	
	END


---### ROLE procedures START ###---
--DROP SP_Role_Add PROCEDURE
-- --DROP PROCEDURE SP_Role_Add;

--CREATE PROCEDURE FOR INSERTING ROLE
CREATE PROCEDURE SP_Role_Add(@Name varchar(500))
AS 
	BEGIN 
		INSERT INTO Role (Name) VALUES(@Name);
	END

--DROP SELECT one ROLE PROCEDURE
-- --DROP PROCEDURE SP_Role_One_Select;
--SELECT ROLE PROCUDURE 
CREATE PROCEDURE SP_Role_One_Select(@RoleId int)
AS 
	BEGIN 
		SELECT * FROM Role WHERE RoleId=@RoleId;
	END

--DROP Delete one ROLE PROCEDURE
-- --DROP PROCEDURE SP_Role_Delete;
--Delete ROLE PROCUDURE 
CREATE PROCEDURE SP_Role_Delete(@RoleId int)
AS 
	BEGIN 
		DELETE FROM Role WHERE RoleId=@RoleId
	END

--DROP Delete one ROLE PROCEDURE
-- --DROP PROCEDURE SP_Role_Update;
--Delete ROLE PROCUDURE 
CREATE PROCEDURE SP_Role_Update(@RoleId int, @Name varchar(500))
AS 
	BEGIN 
		UPDATE Role SET Name=@Name WHERE RoleId=@RoleId
	END
	
--DROP SELECT ALL ROLE PROCEDURE
-- --DROP PROCEDURE SP_Role_All_Select;
--SELECT ROLES PROCUDURE 
CREATE PROCEDURE SP_Role_All_Select
AS 
	BEGIN 
		SELECT * FROM Role;
	END

exec SP_Role_All_Select;
---### ROLE procedures END ###---

--##################################

---### UNIT procedures START ###---
--DROP SP_Unir_Add PROCEDURE
-- --DROP PROCEDURE SP_Unit_Add;

--CREATE PROCEDURE FOR INSERTING Unir
CREATE PROCEDURE SP_Unit_Add(@Name varchar(500), @ShortName varchar(300))
AS 
	BEGIN 
		INSERT INTO Unit (Name, ShortName) VALUES(@Name, @ShortName);
	END

--DROP SELECT one Unir PROCEDURE
-- --DROP PROCEDURE SP_Unit_One_Select;
--SELECT Unir PROCUDURE 
CREATE PROCEDURE SP_Unit_One_Select(@UnitId int)
AS 
	BEGIN 
		SELECT * FROM Unit WHERE UnitId=@UnitId;
	END

--DROP Delete one Unit PROCEDURE
-- --DROP PROCEDURE SP_Unit_Delete;
--Delete Unit PROCUDURE 
CREATE PROCEDURE SP_Unit_Delete(@UnitId int)
AS 
	BEGIN 
		DELETE FROM Unit WHERE UnitId=@UnitId
	END

--DROP Delete one Unit PROCEDURE
-- --DROP PROCEDURE SP_Unit_Update;
--Delete Unit PROCUDURE 
CREATE PROCEDURE SP_Unit_Update(@UnitId int, @Name varchar(500), @ShortName varchar(500))
AS 
	BEGIN 
		UPDATE Unit SET Name=@Name, ShortName=@ShortName WHERE UnitId=@UnitId
	END
	
--DROP SELECT ALL Unit PROCEDURE
-- --DROP PROCEDURE SP_Unit_All_Select;
--SELECT UnitS PROCUDURE 
CREATE PROCEDURE SP_Unit_All_Select
AS 
	BEGIN 
		SELECT * FROM Unit;
	END

exec SP_Unit_All_Select;
--### Unit procedure END ### ---

---##############################---

---### STORE procedures START ###---
--DROP SP_Store_Add PROCEDURE
 --DROP PROCEDURE SP_Store_Add;

--CREATE PROCEDURE FOR INSERTING Unit
CREATE PROCEDURE SP_Store_Add(@Name varchar(500), @Square float, @EmployeeId int)
AS 
	BEGIN 
		INSERT INTO Store (Name, Square, EmployeeId) VALUES(@Name, @Square, @EmployeeId);
	END

--DROP SELECT one Unir PROCEDURE
 --DROP PROCEDURE SP_Store_One_Select;
--SELECT Unir PROCUDURE 
CREATE PROCEDURE SP_Store_One_Select(@StoreId int)
AS 
	BEGIN 
		SELECT s.*, e.FullName as EmployeeName FROM Store s 
		INNER JOIN Employee e ON e.EmployeeId = s.EmployeeId
		WHERE s.StoreId=@StoreId;
	END

--DROP Delete one Store PROCEDURE
 --DROP PROCEDURE SP_Store_Delete;
--Delete Store PROCUDURE 
CREATE PROCEDURE SP_Store_Delete(@StoreId int)
AS 
	BEGIN 
		DELETE FROM Store WHERE StoreId=@StoreId
	END

--DROP Delete one Store PROCEDURE
 --DROP PROCEDURE SP_Store_Update;
--Delete Store PROCUDURE 
CREATE PROCEDURE SP_Store_Update(@StoreId int, @Name varchar(500), @Square float, @EmployeeId int)
AS 
	BEGIN 
		UPDATE Store SET Name=@Name, Square=@Square, @EmployeeId=@EmployeeId WHERE StoreId=@StoreId
	END
	
--DROP SELECT ALL Store PROCEDURE
 --DROP PROCEDURE SP_Store_All_Select;
--SELECT StoreS PROCUDURE 
CREATE PROCEDURE SP_Store_All_Select
AS 
	BEGIN 
		SELECT s.*, e.FullName as EmployeeName FROM Store s 
		INNER JOIN Employee e ON e.EmployeeId = s.EmployeeId
	END

exec SP_Store_All_Select;
--### STORE END PROCEDURES ###---

--##################################

---### Employee procedures START ###---
--DROP SP_Employee_Add PROCEDURE
 --DROP PROCEDURE SP_Employee_Add;

--CREATE PROCEDURE FOR INSERTING Employee
CREATE PROCEDURE SP_Employee_Add(
	@FullName varchar(500),
	@RoleId int,
	@DoB datetime,
	@Salary float,
	@Password varchar(500),
	@Address varchar(100)
	)
AS 
	BEGIN 
		INSERT INTO Employee (
			FullName,
			RoleId,
			DoB,
			Salary,
			Password,
			Address
		) VALUES(
			@FullName,
			@RoleId,
			@DoB,
			@Salary,
			@Password,
			@Address
		);
	END

--DROP SELECT one Employee PROCEDURE
 --DROP PROCEDURE SP_Employee_One_Select;
--SELECT Employee PROCUDURE 
CREATE PROCEDURE SP_Employee_One_Select(@EmployeeId int)
AS 
	BEGIN 
		SELECT e.*, r.Name as Role FROM Employee e 
		INNER JOIN Role r ON r.RoleId = e.RoleId
		WHERE EmployeeId=@EmployeeId;
	END

EXEC SP_Employee_One_Select @EmployeeId=2

--DROP Delete one Employee PROCEDURE
 --DROP PROCEDURE SP_Employee_Delete;
--Delete Employee PROCUDURE 
CREATE PROCEDURE SP_Employee_Delete(@EmployeeId int)
AS 
	BEGIN 
		DELETE FROM Employee WHERE EmployeeId=@EmployeeId
	END

--DROP Delete one Employee PROCEDURE
 --DROP PROCEDURE SP_Employee_Update;
--Delete Employee PROCUDURE 
CREATE PROCEDURE SP_Employee_Update(
	@EmployeeId int, 
	@FullName varchar(500),
	@RoleId int,
	@DoB datetime,
	@Salary float,
	@Password varchar(500),
	@Address varchar(100))
AS 
	BEGIN 
		UPDATE Employee SET 
			FullName=@FullName,
			RoleId=@RoleId,
			DoB=@DoB,
			Salary=@Salary,
			Password=@Password,
			Address=@Address
		WHERE EmployeeId=@EmployeeId
	END
	
--DROP SELECT ALL Employee PROCEDURE
 --DROP PROCEDURE SP_Employee_All_Select;
--SELECT EmployeeS PROCUDURE 
CREATE PROCEDURE SP_Employee_All_Select
AS 
	BEGIN 
		SELECT e.*, r.Name as Role FROM Employee e 
		INNER JOIN Role r ON r.RoleId = e.RoleId;
	END

exec SP_Employee_All_Select;
---### Employee procedures END ###---

---### Supplier procedures ###---
--CREATE PROCEDURE FOR INSERTING Supplier
CREATE PROCEDURE SP_Supplier_Add(@Name varchar(500), @Address varchar(500))
AS 
	BEGIN 
		INSERT INTO Supplier (Name, Address) VALUES(@Name, @Address);
	END

--DROP SELECT one Supplier PROCEDURE
 --DROP PROCEDURE SP_Supplier_One_Select;
--SELECT Supplier PROCUDURE 
CREATE PROCEDURE SP_Supplier_One_Select(@SupplierId int)
AS 
	BEGIN 
		SELECT * FROM Supplier WHERE SupplierId=@SupplierId;
	END

--DROP Delete one Supplier PROCEDURE
 --DROP PROCEDURE SP_Supplier_Delete;
--Delete Supplier PROCUDURE 
CREATE PROCEDURE SP_Supplier_Delete(@SupplierId int)
AS 
	BEGIN 
		DELETE FROM Supplier WHERE SupplierId=@SupplierId
	END

--DROP Delete one Supplier PROCEDURE
 --DROP PROCEDURE SP_Supplier_Update;
--Delete Supplier PROCUDURE 
CREATE PROCEDURE SP_Supplier_Update(@SupplierId int, @Name varchar(500), @Address varchar(500))
AS 
	BEGIN 
		UPDATE Supplier SET Name=@Name, Address=@Address WHERE SupplierId=@SupplierId
	END
	
--DROP SELECT ALL Supplier PROCEDURE
 --DROP PROCEDURE SP_Supplier_All_Select;
--SELECT SupplierS PROCUDURE 
CREATE PROCEDURE SP_Supplier_All_Select
AS 
	BEGIN 
		SELECT * FROM Supplier;
	END

exec SP_Supplier_All_Select;
---### Supplier procedures END ###---

---### Supplier procedures ###---
--CREATE PROCEDURE FOR INSERTING ItemTransaction
CREATE PROCEDURE SP_ItemTransaction_Add(
	@Notes varchar(500), 
	@EmployeeId int,
	@Operation int
	)
AS 
	BEGIN 
		--OPERATION CAN ONLY BE 1 OR -1
		IF (@Operation=1 OR @Operation=-1)
			INSERT INTO ItemTransaction(Notes, EmployeeId, Operation) VALUES(@Notes, @EmployeeId, @Operation);
	END

--DROP SELECT one ItemTransaction PROCEDURE
 --DROP PROCEDURE SP_ItemTransaction_One_Select;
--SELECT ItemTransaction PROCUDURE 
CREATE PROCEDURE SP_ItemTransaction_One_Select(@ItemTransactionId int)
AS 
	BEGIN 
		SELECT it.*, e.FullName as EmployeeName FROM ItemTransaction it 
		INNER JOIN Employee e ON e.EmployeeId = it.EmployeeId 
		WHERE it.ItemTransactionId=@ItemTransactionId;
	END
exec SP_ItemTransaction_One_Select @ItemTransactionid=1
--DROP Delete one ItemTransaction PROCEDURE
--DROP PROCEDURE SP_ItemTransaction_Delete;
--Delete ItemTransaction PROCUDURE 
CREATE PROCEDURE SP_ItemTransaction_Delete(@ItemTransactionId int)
AS 
	BEGIN 
		DELETE FROM ItemTransaction WHERE ItemTransactionId=@ItemTransactionId
	END

--DROP Delete one ItemTransaction PROCEDURE
 DROP PROCEDURE SP_ItemTransaction_Update;
--Delete ItemTransaction PROCUDURE 
CREATE PROCEDURE SP_ItemTransaction_Update(
	@ItemTransactionId int, 
	@Notes varchar(500), 
	@EmployeeId int,
	@Operation int
	)
AS 
	BEGIN 
		UPDATE ItemTransaction SET Notes=@Notes, EmployeeId=@EmployeeId, Operation=@Operation 
		WHERE ItemTransactionId=@ItemTransactionId
	END
	
--DROP SELECT ALL ItemTransaction PROCEDURE
 --DROP PROCEDURE SP_ItemTransaction_All_Select;
--SELECT ItemTransactionS PROCUDURE 
CREATE PROCEDURE SP_ItemTransaction_All_Select
AS 
	BEGIN 
		SELECT it.*, e.FullName as EmployeeName FROM ItemTransaction it
		INNER JOIN Employee e ON e.EmployeeId = it.EmployeeId
		ORDER BY it.CreatedAt DESC
		;
	END

exec SP_ItemTransaction_All_Select;
---### ItemTransaction procedures END ###---

--############################-##########---

---### ROLE procedures START ###---
--DROP SP_Role_Add PROCEDURE
-- --DROP PROCEDURE SP_Role_Add;

--CREATE PROCEDURE FOR INSERTING ROLE
CREATE PROCEDURE SP_ItemTransactionList_Add(
	@ItemTransactionId int,
	@ItemId int,
	@Amount decimal
)
AS 
	BEGIN 
		INSERT INTO ItemTransactionList (ItemTransactionId, ItemId, Amount) 
		VALUES(@ItemTransactionId, @ItemId, @Amount);
	END

--DROP SELECT one ItemTransactionList PROCEDURE
--DROP PROCEDURE SP_ItemTransactionList_One_Select;
--SELECT ItemTransactionList PROCUDURE 
CREATE PROCEDURE SP_ItemTransactionList_One_Select(@ItemTransactionListId int)
AS 
	BEGIN 
		SELECT tl.*, i.LocalName, i.GlobalName, u.Name as UnitName, it.Notes, it.Operation FROM ItemTransactionList tl
		INNER JOIN Item i ON i.ItemId=tl.ItemId
		INNER JOIN Unit u ON i.UnitId = u.UnitId
		INNER JOIN ItemTransaction it ON it.ItemTransactionId = tl.ItemTransactionListId 
		WHERE ItemTransactionListId=@ItemTransactionListId;
	END

--DROP Delete one ItemTransactionList PROCEDURE
-- --DROP PROCEDURE SP_ItemTransactionList_Delete;
--Delete ItemTransactionList PROCUDURE 
CREATE PROCEDURE SP_ItemTransactionList_Delete(@ItemTransactionListId int)
AS 
	BEGIN 
		DELETE FROM ItemTransactionList WHERE ItemTransactionListId=@ItemTransactionListId
	END

--DROP Delete one ItemTransactionList PROCEDURE
-- --DROP PROCEDURE SP_ItemTransactionList_Update;
--Delete ItemTransactionList PROCUDURE 
CREATE PROCEDURE SP_ItemTransactionList_Update(
	@ItemTransactionListId int, 
	@ItemTransactionId int,
	@ItemId int,
	@Amount decimal
	)
AS 
	BEGIN 
		UPDATE ItemTransactionList SET 
			ItemTransactionId=@ItemTransactionId,
			ItemId=@ItemId,
			Amount=@Amount
		WHERE ItemTransactionListId=@ItemTransactionListId
	END
	
--DROP SELECT ALL ItemTransactionList PROCEDURE
DROP PROCEDURE SP_ItemTransactionList_All_Select;
--SELECT ItemTransactionListS PROCUDURE 
CREATE PROCEDURE SP_ItemTransactionList_All_Select
AS 
	BEGIN 
		SELECT tl.*, i.LocalName, i.GlobalName, u.Name as UnitName, it.Notes, it.Operation FROM ItemTransactionList tl
		INNER JOIN Item i ON i.ItemId=tl.ItemId
		INNER JOIN Unit u ON i.UnitId = u.UnitId
		INNER JOIN ItemTransaction it ON it.ItemTransactionId = tl.ItemTransactionListId 
		;
	END

exec SP_ItemTransactionList_All_Select;
---### ItemTransactionList procedures END ###---

