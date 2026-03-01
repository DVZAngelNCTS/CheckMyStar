CREATE TABLE [dbo].[AssessmentCriterion] (
    [AssessmentIdentifier] INT          NOT NULL,
    [CriterionId]          INT          NOT NULL,
    [Points]               INT          NOT NULL,   -- Points obtenus pour ce critère
    [Status]               VARCHAR(10)  NOT NULL,   -- ex: 'x', 'o', 'na', 'x_onc'
    [IsValidated]          BIT          NOT NULL DEFAULT 0,
    [Comment]              VARCHAR(500) NULL,
    CONSTRAINT [PK_EvaluationCriterion] PRIMARY KEY ([AssessmentIdentifier], [CriterionId]),
    CONSTRAINT [FK_EvalCrit_Evaluation] FOREIGN KEY ([AssessmentIdentifier]) REFERENCES [Assessment]([Identifier]),
    CONSTRAINT [FK_EvalCrit_Criterion] FOREIGN KEY ([CriterionId]) REFERENCES [Criterion]([CriterionId])
);