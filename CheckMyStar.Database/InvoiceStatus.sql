CREATE TABLE [dbo].[InvoiceStatus]
(
    [Identifier] INT NOT NULL,
    [Label] VARCHAR(50) NOT NULL,

    CONSTRAINT [PK_InvoiceStatus] PRIMARY KEY ([Identifier])
);