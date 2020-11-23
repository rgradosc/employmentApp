
CREATE TABLE [dbo].[Employees]
(
	[Id] INT IDENTITY NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(500) NOT NULL, 
    [Department] VARCHAR(500) NOT NULL, 
    [DateOfJoining] DATE NOT NULL, 
    [PhotoFileName] VARCHAR(500) NOT NULL
)