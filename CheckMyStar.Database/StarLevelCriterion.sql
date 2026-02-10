CREATE TABLE [dbo].[StarLevelCriterion] (
    [StarLevelId] [TINYINT] NOT NULL,
    [CriterionId] [INT] NOT NULL,
    [TypeCode]    [VARCHAR](5) NOT NULL,

    CONSTRAINT [PK_StarLevelCriterion]
        PRIMARY KEY ([StarLevelId], [CriterionId]),

    CONSTRAINT [FK_SLC_StarLevel]
        FOREIGN KEY ([StarLevelId])
        REFERENCES [dbo].[StarLevel]([StarLevelId]),

    CONSTRAINT [FK_SLC_Criterion]
        FOREIGN KEY ([CriterionId])
        REFERENCES [dbo].[Criterion]([CriterionId]),

    CONSTRAINT [FK_SLC_Type]
        FOREIGN KEY ([TypeCode])
        REFERENCES [dbo].[CriterionType]([TypeCode])
);
