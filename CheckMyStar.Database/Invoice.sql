CREATE TABLE [dbo].[Invoice]
(
	[Identifier] INT NOT NULL,
	[Number] VARCHAR(50) NOT NULL,
	[Amount] DECIMAL(10,2) NOT NULL,
	[IsPaid] BIT NOT NULL DEFAULT 0,
	[CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[DueDate] DATETIME NULL, 
    CONSTRAINT [PK_Invoice] PRIMARY KEY ([Identifier])
)
