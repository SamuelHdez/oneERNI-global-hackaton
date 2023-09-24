import { Component, HostListener } from '@angular/core';
import { SignalrService } from '../api/signalr.service';
import { ConnectionEvent } from '../models/ConnectionEvent.model';
import { RobotCommandServiceApi } from '../api/robotCommand.service';
import { debounceTime } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {

  public connection: ConnectionEvent = new ConnectionEvent({ isConnected: false, dateTime: new Date() });
  public forwardKeyPressed: boolean = false;
  public backwardKeyPressed: boolean = false;
  public leftKeyPressed: boolean = false;
  public rigthKeyPressed: boolean = false;

  constructor(private readonly signalrService: SignalrService, private readonly robotCommandService: RobotCommandServiceApi) {
    this.signalrService.hubConnectionEvent.subscribe((hubConnectionEvent: ConnectionEvent) => {
      this.connection = hubConnectionEvent;
    });
  }

  public getConnectionStatus(): string {
    return this.connection.isConnected ? 'Connected' : 'Disconnected';
  }

  @HostListener('window:keydown', ['$event'])
  keyDown(event: KeyboardEvent) {
    if (event.key === 'w' || event.key === 'W' || event.key === 'ArrowUp') {
      this.pressMoveForward();
    }
    if (event.key === 's' || event.key === 'S' || event.key === 'ArrowDown') {
      this.pressMoveBackward();
    }
    if (event.key === 'a' || event.key === 'A' || event.key === 'ArrowLeft') {
      this.pressMoveLeft();
    }
    if (event.key === 'd' || event.key === 'D' || event.key === 'ArrowRight') {
      this.pressMoveRight();
    }
  }

  @HostListener('window:keyup', ['$event'])
  keyUp(event: KeyboardEvent) {
    if (event.key === 'w' || event.key === 'W' || event.key === 'ArrowUp') {
      this.forwardKeyPressed = false;
    }
    if (event.key === 's' || event.key === 'S' || event.key === 'ArrowDown') {
      this.backwardKeyPressed = false;
    }
    if (event.key === 'a' || event.key === 'A' || event.key === 'ArrowLeft') {
      this.leftKeyPressed = false;
    }
    if (event.key === 'd' || event.key === 'D' || event.key === 'ArrowRight') {
      this.rigthKeyPressed = false;
    }
  }

  pressMoveForward() {
    this.forwardKeyPressed = true;
    this.robotCommandService.moveForward().pipe(debounceTime(1000)).subscribe(() => {
      console.log('moveForward()');
    });
  }

  pressMoveBackward() {
    this.backwardKeyPressed = true;
    this.robotCommandService.moveBackward().pipe(debounceTime(1000)).subscribe(() => {
      console.log('moveBackward()');
    });
  }

  pressMoveLeft() {
    this.leftKeyPressed = true;
    this.robotCommandService.moveLeft().pipe(debounceTime(1000)).subscribe(() => {
      console.log('moveLeft()');
    });
  }

  pressMoveRight() {
    this.rigthKeyPressed = true;
    this.robotCommandService.moveRight().pipe(debounceTime(1000)).subscribe(() => {
      console.log('moveRight()');
    });
  }
}
