CREATE TABLE [dbo].[lot] (
    [pk_lot]              INT           IDENTITY (1, 1) NOT NULL,
    [nomLot]              NVARCHAR (20) NOT NULL,
    [fk_paramDetail_Proc] INT           NULL,
    [codeLot]             NCHAR (10)    NOT NULL,
    [fk_offices]          INT           NULL,
    [dateArriveeLivres]   DATE          NULL,
    [dateMiseEnVente]     DATE          NULL,
    [enCours]             BIT           NULL,
    CONSTRAINT [PK_lot] PRIMARY KEY CLUSTERED ([pk_lot] ASC),
    CONSTRAINT [FK_lot_processus] FOREIGN KEY ([fk_paramDetail_Proc]) REFERENCES [dbo].[paramDetail] ([pk_paramDetail])
);

