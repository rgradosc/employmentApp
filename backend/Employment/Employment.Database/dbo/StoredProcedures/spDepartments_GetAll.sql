CREATE PROCEDURE [dbo].[spDepartments_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name] FROM [dbo].[Departments] 
END
GO