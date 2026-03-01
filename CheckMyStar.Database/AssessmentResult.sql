CREATE TABLE [dbo].[AssessmentResult] (
    [Identifier] INT NOT NULL,
    [AssessmentIdentifier] int NOT NULL,
    [IsAccepted] BIT NOT NULL,
    [MandatoryPointsEarned] INT NOT NULL,
    [MandatoryThreshold] INT NOT NULL,
    [OptionalPointsEarned] INT NOT NULL,
    [OptionalRequired] INT NOT NULL,
    [OnceFailedCount] INT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedDate] DATETIME NULL,

    CONSTRAINT [PK_AssessmentResult] PRIMARY KEY ([Identifier]),

    CONSTRAINT [FK_AssessmentResult_Assessment] 
        FOREIGN KEY ([AssessmentIdentifier]) 
        REFERENCES [Assessment]([Identifier])
);