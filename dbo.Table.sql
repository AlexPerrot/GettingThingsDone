CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Username] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [Mail] VARCHAR(50) NOT NULL
)
