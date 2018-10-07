import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from "rxjs/Subject";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Subscription } from "rxjs";
import 'rxjs/add/observable/throw';

import { Lot } from "../models/lot";
import { ParamDetail } from '../models/paramTable';

@Injectable()
export class LotService {

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    searchLot(nomLot: string, codeLot: string, dateMiseEnVenteDebut: any, dateMiseEnVentefin: any, processusId: any): Observable<Lot[]> {
        let httpParams = new HttpParams();
        if (nomLot !== '' && nomLot !== null && nomLot !== undefined) {
            httpParams = httpParams.set('nomLot', nomLot);
        }
        if (codeLot !== '' && codeLot !== null && codeLot !== undefined) {
            httpParams = httpParams.set('codeLot', codeLot);
        }
        if (dateMiseEnVenteDebut !== '' && dateMiseEnVenteDebut !== null && dateMiseEnVenteDebut !== undefined) {
            httpParams = httpParams.set('dateMiseEnVenteDebut', dateMiseEnVenteDebut.toISOString().substring(0, 10));
        }
        if (dateMiseEnVentefin !== '' && dateMiseEnVentefin !== null && dateMiseEnVentefin !== undefined) {
            httpParams = httpParams.set('dateMiseEnVentefin', dateMiseEnVentefin.toISOString().substring(0, 10));
        }
        if (processusId !== '' && processusId !== null && processusId !== undefined && processusId > 0) {
            httpParams = httpParams.set('processusId', processusId);
        }

        return this.httpClient.get<Lot[]>(this.baseUrl + '/api/lot/list', { params: httpParams })
            .catch(this.handleError);
    }

    getProcessusList(): Observable<ParamDetail[]> {
        return this.httpClient.get<ParamDetail[]>(this.baseUrl + '/api/shared/process-list')
            .catch(this.handleError);
    }



    getLotById(id: number): Observable<Lot> {
        return this.httpClient.get<Lot>(this.baseUrl + '/api/lot/getLot?lotId=' + id)
            .catch(this.handleError);
    }

    saveLot(lot: Lot) {
        let headers = new HttpHeaders();
        headers = headers.set('Content-Type', 'application/json; charset=utf-8');
        return this.httpClient.post(this.baseUrl + 'api/lot/saveLot', lot)
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        return Observable.throw(error);
    }

    errorHandler(error: any): void {
        console.log(error)
    }
}