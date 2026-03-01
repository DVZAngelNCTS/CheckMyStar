CREATE TABLE [dbo].[Folder]
(
	[Identifier] INT NOT NULL,
    [AccommodationTypeIdentifier] INT NOT NULL,
    [AccommodationIdentifier] INT NOT NULL,
    [OwnerUserIdentifier] INT NOT NULL,
    [InspectorUserIdentifier] INT NOT NULL,
    [FolderStatusIdentifier] INT NOT NULL,
    [QuoteIdentifier] INT NULL,
    [InvoiceIdentifier] INT NULL,
    [AppointmentIdentifier] INT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedDate] DATETIME NULL,
	CONSTRAINT [PK_Folder] PRIMARY KEY ([Identifier]),
    CONSTRAINT [FK_Folder_User] FOREIGN KEY ([OwnerUserIdentifier]) REFERENCES [User]([Identifier]),
    CONSTRAINT [FK_Folder_Inspector] FOREIGN KEY ([InspectorUserIdentifier]) REFERENCES [User]([Identifier]),
    CONSTRAINT [FK_Folder_AccommodationType] FOREIGN KEY ([AccommodationTypeIdentifier]) REFERENCES [AccommodationType]([Identifier]),
    CONSTRAINT [FK_Folder_Accommodation] FOREIGN KEY ([AccommodationIdentifier]) REFERENCES [Accommodation]([Identifier]),
    CONSTRAINT [FK_Folder_Status] FOREIGN KEY ([FolderStatusIdentifier]) REFERENCES [FolderStatus]([Identifier]),
    CONSTRAINT [FK_Folder_Quote] FOREIGN KEY ([QuoteIdentifier]) REFERENCES [Quote]([Identifier]),
    CONSTRAINT [FK_Folder_Invoice] FOREIGN KEY ([InvoiceIdentifier]) REFERENCES [Invoice]([Identifier]),
    CONSTRAINT [FK_Folder_Appointment] FOREIGN KEY ([AppointmentIdentifier]) REFERENCES [Appointment]([Identifier])
)
