CREATE TABLE [dbo].[Appointment]
(
	[Identifier] INT NOT NULL,
	[AppointmentDate] DATETIME NOT NULL,
	[Location] VARCHAR(255) NULL,
	[Comment] VARCHAR(255) NULL,
	[CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(), 
    [UpdatedDate] DATETIME NULL, 
    CONSTRAINT [PK_Appointment] PRIMARY KEY ([Identifier])
)
