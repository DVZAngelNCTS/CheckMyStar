CREATE TABLE [dbo].[Invoice]
(
	[Identifier] INT NOT NULL,
	[Reference] VARCHAR(50) NOT NULL,
	[Amount] DECIMAL(10,2) NOT NULL,
	[IsPaid] BIT NOT NULL DEFAULT 0,
	[IssuedDate] DATETIME NOT NULL DEFAULT GETDATE(),

	CONSTRAINT [PK_Invoice] PRIMARY KEY ([Identifier])
)
