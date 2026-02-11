CREATE TABLE [dbo].[Criterion] (
    [CriterionId]   INT IDENTITY(1,1) NOT NULL ,
    [Description]   VARCHAR(500) NOT NULL,
    [BasePoints]    DECIMAL(9,2) NOT NULL,
    CONSTRAINT [CK_Criterion_PointsNonNeg] CHECK ([BasePoints] >= 0), 
    CONSTRAINT [PK_Criterion] PRIMARY KEY ([CriterionId])
);
