CREATE TABLE [dbo].[commentaire] (
    [pk_commentaire]  INT            IDENTITY (1, 1) NOT NULL,
    [tableComm]       VARCHAR (255)  NOT NULL,
    [recordKey]       INT            NOT NULL,
    [recordName]      VARCHAR (255)  NULL,
    [fieldComm]       VARCHAR (255)  NULL,
    [lastUpdate]      DATETIME       NOT NULL,
    [userName]        NVARCHAR (256) NULL,
    [commentaireHtml] NVARCHAR (MAX) NULL,
    [isImportant]     BIT            CONSTRAINT [DF_commentaire_isImportant] DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([pk_commentaire] ASC)
);

