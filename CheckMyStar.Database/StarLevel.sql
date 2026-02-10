CREATE TABLE [dbo].[StarLevel] (
    [StarLevelId] TINYINT NOT NULL ,
    [Label]       NVARCHAR(30) NOT NULL,
    CONSTRAINT [CK_StarLevel_1_5] CHECK ([StarLevelId] BETWEEN 1 AND 5), 
    CONSTRAINT [PK_StarLevel] PRIMARY KEY ([StarLevelId])
);
