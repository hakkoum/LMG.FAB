CREATE TABLE [dbo].[ficheStock] (
    [pk_ficheStock] INT             IDENTITY (1, 1) NOT NULL,
    [numFiche]      INT             NULL,
    [denomination]  VARCHAR (50)    NULL,
    [largeurCm]     NUMERIC (11, 2) NULL,
    [hauteurCm]     NUMERIC (11, 2) NULL,
    [grammage]      NUMERIC (11, 2) NULL,
    PRIMARY KEY CLUSTERED ([pk_ficheStock] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ficheStock_ui]
    ON [dbo].[ficheStock]([numFiche] ASC);

