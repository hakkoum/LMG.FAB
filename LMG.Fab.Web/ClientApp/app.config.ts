import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';

@Injectable()
export class AppConfig {

    private cache :any = {};

    constructor() { }

    public loadInstance(jsonFile: string, type: string) {
        return new Promise((resolve, reject) => {
            var xobj = new XMLHttpRequest();
            xobj.overrideMimeType("application/json");    

            xobj.open('GET', jsonFile, true);
            xobj.onreadystatechange = () => {
                if (xobj.readyState == 4) {
                    if (xobj.status == 200) {
                        this.cache[type] = JSON.parse(xobj.responseText);
                        resolve();
                    }
                    else {
                        reject(`Could not load file '${jsonFile}': ${xobj.status}`);
                    }
                }
            }
            xobj.send(null);
        });
    }

    public getlabel(key: string) {
        if (this.cache == null ||this.cache['labels'] == null) {
            return null;
        }
        if (key in this.cache['labels']) {
            return this.cache['labels'][key];
        }
        return key;
    }

    public get(cacheName: string, key: string) {
        if (this.cache == null || this.cache[cacheName] == null) {
            return null;
        }
        if (key in this.cache[cacheName]) {
            return this.cache[cacheName][key];
        }
        return null;
    }

    public getLevel2Key(cacheName: string, key1: string, key2: string) {
        let parent = this.get(cacheName, key1);
        if (parent == null) {
            return null;
        }
        if (key2 in parent) {
            return parent[key2];
        }
        return null;
    }


    public getObject(cacheName: string) {
        if (this.cache == null || this.cache[cacheName] == null) {
            return null;
        }
        return this.cache[cacheName];
    }
}