CREATE TABLE [dbo].[User] 
(
    [Identifier] INT NOT NULL,
    [CivilityIdentifier] INT NOT NULL,
    [LastName] VARCHAR(50) NOT NULL,
    [FirstName] VARCHAR(50) NOT NULL,
    [SocietyIdentifier] INT NULL,
    [Email] VARCHAR(100) NOT NULL,
    [Phone] VARCHAR(10) NULL,
    [Password] VARCHAR(64) NOT NULL,
    [RoleIdentifier] INT NOT NULL,
    [AddressIdentifier] INT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsFirstConnection] BIT NOT NULL DEFAULT 1, 
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedDate] DATETIME NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Identifier]),
    CONSTRAINT [FK_User_Address] FOREIGN KEY ([AddressIdentifier]) REFERENCES [Address]([Identifier]),
    CONSTRAINT [FK_User_Role] FOREIGN KEY ([RoleIdentifier]) REFERENCES [Role]([Identifier]),
    CONSTRAINT [FK_User_Civility] FOREIGN KEY ([CivilityIdentifier]) REFERENCES [Civility]([Identifier]),
    CONSTRAINT [FK_User_Society] FOREIGN KEY ([SocietyIdentifier]) REFERENCES [Society]([Identifier])
);

