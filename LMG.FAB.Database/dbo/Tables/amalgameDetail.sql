CREATE TABLE [dbo].[amalgameDetail] (
    [pk_amalgameDetail] INT IDENTITY (1, 1) NOT NULL,
    [fk_amalgame]       INT NULL,
    [fk_devis]          INT NULL,
    PRIMARY KEY CLUSTERED ([pk_amalgameDetail] ASC)
);

