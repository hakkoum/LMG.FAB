CREATE TABLE [dbo].[collectionTechnique] (
    [pk_collectionTechnique]       INT           IDENTITY (1, 1) NOT NULL,
    [code]                         VARCHAR (10)  NULL,
    [nom]                          VARCHAR (80)  NULL,
    [tri]                          INT           NULL,
    [nbJustificatifs]              INT           NULL,
    [texteJustificatif]            VARCHAR (100) NULL,
    [fk_paramDetail_gamm]          INT           NULL,
    [okActif]                      BIT           CONSTRAINT [DF_collectionTechnique_okActif] DEFAULT ((1)) NULL,
    [fk_paramDetail_pell_couv]     INT           NULL,
    [fk_paramDetail_vern_couv]     INT           NULL,
    [fk_paramDetail_pell_couvr]    INT           NULL,
    [fk_paramDetail_vern_couvr]    INT           NULL,
    [fk_paramDetail_pell_jaquette] INT           NULL,
    [fk_paramDetail_vern_jaquette] INT           NULL,
    PRIMARY KEY CLUSTERED ([pk_collectionTechnique] ASC)
);



