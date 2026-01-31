CREATE TABLE [dbo].[Role]
(
	[Identifier] INT NOT NULL , 
    [Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(255) NULL, 
    [IsActive] BIT NULL DEFAULT 1, 
    CONSTRAINT [PK_Role] PRIMARY KEY ([Identifier])
)
