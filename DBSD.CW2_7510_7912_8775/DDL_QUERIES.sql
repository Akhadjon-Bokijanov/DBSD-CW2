--1 CREATE Unit TABLE
DROP TABLE IF EXISTS [Unit];
CREATE TABLE Unit (
  [UnitId] int NOT NULL IDENTITY,
  [ShortName] varchar(45) NOT NULL,
  [Name] varchar(100) DEFAULT NULL,
  PRIMARY KEY ([UnitId])

)  ;

--SAMPLE 1
INSERT INTO Unit (ShortName, Name) VALUES ('KG', 'Kilogram');

--2 CREATE Role table
DROP TABLE IF EXISTS [Role];
CREATE TABLE Role (
	[RoleId] int NOT NULL IDENTITY,
	[Name] varchar(200) NOT NULL,

	PRIMARY KEY ([RoleId])
);
INSERT INTO Role (Name) VALUES ('Manager');

--3 CREATE User table
DROP TABLE IF EXISTS [Employee];
CREATE TABLE Employee (
  [EmployeeId] int NOT NULL IDENTITY,
  [FullName] varchar(100) NOT NULL,
  [RoleId] int NOT NULL,
  [DoB] date NOT NULL,
  [Salary] float DEFAULT NULL,
  [Password] varchar(500) NOT NULL,
  [Address] varchar(100) NOT NULL,
  --BIND Role TO Employee
  CONSTRAINT [FK_EmployeeRoleBind] FOREIGN KEY ([RoleId]) REFERENCES Role ([RoleId]),
  
  PRIMARY KEY ([EmployeeId]),
)  ;
INSERT INTO Employee (FullName, RoleId, DoB, Salary, Password, Address) 
VALUES ('Akhadjon Bokijanov', 1, '2000-05-18', 1000, 'password', 'address')

--4 LET'S CREATE INDEX ON FullName, Address, Salary for optimization of search and filters
 -- CREATE INDEX [USER_FULLNAME_INDEX] ON Employee ([FullName]);
  --CREATE INDEX [USER_ADDRESS_INDEX] ON Employee ([Address]);
  --CREATE INDEX [USER_SALARY_INDEX] ON Employee ([Salary]);

 --5 CREATE Storage TABLE
DROP TABLE IF EXISTS [Store];
CREATE TABLE Store (
  [StoreId] int NOT NULL IDENTITY,
  [Name] varchar(250) NOT NULL,
  [Square] float DEFAULT NULL,
  [EmployeeId] int NOT NULL,

  PRIMARY KEY ([StoreId]),
  CONSTRAINT [FK_EmployeeStoreBind] FOREIGN KEY ([EmployeeId]) REFERENCES Employee ([EmployeeId]),
  )
  INSERT INTO Store (Name, Square, EmployeeId)
  VALUES ('Store A', 1000, 1)

--6 CREATE SUPPLIER table
DROP TABLE IF EXISTS [Supplier];
CREATE TABLE Supplier (
	[SupplierId] int NOT NULL IDENTITY,
	[Name] varchar(1000) NOT NULL,
	[Address] varchar(1000) NOT NULL

	PRIMARY KEY ([SupplierId])
);
INSERT INTO Supplier (Name, Address)
VALUES ('Supllier A', 'Supplier address')

--7 CREATE Item table
DROP TABLE IF EXISTS [Item];
CREATE TABLE Item (
  [ItemId] int NOT NULL IDENTITY,
  [LocalName] varchar(500) NOT NULL,
  [GlobalName] varchar(500) DEFAULT NULL,
  [ItemUID] int NOT NULL,
  [IsEchangeble] bit,
  [Image] varbinary(max) NULL,
  [MadeOf] varchar(500),
  [UsageStartedAt] datetime,
  [SupplierId] int NULL,  
  [UnitId] int NOT NULL,
  [StoreId] int NOT NULL,

  PRIMARY KEY ([ItemId]),
  CONSTRAINT [ITEM_UIID_UNIQUE] UNIQUE  ([ItemUID])
 ,
  CONSTRAINT [FK_ItemUnitBind] FOREIGN KEY ([UnitId]) REFERENCES Unit ([UnitId]),
  CONSTRAINT [FK_ItemStoreBind] FOREIGN KEY ([StoreId]) REFERENCES Store ([StoreId]),
  CONSTRAINT [FK_ItemSupplierBind] FOREIGN KEY ([SupplierId]) REFERENCES Supplier ([SupplierId]),
);

--8 CREATE TABLE FOR parent child relations of Items
DROP TABLE IF EXISTS [ItemParentChildBind]
CREATE TABLE ItemParentChildBind (
	[ItemBindId] int NOT NULL IDENTITY,
	[ParentId] int NOT NULL,
	[ChildId] int NOT NULL,
	[ChildAmount] decimal(10, 3) NOT NULL,

	PRIMARY KEY ([ItemBindId]),

	CONSTRAINT [FK_ParentChildBindParentId] FOREIGN KEY ([ParentId]) REFERENCES Item ([ItemId]),
	CONSTRAINT [FK_ParentChildBindChildId] FOREIGN KEY ([ChildId]) REFERENCES Item ([ItemId]),
)

--9 CREATE ItemTransaction TABLE 
DROP TABLE IF EXISTS [ItemTransaction]
CREATE TABLE ItemTransaction (
	[ItemTransactionId] int NOT NULL IDENTITY,
	[Notes] varchar(1000) NULL,
	[EmployeeId] int NOT NULL,
	[CreatedAt] datetime DEFAULT CURRENT_TIMESTAMP,

	--CAN BE 1 OR -1 (INPUT OR OUTPUT) OPERATION
	[Operation] int NOT NULL,

	PRIMARY KEY([ItemTransactionId]),

	CONSTRAINT [FK_TransactionEmployeeBind] FOREIGN KEY ([EmployeeId]) REFERENCES Employee ([EmployeeId]),
)

--10 CREATE ItemTransaction TABLE 
DROP TABLE IF EXISTS [ItemTransactionList]
CREATE TABLE ItemTransactionList (
	[ItemTransactionListId] int NOT NULL IDENTITY,
	[ItemTransactionId] int NOT NULL,
	[ItemId] int NOT NULL,
	[Amount] decimal (10, 2) NOT NULL,

	PRIMARY KEY ([ItemTransactionListId]),

	CONSTRAINT [FK_TransactionListItemBind] FOREIGN KEY ([ItemId]) REFERENCES Item ([ItemId]),
	CONSTRAINT [FK_TransactionListTransactionBind] FOREIGN KEY ([ItemTransactionId]) REFERENCES ItemTransaction ([ItemTransactionId]),
)

--2 CREATE Role table
DROP TABLE IF EXISTS [disallowed_dates];
CREATE TABLE [disallowed_dates] (
	[disallowed_date_id] int NOT NULL IDENTITY,
	[date] datetime NOT NULL,

	PRIMARY KEY ([disallowed_date_id])
);
INSERT INTO [disallowed_dates] (date) VALUES ('2020-01-01 00:00:00');




