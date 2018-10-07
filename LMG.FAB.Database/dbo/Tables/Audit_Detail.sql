CREATE TABLE [dbo].[Audit_Detail] (
    [AuditDetailId] INT            IDENTITY (1, 1) NOT NULL,
    [AuditId]       INT            NOT NULL,
    [Field]         NVARCHAR (256) NOT NULL,
    [OldValue]      NVARCHAR (MAX) NULL,
    [NewValue]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AuditDetail] PRIMARY KEY CLUSTERED ([AuditDetailId] ASC),
    CONSTRAINT [FK_AuditId] FOREIGN KEY ([AuditId]) REFERENCES [dbo].[Audit] ([AuditId]) ON DELETE CASCADE
);

