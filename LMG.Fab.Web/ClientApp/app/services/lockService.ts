import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from "rxjs/Subject";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Subscription } from "rxjs";

import { TableLock } from '../models/tableLock';

@Injectable()
export class LockService {

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    getLock(tableName: string, recordKey: number): Observable<TableLock> {
        let httpParams = new HttpParams();
        httpParams = httpParams.set('tableName', tableName);
        httpParams = httpParams.set('recordKey', recordKey.toString());

        return this.httpClient.get<TableLock>(this.baseUrl + '/api/shared/lock-get', { params: httpParams })
            .catch(this.handleError);
    }

    deleteLock(tableName: string, recordKey: number) {
        let httpParams = new HttpParams();
        httpParams = httpParams.set('tableName', tableName);
        httpParams = httpParams.set('recordKey', recordKey.toString());

        return this.httpClient.get(this.baseUrl + '/api/shared/lock-delete', { params: httpParams })
            .catch(this.handleError);
    }


    addLock(tableName: string, recordKey: number) {
        let httpParams = new HttpParams();
        httpParams = httpParams.set('tableName', tableName);
        httpParams = httpParams.set('recordKey', recordKey.toString());

        return this.httpClient.get(this.baseUrl + '/api/shared/lock-add', { params: httpParams })
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        return Observable.throw(error);
    }
}