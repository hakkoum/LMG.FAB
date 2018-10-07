CREATE TABLE [dbo].[composant] (
    [pk_composant] INT           IDENTITY (1, 1) NOT NULL,
    [fk_dossier]   INT           NOT NULL,
    [nom]          VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([pk_composant] ASC)
);

