﻿
CREATE TRIGGER trDissallowOnModification ON Item
INSTEAD OF UPDATE, DELETE
AS
BEGIN
	IF EXISTS (SELECT date FROM disallowed_dates WHERE DATEDIFF(day, date, GETDATE()) = 0)
	BEGIN
		RAISERROR('DATA MODIFICATION IS NOT ALLOWED TODAY', 16, 1)
	END
END

SELECT GETDATE()

INSERT INTO disallowed_dates (date) values (GETDATE())

select * from disallowed_dates


CREATE TRIGGER trDissallowOnModification ON Item
INSTEAD OF UPDATE, DELETE
AS
BEGIN
	IF EXISTS (SELECT date FROM disallowed_dates WHERE DATEDIFF(day, date, GETDATE()) = 0)
	BEGIN
		RAISERROR('DATA MODIFICATION IS NOT ALLOWED TODAY', 16, 1)
	END
END
