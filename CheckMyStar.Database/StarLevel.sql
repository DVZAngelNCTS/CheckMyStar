CREATE TABLE [dbo].[StarLevel] (
    [StarLevelId] TINYINT NOT NULL PRIMARY KEY,
    [Label]       NVARCHAR(30) NOT NULL,
    CONSTRAINT [CK_StarLevel_1_5] CHECK ([StarLevelId] BETWEEN 1 AND 5)
);
