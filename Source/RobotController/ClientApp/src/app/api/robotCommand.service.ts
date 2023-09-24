import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class RobotCommandServiceApi {
    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string
    ) {
        this.baseUrl += 'api/robotcommand/';
    }

    moveForward(): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}forward`, { speed: 100 });
    }

    moveBackward(): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}backward`, { speed: 100 });
    }

    moveLeft(): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}left`, { angle: 100 });
    }

    moveRight(): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}right`, { angle: 100 });
    }

    startRecording(): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}start-recording`, {});
    }

    stopRecording(): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}end-recording`, {});
    }

    playRecording(): Observable<void> {
        return this.http.post<void>(`${this.baseUrl}play-last-recorded`, {});
    }

}
