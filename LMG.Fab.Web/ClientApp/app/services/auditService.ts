import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from "rxjs/Subject";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Subscription } from "rxjs";

import { Audit } from "../models/audit";
import { AuditDetail } from "../models/audit";
import { DbField } from '../models/db_fields';
import { Page, PagedData } from '../models/PagedData';

@Injectable()
export class AuditService {

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    getAuditHistory(tableName: string, operationType: string, recordKey: any, operationDateDebut: any, operationDateFin: any
        , fieldFilter: string, userFilter: string): Observable<PagedData<Audit>> {
        let httpParams = new HttpParams();
        if (tableName !== '' && tableName !== null && tableName !== undefined && tableName !== 'all') {
            httpParams = httpParams.set('tableName', tableName);
        }
        if (operationType !== '' && operationType !== null && operationType !== undefined) {
            httpParams = httpParams.set('operationType', operationType);
        }
        if (recordKey !== null && recordKey !== undefined && recordKey > 0) {
            httpParams = httpParams.set('recordKey', recordKey);
        }
        if (operationDateDebut !== '' && operationDateDebut !== null && operationDateDebut !== undefined) {
            httpParams = httpParams.set('operationDateDebut', operationDateDebut.toISOString().substring(0, 10));
        }
        if (operationDateFin !== '' && operationDateFin !== null && operationDateFin !== undefined) {
            httpParams = httpParams.set('operationDateFin', operationDateFin.toISOString().substring(0, 10));

        }
        if (fieldFilter !== '' && fieldFilter !== null && fieldFilter !== undefined && fieldFilter !== 'all') {
            httpParams = httpParams.set('fieldFilter', fieldFilter);
        }
        httpParams = httpParams.set('userFilter', userFilter);

        return this.httpClient.get<PagedData<Audit>>(this.baseUrl + '/api/shared/audit-history', { params: httpParams })
            .catch(this.handleError);
    }

    private getPagedAuditHistory(page: Page, tableName: string, operationType: string, recordKey: any, operationDateDebut: any, operationDateFin: any, fieldFilter: string, userFilter: string): Observable<PagedData<Audit>> {

        return Observable.create((observer: any) => {
            this.getAuditHistory(tableName, operationType, recordKey, operationDateDebut, operationDateFin, fieldFilter, userFilter).subscribe((data: any) => {
                let pagedData = new PagedData<Audit>();
                page.totalElements = data.length;
                page.totalPages = page.totalElements / page.size;
                let start = page.pageNumber * page.size;
                let end = Math.min((start + page.size), page.totalElements);
                for (let i = start; i < end; i++) {
                    //let jsonObj = data[i];
                    //let employee = new CorporateEmployee(jsonObj.name, jsonObj.gender, jsonObj.company, jsonObj.age);
                    // pagedData.data.push(data[i]);
                }
                //pagedData.page = page;
                //return pagedData;
                observer.next(pagedData);
                observer.complete();
            });

        });

    }


    getDbFieldsList(): Observable<DbField[]> {
        return this.httpClient.get<DbField[]>(this.baseUrl + '/api/shared/db-fields-list')
            .catch(this.handleError);
    }


    private handleError(error: Response) {
        return Observable.throw(error);
    }
}