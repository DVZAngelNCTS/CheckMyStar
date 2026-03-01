CREATE TABLE [dbo].[AccommodationType]
(
	[Identifier] INT NOT NULL,
    [Label] VARCHAR(50) NOT NULL,
    [Description] VARCHAR(255) NULL,
    CONSTRAINT [PK_AccommodationType] PRIMARY KEY ([Identifier])
)
