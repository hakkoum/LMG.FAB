CREATE TABLE [dbo].[prestation] (
    [pk_prestation]       INT            IDENTITY (1, 1) NOT NULL,
    [fk_tiers]            INT            NULL,
    [fk_paramDetail_trfo] INT            NULL,
    [tauxHoraire]         NUMERIC (9, 2) NULL,
    PRIMARY KEY CLUSTERED ([pk_prestation] ASC)
);

