CREATE TABLE [dbo].[typeElement] (
    [pk_typeElement]        INT          IDENTITY (1, 1) NOT NULL,
    [code]                  VARCHAR (10) NULL,
    [nom]                   VARCHAR (50) NULL,
    [fk_typeElement_parent] INT          NULL,
    [okImpression]          BIT          NULL,
    [okPapier]              BIT          NULL,
    [okFaconnage]           BIT          NULL,
    [okCompositionInterne]  BIT          NULL,
    [okCompositionExterne]  BIT          NULL,
    [tri]                   INT          NULL,
    [okImpositionFeuille]   BIT          NULL,
    [okImpositionCahier]    BIT          NULL,
    [okMultiple]            BIT          NULL,
    PRIMARY KEY CLUSTERED ([pk_typeElement] ASC)
);



