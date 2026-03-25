CREATE TABLE [dbo].[Quote]
(
    [Identifier] INT NOT NULL,
    [ClientUserIdentifier]    INT           NULL,
    [ClientAddressIdentifier] INT           NULL,
    [InspectorIdentifier] INT           NULL,
    [CompanySocietyIdentifier] INT          NULL,
    [CompanyAddressIdentifier] INT          NULL,
    [CompanyLogoPath] VARCHAR(500) NULL,
    [CompanyEmail] VARCHAR(100) NULL,
    [CompanyPhone] VARCHAR(10) NULL,
    [CompanySiretCode] VARCHAR(14) NULL,
    [CompanyVatNumber] VARCHAR(20) NULL,
    [CompanyLegalInformation] VARCHAR(500) NULL,
    [TotalAmountHT] DECIMAL(10,2) NOT NULL DEFAULT 0,
    [TotalAmountTTC]          DECIMAL(10,2) NOT NULL DEFAULT 0,
    [ValidityDate]            DATE          NULL,
    [ExecutionDate]           DATE          NULL,
    [QuoteStatusIdentifier]   INT           NULL,
    [IsEditable]              BIT           NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [UpdatedDate] DATETIME NULL, 

    CONSTRAINT [PK_Quote] PRIMARY KEY ([Identifier]),

    CONSTRAINT [FK_Quote_ClientUser]
        FOREIGN KEY ([ClientUserIdentifier])
        REFERENCES [User]([Identifier]),

    CONSTRAINT [FK_Quote_ClientAddress]
        FOREIGN KEY ([ClientAddressIdentifier])
        REFERENCES [Address]([Identifier]),

    CONSTRAINT [FK_Quote_CompanySociety]
        FOREIGN KEY ([CompanySocietyIdentifier])
        REFERENCES [Society]([Identifier]),

    CONSTRAINT [FK_Quote_CompanyAddress]
        FOREIGN KEY ([CompanyAddressIdentifier])
        REFERENCES [Address]([Identifier]),

    CONSTRAINT [FK_Quote_QuoteStatus]
        FOREIGN KEY ([QuoteStatusIdentifier])
        REFERENCES [QuoteStatus]([Identifier]),

    CONSTRAINT [FK_Quote_Inspector] 
        FOREIGN KEY ([InspectorIdentifier]) 
        REFERENCES [User]([Identifier]),
)
