CREATE TABLE [dbo].[CriterionType] (
    [TypeCode]   VARCHAR(5)  NOT NULL ,
    [Label]      NVARCHAR(80) NOT NULL, 
    CONSTRAINT [PK_CriterionType] PRIMARY KEY ([TypeCode])
);
