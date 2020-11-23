CREATE PROCEDURE [dbo].[spEmployees_Update]
	@Name			varchar(500),
	@Department		varchar(500),
	@DateOfJoining	date,
	@PhotoFileName	varchar(500),
	@Id				int
AS
BEGIN
	UPDATE [dbo].[Employees] 
	SET  Name = @Name
		,Department = @Department
		,DateOfJoining = @DateOfJoining
		,PhotoFileName = @PhotoFileName
	WHERE Id = @Id
END
GO