CREATE TABLE [dbo].[elImpositionFeuille] (
    [pk_element]         INT            NOT NULL,
    [imposition64Pages]  BIT            NULL,
    [nbPagesParFeuille1] INT            NULL,
    [nbPagesParFeuille2] INT            NULL,
    [nbPagesParFeuille3] INT            NULL,
    [nbPagesChute]       INT            NULL,
    [nbFeuilles1]        NUMERIC (9, 2) NULL,
    [nbFeuilles2]        NUMERIC (9, 2) NULL,
    [nbFeuilles3]        NUMERIC (9, 2) NULL,
    CONSTRAINT [PK_elImpositionFeuille] PRIMARY KEY CLUSTERED ([pk_element] ASC)
);

