import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class RobotCameraServiceApi {
    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string
    ) {
        this.baseUrl += 'api/robotcamera/';
    }

    moveUp(): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}up`, { speed: 100 });
    }

    moveDown(): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}down`, { speed: 100 });
    }

    moveLeft(): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}left`, { angle: 100 });
    }

    moveRight(): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}right`, { angle: 100 });
    }

    centerCamera(): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}center`, {});
    }
}
