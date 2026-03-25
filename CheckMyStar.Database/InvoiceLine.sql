CREATE TABLE [dbo].[InvoiceLine]
(
    [Identifier] INT NOT NULL,
    [InvoiceIdentifier] INT NOT NULL,

    [Description] VARCHAR(500) NOT NULL,
    [Quantity] DECIMAL(10,2) NOT NULL,
    [Unit] VARCHAR(50) NOT NULL,

    [UnitPriceHT] DECIMAL(10,2) NOT NULL,
    [VATRate] DECIMAL(5,2) NOT NULL DEFAULT 20.00,

    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedDate] DATETIME NULL,

    CONSTRAINT [PK_InvoiceLine] PRIMARY KEY ([Identifier]),

    CONSTRAINT [FK_InvoiceLine_Invoice]
        FOREIGN KEY ([InvoiceIdentifier])
        REFERENCES [Invoice]([Identifier])
);