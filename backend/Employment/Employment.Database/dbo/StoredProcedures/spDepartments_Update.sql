CREATE PROCEDURE [dbo].[spDepartments_Update]
	@Name varchar(500),
	@Id int
AS
BEGIN
	UPDATE [dbo].[Departments] 
	SET Name = @Name
	WHERE Id = @Id
END
GO