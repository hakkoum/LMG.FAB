CREATE TABLE [dbo].[elPapier] (
    [pk_element]          INT            NOT NULL,
    [fk_produit]          INT            NULL,
    [fk_paramDetail_qual] INT            NULL,
    [grammage]            NUMERIC (9, 2) NULL,
    [prix]                NUMERIC (9, 2) NULL,
    [main]                NUMERIC (9, 2) NULL,
    [largeurMm]           NUMERIC (9, 2) NULL,
    [hauteurMm]           NUMERIC (9, 2) NULL,
    [papierEco]           BIT            NULL,
    CONSTRAINT [PK_elPapier] PRIMARY KEY CLUSTERED ([pk_element] ASC)
);



