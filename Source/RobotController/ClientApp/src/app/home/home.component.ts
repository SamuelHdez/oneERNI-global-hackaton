import { Component } from '@angular/core';
import { SignalrService } from '../api/signalr.service';
import { ConnectionEvent } from '../models/ConnectionEvent.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {

  public connection: ConnectionEvent = new ConnectionEvent({ isConnected: false, dateTime: new Date() });

  constructor(private readonly signalrService: SignalrService) {
    this.signalrService.hubConnectionEvent.subscribe((hubConnectionEvent: ConnectionEvent) => {
      this.connection = hubConnectionEvent;
    });
  }

  public getConnectionStatus(): string {
    return this.connection.isConnected ? 'Connected' : 'Disconnected';
  }
}
