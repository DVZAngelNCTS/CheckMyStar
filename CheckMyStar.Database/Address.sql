CREATE TABLE [dbo].[Address]
(
	[Identifier] INT NOT NULL PRIMARY KEY, 
    [Number] VARCHAR(20) NOT NULL, 
    [AddressLine] VARCHAR(100) NOT NULL, 
    [City] VARCHAR(100) NOT NULL, 
    [ZipCode] VARCHAR(20) NOT NULL, 
    [Region] VARCHAR(50) NULL, 
    [CountryIdentifier] INT NOT NULL, 
    CONSTRAINT [FK_Address_Counrty] FOREIGN KEY ([CountryIdentifier]) REFERENCES [Country]([Identifier])
)
