import { Inject, Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { ConnectionEvent } from '../models/ConnectionEvent.model';

@Injectable({
    providedIn: 'root'
})
export class SignalrService {
    hubUrl: string;
    connection: any;
    hubConnectionEvent: BehaviorSubject<ConnectionEvent>;

    constructor(@Inject('BASE_URL') private baseUrl: string) {
       // this.hubUrl = this.baseUrl + 'hub';
         this.hubUrl = 'https://localhost:7018/hub';
        this.hubConnectionEvent = new BehaviorSubject<ConnectionEvent>(new ConnectionEvent());
    }

    public async initiateSignalrConnection(): Promise<void> {
        try {
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl(this.hubUrl)
                .withAutomaticReconnect()
                .build();

            await this.connection.start({ withCredentials: false });
            this.setSignalrClientMethods();

            console.log(`SignalR connection success! connectionId: ${this.connection.connectionId}`);
        }
        catch (error) {
            console.log(`SignalR connection error: ${error}`);
        }
    }

    private setSignalrClientMethods(): void {
        this.connection.on('ConnectionEvent', (message: ConnectionEvent) => {
            console.log(message);
            this.hubConnectionEvent.next(message);
        });
    }
}