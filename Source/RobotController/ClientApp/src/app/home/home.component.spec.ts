import { ComponentFixture, TestBed } from '@angular/core/testing';
import { BehaviorSubject, of } from 'rxjs';
import { HomeComponent } from './home.component';
import { SignalrService } from '../api/signalr.service';
import { RobotCommandServiceApi } from '../api/robotCommand.service';
import { RobotCameraServiceApi } from '../api/robotCamera.service';
import { ConnectionEvent } from '../models/ConnectionEvent.model';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let signalrService: SignalrService;
  let robotCommandServiceSpy: jasmine.SpyObj<RobotCommandServiceApi>;
  let robotCameraServiceSpy: jasmine.SpyObj<RobotCameraServiceApi>;

  beforeEach(async () => {
    robotCommandServiceSpy = jasmine.createSpyObj('RobotCommandServiceApi', ['moveForward', 'moveBackward', 'moveLeft', 'moveRight', 'startRecording', 'stopRecording', 'playRecording']);
    robotCameraServiceSpy = jasmine.createSpyObj('RobotCameraServiceApi', ['moveUp', 'moveDown', 'moveLeft', 'moveRight', 'centerCamera']);
    robotCommandServiceSpy.moveForward.and.returnValue(of());
    robotCommandServiceSpy.moveBackward.and.returnValue(of());
    robotCommandServiceSpy.moveLeft.and.returnValue(of());
    robotCommandServiceSpy.moveRight.and.returnValue(of());
    robotCommandServiceSpy.startRecording.and.returnValue(of());
    robotCommandServiceSpy.stopRecording.and.returnValue(of());
    robotCommandServiceSpy.playRecording.and.returnValue(of());
    robotCameraServiceSpy.moveUp.and.returnValue(of());
    robotCameraServiceSpy.moveDown.and.returnValue(of());
    robotCameraServiceSpy.moveLeft.and.returnValue(of());
    robotCameraServiceSpy.moveRight.and.returnValue(of());
    robotCameraServiceSpy.centerCamera.and.returnValue(of());

    await TestBed.configureTestingModule({
      declarations: [HomeComponent],
      providers: [
        SignalrService,
        { provide: RobotCommandServiceApi, useValue: robotCommandServiceSpy },
        { provide: RobotCameraServiceApi, useValue: robotCameraServiceSpy },
        { provide: 'BASE_URL', useValue: 'http://localhost:4200/' }, // Proporciona un valor para BASE_URL
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    signalrService = TestBed.inject(SignalrService);
    signalrService.hubConnectionEvent = new BehaviorSubject(new ConnectionEvent({ isConnected: true, dateTime: new Date() }));
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call moveForward on RobotCommandServiceApi and set forwardKeyPressed to true when pressMoveForward is called and isConnected is true', () => {
    component.connection.isConnected = true;
    component.pressMoveForward();
    expect(robotCommandServiceSpy.moveForward).toHaveBeenCalled();
    expect(component.forwardKeyPressed).toBeTrue();
  });

  it('should not call moveForward on RobotCommandServiceApi and not change forwardKeyPressed when pressMoveForward is called and isConnected is false', () => {
    component.connection.isConnected = false;
    component.pressMoveForward();
    expect(robotCommandServiceSpy.moveForward).not.toHaveBeenCalled();
    expect(component.forwardKeyPressed).toBeFalse();
  });

  it('should call moveBackward on RobotCommandServiceApi and set backwardKeyPressed to true when pressMoveBackward is called and isConnected is true', () => {
    component.connection.isConnected = true;
    component.pressMoveBackward();
    expect(robotCommandServiceSpy.moveBackward).toHaveBeenCalled();
    expect(component.backwardKeyPressed).toBeTrue();
  });

  it('should not call moveBackward on RobotCommandServiceApi and not change backwardKeyPressed when pressMoveBackward is called and isConnected is false', () => {
    component.connection.isConnected = false;
    component.pressMoveBackward();
    expect(robotCommandServiceSpy.moveBackward).not.toHaveBeenCalled();
    expect(component.backwardKeyPressed).toBeFalse();
  });

  it('should call moveLeft on RobotCommandServiceApi and set leftKeyPressed to true when pressMoveLeft is called and isConnected is true', () => {
    component.connection.isConnected = true;
    component.pressMoveLeft();
    expect(robotCommandServiceSpy.moveLeft).toHaveBeenCalled();
    expect(component.leftKeyPressed).toBeTrue();
  });

  it('should not call moveLeft on RobotCommandServiceApi and not change leftKeyPressed when pressMoveLeft is called and isConnected is false', () => {
    component.connection.isConnected = false;
    component.pressMoveLeft();
    expect(robotCommandServiceSpy.moveLeft).not.toHaveBeenCalled();
    expect(component.leftKeyPressed).toBeFalse();
  });

  it('should call moveRight on RobotCommandServiceApi and set rigthKeyPressed to true when pressMoveRight is called and isConnected is true', () => {
    component.connection.isConnected = true;
    component.pressMoveRight();
    expect(robotCommandServiceSpy.moveRight).toHaveBeenCalled();
    expect(component.rigthKeyPressed).toBeTrue();
  });

  it('should not call moveRight on RobotCommandServiceApi and not change rigthKeyPressed when pressMoveRight is called and isConnected is false', () => {
    component.connection.isConnected = false;
    component.pressMoveRight();
    expect(robotCommandServiceSpy.moveRight).not.toHaveBeenCalled();
    expect(component.rigthKeyPressed).toBeFalse();
  });

  it('should call centerCamera on RobotCameraServiceApi and set camCenterKeyPressed to true when pressCamCenter is called and isConnected is true', () => {
    component.connection.isConnected = true;
    component.pressCamCenter();
    expect(robotCameraServiceSpy.centerCamera).toHaveBeenCalled();
    expect(component.camCenterKeyPressed).toBeTrue();
  });

  it('should not call centerCamera on RobotCameraServiceApi and not change camCenterKeyPressed when pressCamCenter is called and isConnected is false', () => {
    component.connection.isConnected = false;
    component.pressCamCenter();
    expect(robotCameraServiceSpy.centerCamera).not.toHaveBeenCalled();
    expect(component.camCenterKeyPressed).toBeFalse();
  });

  it('should call startRecording on RobotCommandServiceApi when startRecording is called and isConnected is true', () => {
    component.connection.isConnected = true;
    component.startRecording();
    expect(robotCommandServiceSpy.startRecording).toHaveBeenCalled();
  });

  it('should not call startRecording on RobotCommandServiceApi when startRecording is called and isConnected is false', () => {
    component.connection.isConnected = false;
    component.startRecording();
    expect(robotCommandServiceSpy.startRecording).not.toHaveBeenCalled();
  });

  it('should call stopRecording on RobotCommandServiceApi when stopRecording is called and isConnected is true', () => {
    component.connection.isConnected = true;
    component.stopRecording();
    expect(robotCommandServiceSpy.stopRecording).toHaveBeenCalled();
  });

  it('should not call stopRecording on RobotCommandServiceApi when stopRecording is called and isConnected is false', () => {
    component.connection.isConnected = false;
    component.stopRecording();
    expect(robotCommandServiceSpy.stopRecording).not.toHaveBeenCalled();
  });

  it('should call playRecording on RobotCommandServiceApi when playRecording is called and isConnected is true', () => {
    component.connection.isConnected = true;
    component.playRecording();
    expect(robotCommandServiceSpy.playRecording).toHaveBeenCalled();
  });

  it('should not call playRecording on RobotCommandServiceApi when playRecording is called and isConnected is false', () => {
    component.connection.isConnected = false;
    component.playRecording();
    expect(robotCommandServiceSpy.playRecording).not.toHaveBeenCalled();
  });

  it('should call moveUp on RobotCameraServiceApi when pressCamUp is called and isConnected is true', () => {
    component.connection.isConnected = true;
    component.pressCamMoveUp();
    expect(robotCameraServiceSpy.moveUp).toHaveBeenCalled();
  });

  it('should not call moveUp on RobotCameraServiceApi when pressCamUp is called and isConnected is false', () => {
    component.connection.isConnected = false;
    component.pressCamMoveUp();
    expect(robotCameraServiceSpy.moveUp).not.toHaveBeenCalled();
  });

  it('should call moveDown on RobotCameraServiceApi when pressCamDown is called and isConnected is true', () => {
    component.connection.isConnected = true;
    component.pressCamMoveDown();
    expect(robotCameraServiceSpy.moveDown).toHaveBeenCalled();
  });

  it('should not call moveDown on RobotCameraServiceApi when pressCamDown is called and isConnected is false', () => {
    component.connection.isConnected = false;
    component.pressCamMoveDown();
    expect(robotCameraServiceSpy.moveDown).not.toHaveBeenCalled();
  });

  it('should call moveLeft on RobotCameraServiceApi when pressCamLeft is called and isConnected is true', () => {
    component.connection.isConnected = true;
    component.pressCamMoveLeft();
    expect(robotCameraServiceSpy.moveLeft).toHaveBeenCalled();
  });

  it('should not call moveLeft on RobotCameraServiceApi when pressCamLeft is called and isConnected is false', () => {
    component.connection.isConnected = false;
    component.pressCamMoveLeft();
    expect(robotCameraServiceSpy.moveLeft).not.toHaveBeenCalled();
  });

  it('should call moveRight on RobotCameraServiceApi when pressCamRight is called and isConnected is true', () => {
    component.connection.isConnected = true;
    component.pressCamMoveRight();
    expect(robotCameraServiceSpy.moveRight).toHaveBeenCalled();
  });

  it('should not call moveRight on RobotCameraServiceApi when pressCamRight is called and isConnected is false', () => {
    component.connection.isConnected = false;
    component.pressCamMoveRight();
    expect(robotCameraServiceSpy.moveRight).not.toHaveBeenCalled();
  });
});
