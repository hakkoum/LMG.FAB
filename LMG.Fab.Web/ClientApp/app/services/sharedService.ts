import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from "rxjs/Subject";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Subscription } from "rxjs";

import { Commentaire } from '../models/commentaire';
import { ParamDetail, ParamTable } from '../models/paramTable';
import { Tiers } from '../models/tiers';
import { CollectionTechnique } from '../models/collection_technique';

@Injectable()
export class SharedService {
    constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    addComment(tableName: string, recordKey: any, fieldName: string, comment: string, isImportant: boolean): Observable<Commentaire> {
        let httpParams = new HttpParams();
        httpParams = httpParams.set('tableName', tableName);
        httpParams = httpParams.set('recordKey', recordKey);
        httpParams = httpParams.set('fieldName', fieldName);
        httpParams = httpParams.set('comment', comment);
        httpParams = httpParams.set('isImportant', isImportant ? '1' : '0');

        return this.httpClient.get<Commentaire>(this.baseUrl + '/api/shared/comment-add', { params: httpParams })
            .catch(this.handleError);
    }

    updateComment(commentId: any, comment: string, isImportant: boolean) {
        let httpParams = new HttpParams();
        httpParams = httpParams.set('commentId', commentId);
        httpParams = httpParams.set('comment', comment);
        httpParams = httpParams.set('isImportant', isImportant ? '1' : '0');
        return this.httpClient.get(this.baseUrl + '/api/shared/comment-update', { params: httpParams })
            .catch(this.handleError);
    }

    deleteComment(commentId: any) {
        let httpParams = new HttpParams();
        httpParams = httpParams.set('commentId', commentId);
        return this.httpClient.get(this.baseUrl + '/api/shared/comment-delete', { params: httpParams })
            .catch(this.handleError);
    }

    getRecordComments(tableName: string, recordKey: any): Observable<Commentaire[]> {
        let httpParams = new HttpParams();
        httpParams = httpParams.set('tableName', tableName);
        httpParams = httpParams.set('recordKey', recordKey);
        return this.httpClient.get<Commentaire[]>(this.baseUrl + '/api/shared/comment-list', { params: httpParams })
            .catch(this.handleError);
    }

    getParamTable(): Observable<ParamTable[]> {
        return this.httpClient.get<ParamTable[]>(this.baseUrl + '/api/shared/param-list')
            .catch(this.handleError);
    }

    saveParam(param: ParamTable): Observable<ParamTable> {
        return this.httpClient.post<ParamTable>(this.baseUrl + 'api/shared/param-save', param).catch(this.handleError);
    }

    deleteParam(paramId: number) {
        let httpParams = new HttpParams();
        httpParams = httpParams.set('paramId', paramId.toString());
        return this.httpClient.get(this.baseUrl + '/api/shared/param-delete', { params: httpParams })
            .catch(this.handleError);
    }


    deleteParamDetail(paramDetailId: number) {
        let httpParams = new HttpParams();
        httpParams = httpParams.set('paramDetailId', paramDetailId.toString());
        return this.httpClient.get(this.baseUrl + '/api/shared/param-detail-delete', { params: httpParams })
            .catch(this.handleError);
    }

    getParamValues(paramCode: string): Observable<ParamDetail[]> {
        let httpParams = new HttpParams();
        httpParams = httpParams.set('paramCode', paramCode);
        return this.httpClient.get<ParamDetail[]>(this.baseUrl + '/api/shared/param-values', { params: httpParams })
            .catch(this.handleError);
    }

    getTiers(): Observable<Tiers[]> {
        return this.httpClient.get<Tiers[]>(this.baseUrl + '/api/shared/tiers-list')
            .catch(this.handleError);
    }

    getTiersInfo(tiersId: number): Observable<Tiers> {
        let httpParams = new HttpParams();
        httpParams = httpParams.set('tiersId', tiersId.toString());
        return this.httpClient.get<Tiers>(this.baseUrl + '/api/shared/tiers-info', { params: httpParams })
            .catch(this.handleError);
    }


    getCollectionTechnique(): Observable<CollectionTechnique[]> {
        return this.httpClient.get<CollectionTechnique[]>(this.baseUrl + '/api/shared/coll-list')
            .catch(this.handleError);
    }

    

    private handleError(error: Response) {
        return Observable.throw(error);
    }
}