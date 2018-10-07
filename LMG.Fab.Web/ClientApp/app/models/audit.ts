export class Audit {
    AuditId: number;
    UserName: string;
    TableName: string;
    OperationType: string;
    OperationDate: Date;
    RecordKey: number;
    RecordName: string;
    AuditDetail: AuditDetail[];
}

export class AuditDetail {
    AuditDetailId: number;
    AuditId: number;
    Field: string;
    OldValue: string;
    NewValue: string;
}