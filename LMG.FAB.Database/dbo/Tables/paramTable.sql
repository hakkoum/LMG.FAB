CREATE TABLE [dbo].[paramTable] (
    [pk_paramTable] INT           IDENTITY (1, 1) NOT NULL,
    [code]          VARCHAR (10)  NULL,
    [libelleLong]   VARCHAR (255) NULL,
    [libelleCourt]  VARCHAR (50)  NULL,
    [codeDefaut]    VARCHAR (10)  NULL,
    CONSTRAINT [PK__paramTab__89FBF5BD6F1576F7] PRIMARY KEY CLUSTERED ([pk_paramTable] ASC)
);

