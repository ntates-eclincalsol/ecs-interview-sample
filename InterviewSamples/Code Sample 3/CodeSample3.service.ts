import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Sample3User } from './CodeSample3.interface';

@Injectable({
    providedIn: 'root'
})
export class Sample3UserService {
    apiUrl = 'http://localhost:3000/users';

    constructor(private http: HttpClient) { }

    getUsers(): Observable<Sample3User[]> {
        return this.http.get<Sample3User[]>(this.apiUrl);
    }

    disableUser(userId: number): Observable<Sample3User> {
        return this.http.post<Sample3User>(`${this.apiUrl}/${userId}/disable`, null);
    }
}
