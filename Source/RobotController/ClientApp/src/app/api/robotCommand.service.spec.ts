import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { RobotCommandServiceApi } from './robotCommand.service';

describe('RobotCameraServiceApi', () => {
    let service: RobotCommandServiceApi;
    let httpTestingController: HttpTestingController;
    const baseUrl = 'http://localhost:4200/';

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                RobotCommandServiceApi,
                { provide: 'BASE_URL', useValue: baseUrl }
            ]
        });

        service = TestBed.inject(RobotCommandServiceApi);
        httpTestingController = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpTestingController.verify();
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should move the robot forward', () => {
        service.moveForward().subscribe();
        const req = httpTestingController.expectOne(`${baseUrl}api/robotcommand/forward`);
        expect(req.request.method).toEqual('POST');
        expect(req.request.body).toEqual({ speed: 100 });
        req.flush({});
    });

    it('should move the robot backward', () => {
        service.moveBackward().subscribe();
        const req = httpTestingController.expectOne(`${baseUrl}api/robotcommand/backward`);
        expect(req.request.method).toEqual('POST');
        expect(req.request.body).toEqual({ speed: 100 });
        req.flush({});
    });

    it('should move the robot left', () => {
        service.moveLeft().subscribe();
        const req = httpTestingController.expectOne(`${baseUrl}api/robotcommand/left`);
        expect(req.request.method).toEqual('POST');
        expect(req.request.body).toEqual({ angle: 100 });
        req.flush({});
    });

    it('should move the robot right', () => {
        service.moveRight().subscribe();
        const req = httpTestingController.expectOne(`${baseUrl}api/robotcommand/right`);
        expect(req.request.method).toEqual('POST');
        expect(req.request.body).toEqual({ angle: 100 });
        req.flush({});
    });

    it('should start recording', () => {
        service.startRecording().subscribe();
        const req = httpTestingController.expectOne(`${baseUrl}api/robotcommand/start-recording`);
        expect(req.request.method).toEqual('POST');
        expect(req.request.body).toEqual({});
        req.flush({});
    });

    it('should stop recording', () => {
        service.stopRecording().subscribe();
        const req = httpTestingController.expectOne(`${baseUrl}api/robotcommand/end-recording`);
        expect(req.request.method).toEqual('POST');
        expect(req.request.body).toEqual({});
        req.flush({});
    });

    it('should play last recorded', () => {
        service.playRecording().subscribe();
        const req = httpTestingController.expectOne(`${baseUrl}api/robotcommand/play-last-recorded`);
        expect(req.request.method).toEqual('POST');
        expect(req.request.body).toEqual({});
        req.flush({});
    });
});
