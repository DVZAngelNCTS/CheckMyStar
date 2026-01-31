CREATE TABLE [dbo].[Civility]
(
	[Identifier] INT NOT NULL , 
    [Name] VARCHAR(10) NOT NULL, 
    [Description] VARCHAR(20) NOT NULL, 
    CONSTRAINT [PK_Civility] PRIMARY KEY ([Identifier])
)
