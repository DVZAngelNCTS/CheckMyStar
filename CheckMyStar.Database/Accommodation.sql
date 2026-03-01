CREATE TABLE [dbo].[Accommodation]
(
    [Identifier] INT NOT NULL,
    [AccommodationName] VARCHAR(100) NOT NULL,
    [AccommodationPhone] VARCHAR(10) NULL,
    [AccommodationTypeIdentifier] INT NOT NULL,
    [AccommodationCurrentStar] iNT NULL,
    [AddressIdentifier] INT NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedDate] DATETIME NULL
    CONSTRAINT [PK_Accommodation] PRIMARY KEY ([Identifier]),
    CONSTRAINT [FK_Accommodation_Type] FOREIGN KEY ([AccommodationTypeIdentifier]) REFERENCES [AccommodationType]([Identifier]),
    CONSTRAINT [FK_Accommodation_Address] FOREIGN KEY ([AddressIdentifier]) REFERENCES [Address]([Identifier])
)
