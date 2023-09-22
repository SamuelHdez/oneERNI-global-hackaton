import { Component, Inject, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { SignalrService } from './api/signalr.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'app';
  hubHelloMessage?: string;

  constructor(public signalrService: SignalrService) {
    this.hubHelloMessage = '';
    
  }

  ngOnInit(): void { 
    this.signalrService.connection
      .invoke('Hello')
      .catch((error: any) => {
        console.log(`SignalrDemoHub.Hello() error: ${error}`);
        alert('SignalrDemoHub.Hello() error!, see console for details.');
      }
    );
  }
}
