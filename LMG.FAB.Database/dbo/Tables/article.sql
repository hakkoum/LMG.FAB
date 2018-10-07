CREATE TABLE [dbo].[article] (
    [pk_article]          INT            IDENTITY (1, 1) NOT NULL,
    [idIntranet]          INT            NULL,
    [fk_paramDetail_edit] VARCHAR (3)    NULL,
    [titre]               VARCHAR (80)   NULL,
    [fk_paramDetail_pres] INT            NULL,
    [fk_paramDetail_form] INT            NULL,
    [hauteur]             NUMERIC (7, 2) NULL,
    [largeur]             NUMERIC (7, 2) NULL,
    [nbPages]             INT            NULL,
    [nbSignesCalibres]    INT            NULL,
    [nbSignesEvalues]     INT            NULL,
    [observation]         VARCHAR (MAX)  NULL,
    [nbPagesHorsTexte]    INT            NULL,
    [nbPagesHorsTexteDef] BIT            NULL,
    [nbPagesInterieurDef] BIT            NULL,
    [fk_paramDetail_tyac] INT            NULL,
    [fk_paramDetail_gamm] INT            NULL,
    CONSTRAINT [PK__article__FDD1215820ACD28B] PRIMARY KEY CLUSTERED ([pk_article] ASC)
);



