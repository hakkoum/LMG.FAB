CREATE TABLE [dbo].[produit] (
    [pk_produit]          INT            IDENTITY (1, 1) NOT NULL,
    [famille]             VARCHAR (10)   NULL,
    [intitule]            VARCHAR (40)   NULL,
    [grammage]            NUMERIC (9, 2) NULL,
    [prixKg]              NUMERIC (9, 3) NULL,
    [main]                NUMERIC (9, 2) NULL,
    [largeurMm]           NUMERIC (9, 2) NULL,
    [hauteurMm]           NUMERIC (9, 2) NULL,
    [okRoto]              BIT            NULL,
    [okFeuille]           BIT            NULL,
    [definiParUtil]       BIT            NULL,
    [fk_paramDetail_qual] INT            NULL,
    PRIMARY KEY CLUSTERED ([pk_produit] ASC)
);



