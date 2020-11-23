CREATE PROCEDURE [dbo].[spEmployees_Delete]
	@Id int
AS
BEGIN
	DELETE FROM [dbo].[Employees] 
	WHERE Id = @Id
END
GO