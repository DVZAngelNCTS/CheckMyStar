CREATE TABLE [dbo].[Quote]
(
	[Identifier] INT NOT NULL,
    [Reference] VARCHAR(50) NOT NULL,
    [Amount] DECIMAL(10,2) NOT NULL,
    [IsAccepted] BIT NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedDate] DATETIME NULL, 
    CONSTRAINT [PK_Quote] PRIMARY KEY ([Identifier])
)
