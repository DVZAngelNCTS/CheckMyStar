CREATE TABLE [dbo].[Country]
(
	[Identifier] INT NOT NULL , 
    [Code] VARCHAR(2) NOT NULL,
    [Name] VARCHAR(100) NOT NULL, 
    CONSTRAINT [PK_Country] PRIMARY KEY ([Identifier])
)
