import { Component, HostListener } from '@angular/core';
import { SignalrService } from '../api/signalr.service';
import { ConnectionEvent } from '../models/ConnectionEvent.model';
import { RobotCommandServiceApi } from '../api/robotCommand.service';
import { debounceTime } from 'rxjs';
import { RobotCameraServiceApi } from '../api/robotCamera.service';

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

  public camUpKeyPressed: boolean = false;
  public camDownKeyPressed: boolean = false;
  public camLeftKeyPressed: boolean = false;
  public camRightKeyPressed: boolean = false;
  public camCenterKeyPressed: boolean = false;

  constructor(
    private readonly signalrService: SignalrService,
    private readonly robotCommandService: RobotCommandServiceApi,
    private readonly robotCameraService: RobotCameraServiceApi) {
    this.signalrService.hubConnectionEvent.subscribe((hubConnectionEvent: ConnectionEvent) => {
      this.connection = hubConnectionEvent;
    });
  }

  public getConnectionStatus(): string {
    return this.connection.isConnected ? 'Connected' : 'Disconnected';
  }

  @HostListener('window:keydown', ['$event'])
  keyDown(event: KeyboardEvent) {
    if (event.key === 'w' || event.key === 'W') {
      this.pressMoveForward();
    }
    if (event.key === 's' || event.key === 'S') {
      this.pressMoveBackward();
    }
    if (event.key === 'a' || event.key === 'A') {
      this.pressMoveLeft();
    }
    if (event.key === 'd' || event.key === 'D') {
      this.pressMoveRight();
    }

    if (event.key === 'ArrowUp') {
      this.pressCamMoveUp();
    }
    if (event.key === 'ArrowDown') {
      this.pressCamMoveDown();
    }
    if (event.key === 'ArrowLeft') {
      this.pressCamMoveLeft();
    }
    if (event.key === 'ArrowRight') {
      this.pressCamMoveRight();
    }
    if (event.key === ' ' || event.key === 'Spacebar' || event.key === 'Space') {
      this.pressCamCenter();
    }
  }

  @HostListener('window:keyup', ['$event'])
  keyUp(event: KeyboardEvent) {
    if (event.key === 'w' || event.key === 'W') {
      this.forwardKeyPressed = false;
    }
    if (event.key === 's' || event.key === 'S') {
      this.backwardKeyPressed = false;
    }
    if (event.key === 'a' || event.key === 'A') {
      this.leftKeyPressed = false;
    }
    if (event.key === 'd' || event.key === 'D') {
      this.rigthKeyPressed = false;
    }

    if (event.key === 'ArrowUp') {
      this.camUpKeyPressed = false;
    }
    if (event.key === 'ArrowDown') {
      this.camDownKeyPressed = false;
    }
    if (event.key === 'ArrowLeft') {
      this.camLeftKeyPressed = false;
    }
    if (event.key === 'ArrowRight') {
      this.camRightKeyPressed = false;
    }
    if (event.key === ' ' || event.key === 'Spacebar' || event.key === 'Space') {
      this.camCenterKeyPressed = false;
    }
  }

  pressCamMoveUp() {
    this.camUpKeyPressed = true;
    this.robotCameraService.moveUp().pipe(debounceTime(1000)).subscribe(() => {
      console.log('camMoveUp()');
    });
  }

  pressCamMoveDown() {
    this.camDownKeyPressed = true;
    this.robotCameraService.moveDown().pipe(debounceTime(1000)).subscribe(() => {
      console.log('camMoveDown()');
    });
  }

  pressCamMoveLeft() {
    this.camLeftKeyPressed = true;
    this.robotCameraService.moveLeft().pipe(debounceTime(1000)).subscribe(() => {
      console.log('camMoveLeft()');
    });
  }

  pressCamMoveRight() {
    this.camRightKeyPressed = true;
    this.robotCameraService.moveRight().pipe(debounceTime(1000)).subscribe(() => {
      console.log('camMoveRight()');
    });
  }

  pressCamCenter() {
    this.camCenterKeyPressed = true;
    this.robotCameraService.centerCamera().pipe(debounceTime(1000)).subscribe(() => {
      console.log('camCenter()');
    });
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
