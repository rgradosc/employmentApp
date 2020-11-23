CREATE PROCEDURE [dbo].[spDepartments_Insert]
	@Name varchar(500),
	@id int = 0 output
AS
BEGIN
	INSERT INTO [dbo].[Departments](Name) VALUES(@Name);
	SET @id = SCOPE_IDENTITY()
END
GO