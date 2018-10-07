CREATE TABLE [dbo].[machine] (
    [pk_machine]           INT            IDENTITY (1, 1) NOT NULL,
    [nom]                  VARCHAR (40)   NULL,
    [tournees]             VARCHAR (100)  NULL,
    [roto]                 BIT            NULL,
    [coming]               BIT            NULL,
    [cameron]              BIT            NULL,
    [fausseCoupe]          NUMERIC (9, 2) NULL,
    [fk_prestation]        INT            NULL,
    [sousMultiplesCahiers] VARCHAR (100)  NULL,
    PRIMARY KEY CLUSTERED ([pk_machine] ASC)
);

