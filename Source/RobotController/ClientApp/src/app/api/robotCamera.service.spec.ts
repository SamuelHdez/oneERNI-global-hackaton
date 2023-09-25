import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { RobotCameraServiceApi } from './robotCamera.service';

describe('RobotCameraServiceApi', () => {
    let service: RobotCameraServiceApi;
    let httpTestingController: HttpTestingController;
    const baseUrl = 'http://localhost:4200/';

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                RobotCameraServiceApi,
                { provide: 'BASE_URL', useValue: baseUrl }
            ]
        });

        service = TestBed.inject(RobotCameraServiceApi);
        httpTestingController = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpTestingController.verify();
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should move the camera up', () => {
        service.moveUp().subscribe();
        const req = httpTestingController.expectOne(`${baseUrl}api/robotcamera/up`);
        expect(req.request.method).toEqual('POST');
        expect(req.request.body).toEqual({ speed: 100 });
        req.flush({});
    });

    it('should move the camera down', () => {
        service.moveDown().subscribe();
        const req = httpTestingController.expectOne(`${baseUrl}api/robotcamera/down`);
        expect(req.request.method).toEqual('POST');
        expect(req.request.body).toEqual({ speed: 100 });
        req.flush({});
    });

    it('should move the camera left', () => {
        service.moveLeft().subscribe();
        const req = httpTestingController.expectOne(`${baseUrl}api/robotcamera/left`);
        expect(req.request.method).toEqual('POST');
        expect(req.request.body).toEqual({ angle: 100 });
        req.flush({});
    });

    it('should move the camera right', () => {
        service.moveRight().subscribe();
        const req = httpTestingController.expectOne(`${baseUrl}api/robotcamera/right`);
        expect(req.request.method).toEqual('POST');
        expect(req.request.body).toEqual({ angle: 100 });
        req.flush({});
    });

    it('should center the camera', () => {
        service.centerCamera().subscribe();
        const req = httpTestingController.expectOne(`${baseUrl}api/robotcamera/center`);
        expect(req.request.method).toEqual('POST');
        expect(req.request.body).toEqual({});
        req.flush({});
    });
});
