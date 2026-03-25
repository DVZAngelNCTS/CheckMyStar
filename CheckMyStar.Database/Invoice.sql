CREATE TABLE [dbo].[Invoice]
(
    [Identifier] INT NOT NULL,
    [InvoiceNumber] VARCHAR(50) NOT NULL,
    [QuoteIdentifier] INT NULL,

    [ClientUserIdentifier] INT NULL,
    [ClientAddressIdentifier] INT NULL,

    [CompanySocietyIdentifier] INT NULL,
    [CompanyAddressIdentifier] INT NULL,

    [InvoiceDate] DATE NOT NULL,
    [DueDate] DATE NULL,

    [TotalAmountHT] DECIMAL(10,2) NOT NULL DEFAULT 0,
    [TotalVATAmount] DECIMAL(10,2) NOT NULL DEFAULT 0,
    [TotalAmountTTC] DECIMAL(10,2) NOT NULL DEFAULT 0,

    [PaymentStatusIdentifier] INT NULL,

    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedDate] DATETIME NULL,

    CONSTRAINT [PK_Invoice] PRIMARY KEY ([Identifier]),

    CONSTRAINT [FK_Invoice_Quote]
        FOREIGN KEY ([QuoteIdentifier])
        REFERENCES [Quote]([Identifier]),

    CONSTRAINT [FK_Invoice_ClientUser]
        FOREIGN KEY ([ClientUserIdentifier])
        REFERENCES [User]([Identifier]),

    CONSTRAINT [FK_Invoice_ClientAddress]
        FOREIGN KEY ([ClientAddressIdentifier])
        REFERENCES [Address]([Identifier]),

    CONSTRAINT [FK_Invoice_CompanySociety]
        FOREIGN KEY ([CompanySocietyIdentifier])
        REFERENCES [Society]([Identifier]),

    CONSTRAINT [FK_Invoice_CompanyAddress]
        FOREIGN KEY ([CompanyAddressIdentifier])
        REFERENCES [Address]([Identifier]),

    CONSTRAINT [FK_Invoice_Status]
        FOREIGN KEY ([PaymentStatusIdentifier])
        REFERENCES [InvoiceStatus]([Identifier]),
);