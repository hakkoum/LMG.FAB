import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from "rxjs/Subject";

@Injectable()
export class TitleService {

    constructor() {
    }

    public Title: string;

    SetTitle(title: string) {
        this.Title = title;
    }
}