CREATE TABLE [dbo].[tiers] (
    [pk_tiers]                INT           IDENTITY (1, 1) NOT NULL,
    [fk_paramDetail_type]     INT           NULL,
    [nom]                     VARCHAR (40)  NULL,
    [adresse1]                VARCHAR (35)  NULL,
    [adresse2]                VARCHAR (35)  NULL,
    [adresse3]                VARCHAR (35)  NULL,
    [codePostal]              VARCHAR (10)  NULL,
    [ville]                   VARCHAR (40)  NULL,
    [pays]                    VARCHAR (40)  NULL,
    [nomContact]              VARCHAR (35)  NULL,
    [telephone]               VARCHAR (20)  NULL,
    [codeFournisseurERP]      VARCHAR (20)  NULL,
    [codeEntrepotERP]         VARCHAR (20)  NULL,
    [signatureFicheTechnique] VARCHAR (50)  NULL,
    [texteCommande]           VARCHAR (MAX) NULL,
    [actif]                   BIT           NULL,
    [adresseEmail]            VARCHAR (200) NULL,
    [okImprimvert]            BIT           NULL,
    PRIMARY KEY CLUSTERED ([pk_tiers] ASC)
);



