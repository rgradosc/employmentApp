CREATE PROCEDURE [dbo].[spEmployees_GetAll]
	AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [Department], [DateOfJoining], [PhotoFileName] 
	FROM [dbo].[Employees] 
END
GO