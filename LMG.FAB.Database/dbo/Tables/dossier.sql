CREATE TABLE [dbo].[dossier] (
    [pk_dossier]           INT            IDENTITY (1, 1) NOT NULL,
    [idTirage]             VARCHAR (17)   NULL,
    [fk_lot]               INT            NULL,
    [prixTTC]              NUMERIC (9, 2) NULL,
    [okCoffret]            BIT            NULL,
    [filierePrincipale]    VARCHAR (10)   NULL,
    [fk_paramDetail_proc]  INT            NULL,
    [idTirage4d]           INT            NULL,
    [qteTirage]            INT            NULL,
    [okQteTirageDef]       BIT            CONSTRAINT [DF_dossier_okQteTirageDef] DEFAULT ((0)) NULL,
    [okLivraisonAnticipee] BIT            CONSTRAINT [DF_dossier_okLivraisonAnticipee] DEFAULT ((0)) NULL,
    [fausseFoliotation]    BIT            NULL,
    [txtModeleComposition] VARCHAR (MAX)  NULL,
    [fk_paramDetail_eqfb]  INT            NULL,
    PRIMARY KEY CLUSTERED ([pk_dossier] ASC)
);





