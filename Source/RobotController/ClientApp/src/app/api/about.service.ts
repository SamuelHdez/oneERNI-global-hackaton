import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TeamMember } from '../models/TeamMember.model';

@Injectable({
    providedIn: 'root',
})
export class AboutServiceApi {
    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string
    ) {
        this.baseUrl += 'api/about/';
    }

    getAllTeamMembers(): Observable<TeamMember[]> {
        return this.http.get<TeamMember[]>(`${this.baseUrl}TeamMembers`);
    }
}
