CREATE TABLE [dbo].[machineGamme] (
    [pk_machineGamme]     INT          IDENTITY (1, 1) NOT NULL,
    [fk_machine]          INT          NULL,
    [fk_paramDetail_gamm] INT          NULL,
    [codeDatexp]          VARCHAR (20) NULL,
    [actif]               BIT          CONSTRAINT [DF_machineGamme_actif] DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([pk_machineGamme] ASC)
);



