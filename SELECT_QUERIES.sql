use [DBSD_CW2_7510_8775_7912] EXEC sp_changedbowner 'sa';

--SELECT QUERIES

SELECT * FROM Item

--CREATE A SELECT QUERY FOR ITEM JOING 5 TABLE
SELECT 
	i.*, 
	st.Name as "StoreName", 
	sup.Name as "SupplierName",
	COUNT(trl.TransactionListId) OVER(PARTITION BY i.ItemId) as "TransactionCount",
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
	LocalName LIKE '%lOCA%' AND
	GlobalName LIKE '%Glo%' AND
	ItemUID LIKE '%1%' AND
	MadeOf LIKE '%mET%' AND
	sup.Name LIKE '%Sup%' AND 
	st.Name LIKE '%STO%'

ORDER BY 
	IsEchangeble DESC,
	UsageStartedAt DESC,
	SupplierName DESC,
	StoreName DESC,
	ItemUID DESC
	