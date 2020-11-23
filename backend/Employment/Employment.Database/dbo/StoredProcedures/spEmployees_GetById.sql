CREATE PROCEDURE [dbo].[spEmployees_GetById]
	@Id INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [Name], [Department], [DateOfJoining], [PhotoFileName] 
	FROM [dbo].[Employees] 
	WHERE Id = @Id
END
GO