CREATE PROCEDURE [dbo].[spDepartments_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name] FROM [dbo].[Departments] 
	WHERE Id = @Id
END
GO