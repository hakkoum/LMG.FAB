CREATE TABLE [dbo].[devis] (
    [fichierFourni]          BIT NULL,
    [pk_devis]               INT IDENTITY (1, 1) NOT NULL,
    [fk_composant]           INT NOT NULL,
    [okActif]                BIT NULL,
    [fk_paramDetail_stfb]    INT NULL,
    [nbHeuresSuiviFab]       INT NULL,
    [fk_paramDetail_sadm]    INT NULL,
    [fk_collectionTechnique] INT NULL,
    PRIMARY KEY CLUSTERED ([pk_devis] ASC)
);



