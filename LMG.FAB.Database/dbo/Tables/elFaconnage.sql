CREATE TABLE [dbo].[elFaconnage] (
    [pk_element]          INT            NOT NULL,
    [faconnerTout]        BIT            NULL,
    [fk_paramDetail_tbro] INT            NULL,
    [fk_produit_toile]    INT            NULL,
    [fk_paramDetail_gard] INT            NULL,
    [quantite]            INT            NULL,
    [valeurToile]         NUMERIC (9, 2) NULL,
    [ferADorer]           NUMERIC (9, 2) NULL,
    [fk_tiers_faconnier]  INT            NULL,
    CONSTRAINT [PK_elFaconnage] PRIMARY KEY CLUSTERED ([pk_element] ASC)
);



