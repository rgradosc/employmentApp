CREATE PROCEDURE [dbo].[spUsers_Auth]
	@UserName NVARCHAR(120)
	,@Password NVARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [U].[Id], [U].[FirstName],[U].[LastName],[U].[Email], [R].[Name] as [Role]
	FROM [dbo].[Users] U
	INNER JOIN [dbo].[Roles] R ON R.Id = U.RoleId
	WHERE U.UserName = @UserName AND U.[Password] = @Password
END
GO