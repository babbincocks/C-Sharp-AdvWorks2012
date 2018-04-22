USE master

IF (SELECT COUNT(*) FROM master.dbo.syslogins WHERE Name = 'AdvWorks2012') > 0
BEGIN

DROP LOGIN AdvWorks2012
USE AdventureWorks2012
DROP USER AdvWorks2012
USE master

END


CREATE LOGIN AdvWorks2012 WITH PASSWORD = 'AW2012'
ALTER LOGIN AdvWorks2012 WITH DEFAULT_DATABASE = AdventureWorks2012

USE AdventureWorks2012

CREATE USER AdvWorks2012 FOR LOGIN AdvWorks2012

ALTER ROLE db_datareader ADD MEMBER AdvWorks2012
ALTER ROLE db_datawriter ADD MEMBER AdvWorks2012

DENY ALTER ON SCHEMA :: HumanResources TO AdvWorks2012
DENY CONTROL ON SCHEMA :: HumanResources TO AdvWorks2012
DENY CREATE SEQUENCE ON SCHEMA :: HumanResources TO AdvWorks2012
DENY DELETE ON SCHEMA :: HumanResources TO AdvWorks2012
DENY EXECUTE ON SCHEMA :: HumanResources TO AdvWorks2012
DENY INSERT ON SCHEMA :: HumanResources TO AdvWorks2012
DENY REFERENCES ON SCHEMA :: HumanResources TO AdvWorks2012
DENY SELECT ON SCHEMA :: HumanResources TO AdvWorks2012
DENY TAKE OWNERSHIP ON SCHEMA :: HumanResources TO AdvWorks2012
DENY UPDATE ON SCHEMA :: HumanResources TO AdvWorks2012
DENY VIEW CHANGE TRACKING ON SCHEMA :: HumanResources TO AdvWorks2012
DENY VIEW DEFINITION ON SCHEMA :: HumanResources TO AdvWorks2012

DENY ALTER ON SCHEMA :: Person TO AdvWorks2012
DENY CREATE SEQUENCE ON SCHEMA :: Person TO AdvWorks2012
DENY DELETE ON SCHEMA :: Person TO AdvWorks2012
DENY INSERT ON SCHEMA :: Person TO AdvWorks2012
DENY UPDATE ON SCHEMA :: Person TO AdvWorks2012
DENY TAKE OWNERSHIP ON SCHEMA :: Person TO AdvWorks2012
GRANT EXECUTE ON OBJECT :: dbo.sp_CustomerSalesInfo TO AdvWorks2012
GRANT EXECUTE ON OBJECT :: dbo.sp_ActiveCustomerNames TO AdvWorks2012

GO

CREATE PROC [dbo].[sp_CustomerSalesInfo]
(@CustID INT)
AS
BEGIN

SELECT SalesOrderID [Sales Order ID], OrderDate [Ordered Date], ShipDate [Shipped Date], CONCAT(P.FirstName, ' ', P.LastName) [Sales Person Name], 
		A.City [Shipped To City], STP.Name [Shipped To State / Province], SOH.TotalDue [Total Amount Due]
FROM Sales.SalesOrderHeader SOH
LEFT JOIN Sales.SalesPerson SP
ON SP.BusinessEntityID = SOH.SalesPersonID
LEFT JOIN Person.Person P
ON P.BusinessEntityID = SP.BusinessEntityID
LEFT JOIN Person.[Address] A
ON A.AddressID = SOH.ShipToAddressID
LEFT JOIN Person.StateProvince STP
ON STP.StateProvinceID = A.StateProvinceID
WHERE SOH.CustomerID = @CustID

END
GO

CREATE PROC [dbo].[sp_ActiveCustomerNames]
AS
BEGIN

SELECT CustomerID, CONCAT(P.FirstName, ' ', P.LastName) [Customer Name]
FROM Sales.Customer C
INNER JOIN Person.Person P
ON P.BusinessEntityID = C.PersonID
ORDER BY CustomerID

END
GO

--EXEC sp_CustomerSalesInfo 14001

--SELECT session_id FROM sys.dm_exec_sessions WHERE login_name = 'AdvWorks2012'
--KILL 53