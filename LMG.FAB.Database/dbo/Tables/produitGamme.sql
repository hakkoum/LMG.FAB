CREATE TABLE [dbo].[produitGamme] (
    [pk_produitGamme]     INT IDENTITY (1, 1) NOT NULL,
    [fk_produit]          INT NULL,
    [fk_paramDetail_gamm] INT NULL,
    PRIMARY KEY CLUSTERED ([pk_produitGamme] ASC)
);

