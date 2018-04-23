USE master
/*
First the script checks to see if there's already a login called "AdvWorks2012", and if there is, it gets rid of the login in the master
database, and then it goes into the AdventureWorks2012 database and deletes the AdvWorks2012 user that goes along with the login.
*/
IF (SELECT COUNT(*) FROM master.dbo.syslogins WHERE Name = 'AdvWorks2012') > 0
BEGIN

DROP LOGIN AdvWorks2012
USE AdventureWorks2012
DROP USER AdvWorks2012
USE master

END

/*
Then it creates a login called AdvWorks2012, and assigns it a password of AW2012. Then, just because, the default database is set to 
the AdventureWorks2012 database, as that's pretty much all it'll be working with, ever.
*/
CREATE LOGIN AdvWorks2012 WITH PASSWORD = 'AW2012'
ALTER LOGIN AdvWorks2012 WITH DEFAULT_DATABASE = AdventureWorks2012

USE AdventureWorks2012






--Then an accompanying user profile in the AdventureWorks2012 database is created for the AdvWorks2012 login.
CREATE USER AdvWorks2012 FOR LOGIN AdvWorks2012

--This user profile is then added to both the datareader and datawriter roles, so it can actually access information in AdventureWorks2012.
ALTER ROLE db_datareader ADD MEMBER AdvWorks2012
ALTER ROLE db_datawriter ADD MEMBER AdvWorks2012

--As this user shouldn't be able to interact with the HumanResources scheme, and just to be thorough and safe, every single aspect of 
--interaction is denied on the HumanResources schema.
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

--Then, as we want the user to be able to read data from the Person schema, but pretty much nothing else to it, all of the aspects related to
--writing data or DDL is denied in the Person schema.
DENY ALTER ON SCHEMA :: Person TO AdvWorks2012
DENY CREATE SEQUENCE ON SCHEMA :: Person TO AdvWorks2012
DENY DELETE ON SCHEMA :: Person TO AdvWorks2012
DENY INSERT ON SCHEMA :: Person TO AdvWorks2012
DENY UPDATE ON SCHEMA :: Person TO AdvWorks2012
DENY TAKE OWNERSHIP ON SCHEMA :: Person TO AdvWorks2012



GO

/*
This is code that checks to see if the stored procedure that's about to be created below this already exists. If it does, it gets rid of that 
stored procedure, so this script can be run over and over again without any errors.
*/
IF (SELECT COUNT(*) FROM dbo.sysobjects WHERE name = 'sp_CustomerSalesInfo') > 0
BEGIN
DROP PROC sp_CustomerSalesInfo
END

GO

/*
A stored procedure is created for use in the C# program that retrieves many different aspects to a customer's order from various different
tables, which is joined together through left joins, pretty much purely because not every order has a salesperson associated with it, and
inner joins would get rid of a lot of orders. Which rows are returned is determined by the value of the variable supplied by the user.

This is created to populate the data grid in the C# application.
*/
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


--Just like before, this checks to see if the next stored procedure already exists, and gets rid of it if it does.
IF (SELECT COUNT(*) FROM dbo.sysobjects WHERE name = 'sp_ActiveCustomerNames') > 0
BEGIN
DROP PROC sp_ActiveCustomerNames
END

GO


/*
Here's another stored procedure that returns the name and ID of all customers, so it can be used to populate the combo box in the C# program.
This combo box is going to be created so the user doesn't need to know any customer ID's, and can instead look up records by a customer's 
name, which are guaranteed to be in the database.
*/
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

--Finally, the user is granted the ability to execute these stored procedures that have been created.
GRANT EXECUTE ON OBJECT :: dbo.sp_CustomerSalesInfo TO AdvWorks2012
GRANT EXECUTE ON OBJECT :: dbo.sp_ActiveCustomerNames TO AdvWorks2012


