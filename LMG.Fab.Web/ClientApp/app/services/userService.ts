import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from "rxjs/Subject";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Subscription } from "rxjs";

import { UserProfil } from "../models/UserProfil";

@Injectable()
export class UserService {

    constructor(private httpClient: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    getUserProfil(): Observable<UserProfil> {
        return this.httpClient.get<UserProfil>(this.baseUrl + '/api/user/userProfil')
            .catch(this.handleError);
    }


    private handleError(error: Response) {
        return Observable.throw(error);
    }
}