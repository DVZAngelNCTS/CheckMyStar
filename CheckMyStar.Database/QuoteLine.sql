CREATE TABLE [dbo].[QuoteLine]
(
    [Identifier]      INT            NOT NULL,
    [QuoteIdentifier] INT            NOT NULL,
    [Description]     VARCHAR(500)   NOT NULL,
    [Quantity]        DECIMAL(10,2)  NOT NULL,
    [Unit]            VARCHAR(50)    NOT NULL,
    [UnitPriceHT]     DECIMAL(10,2)  NOT NULL,
    [VATRate]         DECIMAL(5,2)   NOT NULL DEFAULT 20.00,
    [CreatedDate]     DATETIME       NOT NULL DEFAULT GETDATE(),
    [UpdatedDate]     DATETIME       NULL,

    CONSTRAINT [PK_QuoteLine] PRIMARY KEY ([Identifier]),

    CONSTRAINT [FK_QuoteLine_Quote] FOREIGN KEY ([QuoteIdentifier]) 
        REFERENCES [Quote]([Identifier])
);