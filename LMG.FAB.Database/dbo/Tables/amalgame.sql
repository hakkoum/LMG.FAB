CREATE TABLE [dbo].[amalgame] (
    [pk_amalgame]    INT IDENTITY (1, 1) NOT NULL,
    [fk_lot]         INT NULL,
    [fk_typeElement] INT NULL,
    [fk_tiers]       INT NULL,
    PRIMARY KEY CLUSTERED ([pk_amalgame] ASC)
);

