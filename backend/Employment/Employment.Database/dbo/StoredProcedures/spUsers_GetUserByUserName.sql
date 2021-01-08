CREATE PROCEDURE [dbo].[spUsers_GetUserByUserName]
	@UserName NVARCHAR(120)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [FirstName],[LastName],[Email]
	FROM [dbo].[Users]
	WHERE UserName = @UserName 
END
GO