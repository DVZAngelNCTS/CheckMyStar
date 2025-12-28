CREATE TABLE [dbo].[User]
(
    [Identifier] INT NOT NULL IDENTITY, 
    [Civility] INT NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [Society] VARCHAR(150) NULL, 
    [Email] VARCHAR(100) NOT NULL UNIQUE, 
    [Phone] VARCHAR(10) NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [RoleIdentifier] INT NOT NULL, 
    [AddressIdentifier] INT NOT NULL, 
    CONSTRAINT [PK_User] PRIMARY KEY ([Identifier]), 
    CONSTRAINT [FK_User_Address] FOREIGN KEY ([AddressIdentifier]) REFERENCES [Address]([Identifier]), 
    CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleIdentifier]) REFERENCES [Role]([Identifier]) 
)
