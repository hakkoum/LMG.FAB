CREATE TABLE [dbo].[elImpositionCahier] (
    [pk_element]        INT NOT NULL,
    [nbPagesParCahier1] INT NULL,
    [nbPagesParCahier2] INT NULL,
    [nbPagesParCahier3] INT NULL,
    [nbPagesParCahier4] INT NULL,
    [nbCahiers1]        INT NULL,
    [nbCahiers2]        INT NULL,
    [nbCahiers3]        INT NULL,
    [nbCahiers4]        INT NULL,
    CONSTRAINT [PK_elImpositionCahier] PRIMARY KEY CLUSTERED ([pk_element] ASC)
);

