CREATE PROCEDURE [dbo].[spUsers_GetPasswordHashByUserId]
	@UserId NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [PasswordHash]
	FROM [dbo].[Users]
	WHERE Id = @UserId 
END
GO