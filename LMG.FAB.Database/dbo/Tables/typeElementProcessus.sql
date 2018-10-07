CREATE TABLE [dbo].[typeElementProcessus] (
    [pk_typeElementProcessus] INT IDENTITY (1, 1) NOT NULL,
    [fk_typeElement]          INT NULL,
    [fk_paramDetail_proc]     INT NULL,
    PRIMARY KEY CLUSTERED ([pk_typeElementProcessus] ASC)
);

