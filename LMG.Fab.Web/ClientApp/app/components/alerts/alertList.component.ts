import { Component, OnInit, Input, SimpleChanges } from '@angular/core';
import * as $ from 'jquery';
import { Event } from '@angular/router/src/events';
import { Audit } from '../../models/audit';
import { AuditService } from '../../services/auditService';
import { AppConfig } from '../../../app.config';
import { DbField } from '../../models/db_fields';


@Component({
    selector: 'alert-list',
    templateUrl: './alertList.component.html',
    styleUrls: ['./alertList.component.css', '../app/app.shared.css']
})
export class alertListComponent implements OnInit {

    constructor(private auditService: AuditService, private config: AppConfig) {
    }

    ngOnInit() {
    }

}
