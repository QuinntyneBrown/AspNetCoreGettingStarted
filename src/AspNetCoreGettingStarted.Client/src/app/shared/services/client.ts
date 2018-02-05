import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";

@Injectable()
export class Client {
    constructor(private _inner: HttpClient) { }

    public get<T>(url: string) {        
        return this.send("GET", url);
    }

    public post<T>(url: string, body:any) {        
        return this.send(url, body);
    }

    private send(method: string, url: string, body: any = null) {
        const headers = new HttpHeaders().set('Content-Type', 'application/json');        
        return this._inner.request(method, url, { headers, body });
    }
}