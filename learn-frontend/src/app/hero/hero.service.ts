import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, map } from "rxjs";


@Injectable({
    providedIn: 'root'
})

export class HeroService{
    private apiUrl = 'http://localhost:5263/api/profile'; 

    constructor(private http:HttpClient){}

    getProfile():Observable<any>{
        return this.http.get<any>(this.apiUrl).pipe(
            map(response=>response?.data?.$values?.[0]||null)
        );
    }
}
