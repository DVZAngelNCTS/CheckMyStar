CREATE TABLE [dbo].[Assessment] (
    [Identifier]          INT             IDENTITY(1,1) NOT NULL,
    [FolderIdentifier]    INT             NOT NULL,
    [TargetStarLevel]     TINYINT         NOT NULL,        -- Étoile visée
    [Capacity]            INT             NOT NULL,        -- Capacité d’accueil max
    [NumberOfFloors]      INT             NOT NULL,        -- Nombre d’étages
    [IsWhiteZone]         BIT             NOT NULL DEFAULT 0,
    [IsDromTom]           BIT             NOT NULL DEFAULT 0,
    [IsHighMountain]      BIT             NOT NULL DEFAULT 0,
    [IsBuildingClassified] BIT            NOT NULL DEFAULT 0,
    [IsStudioNoLivingRoom] BIT            NOT NULL DEFAULT 0,
    [IsParkingImpossible] BIT             NOT NULL DEFAULT 0,
    [TotalArea]           DECIMAL(10,2)   NOT NULL,        -- Superficie totale (m²)
    [NumberOfRooms]       INT             NOT NULL,        -- Nombre de chambres
    [TotalRoomsArea]      DECIMAL(10,2)   NOT NULL,        -- Somme des surfaces des chambres
    [SmallestRoomArea]    DECIMAL(10,2)   NOT NULL,        -- Plus petite chambre (m²)
    [CreatedDate]         DATETIME        NOT NULL DEFAULT GETDATE(),
    [UpdatedDate]         DATETIME        NULL,
    [IsComplete]          BIT             NOT NULL DEFAULT 0, -- Évaluation terminée
    CONSTRAINT [PK_Assessment] PRIMARY KEY ([Identifier]), CONSTRAINT [FK_Assessment_Folder] FOREIGN KEY ([FolderIdentifier]) REFERENCES [Folder]([Identifier]),
    CONSTRAINT [FK_Assessment_StarLevel] FOREIGN KEY ([TargetStarLevel]) REFERENCES [StarLevel]([StarLevelId])
);