CREATE PROCEDURE [dbo].[spEmployees_Insert]
	@Name			varchar(500),
	@Department		varchar(500),
	@DateOfJoining	date,
	@PhotoFileName	varchar(500),
	@id int = 0 output
AS
BEGIN
	INSERT INTO [dbo].[Employees](Name, Department, DateOfJoining, PhotoFileName) 
	VALUES(@Name, @Department,@DateOfJoining,@PhotoFileName);
	SET @id = SCOPE_IDENTITY()
END
GO