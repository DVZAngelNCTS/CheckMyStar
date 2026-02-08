CREATE TABLE [dbo].[Activity]
(
	[Identifier] INT NOT NULL, 
    [Description] VARCHAR(255) NOT NULL, 
    [Date] DATETIME NOT NULL, 
    [User] INT NOT NULL, 
    [IsSuccess] BIT NOT NULL, 
    CONSTRAINT [FK_Activity_User] FOREIGN KEY ([User]) REFERENCES [User]([Identifier]),
    CONSTRAINT [PK_Activity] PRIMARY KEY ([Identifier])
)
