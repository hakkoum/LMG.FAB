CREATE TABLE [dbo].[paramDetail] (
    [pk_paramDetail] INT           IDENTITY (1, 1) NOT NULL,
    [fk_paramTable]  INT           NOT NULL,
    [code]           VARCHAR (10)  NULL,
    [libelleLong]    VARCHAR (255) NULL,
    [libelleCourt]   VARCHAR (50)  NULL,
    [tri]            INT           NULL,
    [actif]          BIT           CONSTRAINT [DF_paramDetail_actif] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK__paramDet__2AD306E472E607DB] PRIMARY KEY CLUSTERED ([pk_paramDetail] ASC),
    CONSTRAINT [FK_paramTable_paramDetail] FOREIGN KEY ([fk_paramTable]) REFERENCES [dbo].[paramTable] ([pk_paramTable])
);

