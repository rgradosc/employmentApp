CREATE PROCEDURE [dbo].[spDepartments_Delete]
	@Id int
AS
BEGIN
	DELETE FROM [dbo].[Departments] 
	WHERE Id = @Id
END
GO