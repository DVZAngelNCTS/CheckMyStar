CREATE TABLE [dbo].[Society] (
    [Identifier] INT NOT NULL,
    [Name] VARCHAR(150) NOT NULL,
    [Email] VARCHAR(100) NULL,
    [Phone] VARCHAR(10) NULL,
    [AddressIdentifier] INT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedDate] DATETIME NULL,
    CONSTRAINT [PK_Society] PRIMARY KEY ([Identifier]),
    CONSTRAINT [FK_Society_Address] FOREIGN KEY ([AddressIdentifier]) REFERENCES [Address]([Identifier])
);