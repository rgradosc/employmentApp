﻿CREATE TABLE [dbo].[Users]
(
	[Id] UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [FirstName] NVARCHAR(60) NOT NULL, 
    [LastName] NVARCHAR(60) NOT NULL,
	[Email] NVARCHAR(120) NOT NULL, 
    [UserName] NVARCHAR(120) NOT NULL, 
    [Password] NVARCHAR(MAX) NOT NULL,
    [RoleId] INT NOT NULL, 
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleId]) REFERENCES [Roles]([Id]), 
)
