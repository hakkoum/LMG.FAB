import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import * as $ from 'jquery';
import { Event } from '@angular/router/src/events';
import { Audit } from '../../models/audit';
import { AuditService } from '../../services/auditService';
import { AppConfig } from '../../../app.config';
import { DbField } from '../../models/db_fields';
import { TitleService } from '../../services/titleService';


@Component({
    selector: 'audit-list',
    templateUrl: './auditList.component.html',
    styleUrls: ['./auditList.component.css', '../app/app.shared.css']
})
export class AuditListComponent implements OnInit {
    public audits: Audit[] = [];
    public auditsFiltred: Audit[] = [];
    public expandedIndex = -1;
    @Input() selectedKey: number = 0;
    @Input() selectedTable: string = "all";
    public dbFields: DbField[] = [];
    public codeFilter: string = "";
    public typeOperation: string = "";
    public fieldFilter: string = "";
    public userFilter: string = "";
    public dateDebutFilter: Date;
    public dateFinFilter: Date;
    public tableReadOnly = false;
    public checkCreate = true;
    public checkMaj = true;
    public checkDelete = true;
    public selectedDbField: DbField;
    dbFieldLoaded: boolean = false;
    public totalItems: number = 10;
    public currentPage: number = 1;
    public order: string = 'OperationDate';
    public reverse: boolean = false;
    public tableHeight = "77";


    constructor(private auditService: AuditService, private config: AppConfig, private titleService: TitleService) {
        var today = new Date();
        this.dateFinFilter = new Date(today.getFullYear(), today.getMonth(), today.getDate() + 1, 10, 0, 0);
        this.dateDebutFilter = new Date(today.getFullYear(), today.getMonth(), today.getDate() - 7, 10, 0, 0);
        this.loadDbFields();
    }

    ngOnInit() {

        if (this.selectedTable == "all") {
            this.titleService.SetTitle(this.config.getlabel('lbl_title_audit'));
        }
    }

    loadDbFields() {
        if (this.dbFieldLoaded == false) {
            this.dbFieldLoaded = true;
            this.auditService.getDbFieldsList().subscribe((data: any) => {
                this.dbFields = data;
                let tableAll = this.dbFields.find(p => p.TableName == "all")
                if (tableAll === undefined || tableAll === null) {
                    tableAll = new DbField();
                    tableAll.TableName = "all";
                    tableAll.Fields = [];
                    this.dbFields.unshift(tableAll);
                }
                let tmp = this.dbFields.find(p => p.TableName == this.selectedTable);
                if (tmp !== undefined && tmp !== null) {
                    this.selectedDbField = tmp;
                }
                else {
                    this.selectedDbField = tableAll;
                }
                this.fieldFilter = 'all';
            });
        }
    }

    ngOnChanges(changes: SimpleChanges) {
        this.tableReadOnly = (this.selectedTable !== '') ? true : false;
        if (this.selectedTable !== '') {
            this.loadDbFields();        }
        if (this.tableReadOnly) {
            this.tableHeight = "50";
        }
        else {
            this.tableHeight = "77";
        }

    }

    onTableChange(newValue: any) {
        this.selectedDbField = newValue;
        this.selectedTable = this.selectedDbField.TableName;
        this.fieldFilter = 'all';
    }

    searchAudit() {
        let opTypes: string[] = [];
        if (this.checkCreate) { opTypes.push('Added') };
        if (this.checkMaj) { opTypes.push('Modified') };
        if (this.checkDelete) { opTypes.push('Deleted') };
        if (opTypes.length > 0) {
            this.typeOperation = opTypes.join(',');
        }
        this.auditService.getAuditHistory(this.selectedTable, this.typeOperation, this.selectedKey, this.dateDebutFilter, this.dateFinFilter, this.fieldFilter, this.userFilter).subscribe((data: any) => {
            this.audits = data;
            this.auditsFiltred = this.audits.slice(0, 10);
            this.currentPage = 1;
            this.totalItems = this.audits.length;
        });
    }

    setOrder(value: string) {
        if (this.order === value) {
            this.reverse = !this.reverse;
        }
        this.order = value;
    }


    expandRow(index: number): void {
        this.expandedIndex = index === this.expandedIndex ? -1 : index;
    }

    public pageChanged(event: any): void {
        this.auditsFiltred = this.audits.slice((event.page - 1) * event.itemsPerPage, event.page * event.itemsPerPage);
    }
}
