CREATE TABLE [dbo].[element] (
    [pk_element]           INT            IDENTITY (1, 1) NOT NULL,
    [fk_devis]             INT            NULL,
    [fk_typeElement]       INT            NULL,
    [fraisFixe]            NUMERIC (9, 2) NULL,
    [fraisVariable]        NUMERIC (9, 2) NULL,
    [portFixe]             NUMERIC (9, 2) NULL,
    [fk_tiers_fournisseur] INT            NULL,
    [calculFixe]           NUMERIC (9, 2) NULL,
    [calculVariable]       NUMERIC (9, 2) NULL,
    [calculTotal]          NUMERIC (9, 2) NULL,
    PRIMARY KEY CLUSTERED ([pk_element] ASC)
);



