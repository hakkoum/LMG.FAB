CREATE TABLE [dbo].[Audit] (
    [AuditId]       INT            IDENTITY (1, 1) NOT NULL,
    [UserName]      NVARCHAR (256) NOT NULL,
    [OperationDate] DATETIME       NOT NULL,
    [TableName]     NVARCHAR (40)  NOT NULL,
    [OperationType] NVARCHAR (20)  NOT NULL,
    [RecordName]    NVARCHAR (256) NULL,
    [RecordKey]     INT            NOT NULL,
    CONSTRAINT [PK_Audit] PRIMARY KEY CLUSTERED ([AuditId] ASC)
);

