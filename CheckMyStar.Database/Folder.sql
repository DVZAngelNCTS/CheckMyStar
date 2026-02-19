CREATE TABLE [dbo].[Folder]
(
	[Identifier] INT NOT NULL,
	-- Type d'hébergement
    [AccommodationTypeIdentifier] INT NOT NULL,
	-- Hébergement
    [AccommodationIdentifier] INT NOT NULL,
	-- Propriétaire
    [OwnerUserIdentifier] INT NOT NULL,
    -- Inspecteur
    [InspectorUserIdentifier] INT NOT NULL,
	-- Statut du dossier
    [FolderStatusIdentifier] INT NOT NULL,

	-- Devis / Facture / Rendez-vous
    [QuoteIdentifier] INT NULL,
    [InvoiceIdentifier] INT NULL,
    [AppointmentIdentifier] INT NULL,

	-- Dates
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedDate] DATETIME NULL,

	CONSTRAINT [PK_Folder] PRIMARY KEY ([Identifier]),

    CONSTRAINT [FK_Folder_User] 
        FOREIGN KEY ([OwnerUserIdentifier]) REFERENCES [User]([Identifier]),

    CONSTRAINT [FK_Folder_Inspector] 
        FOREIGN KEY ([InspectorUserIdentifier]) REFERENCES [User]([Identifier]),

    CONSTRAINT [FK_Folder_AccommodationType] 
        FOREIGN KEY ([AccommodationTypeIdentifier]) REFERENCES [AccommodationType]([Identifier]),

    CONSTRAINT [FK_Folder_Accommodation] 
        FOREIGN KEY ([AccommodationIdentifier]) REFERENCES [Accommodation]([Identifier]),

    CONSTRAINT [FK_Folder_Status] 
        FOREIGN KEY ([FolderStatusIdentifier]) REFERENCES [FolderStatus]([Identifier]),

    CONSTRAINT [FK_Folder_Quote]
        FOREIGN KEY ([QuoteIdentifier]) REFERENCES [Quote]([Identifier]),

    CONSTRAINT [FK_Folder_Invoice] 
        FOREIGN KEY ([InvoiceIdentifier]) REFERENCES [Invoice]([Identifier]),

    CONSTRAINT [FK_Folder_Appointment] 
        FOREIGN KEY ([AppointmentIdentifier]) REFERENCES [Appointment]([Identifier])
)
