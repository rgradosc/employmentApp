CREATE TABLE [dbo].[UserRoles]
(
	[UserId] NVARCHAR(128) NOT NULL , 
    [RoleId] NVARCHAR(128) NOT NULL, 
    CONSTRAINT [PK_dbo.UserRoles] PRIMARY KEY CLUSTERED ([UserId], [RoleId]), 
    CONSTRAINT [FK_dbo.UserRoles_dbo.Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id]), 
    CONSTRAINT [FK_dbo.UserRoles_dbo.Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles]([Id]) 
)
