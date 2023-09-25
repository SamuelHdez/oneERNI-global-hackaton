import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { AboutServiceApi } from './about.service';
import { TeamMember } from '../models/TeamMember.model';

describe('AboutServiceApi', () => {
    let service: AboutServiceApi;
    let httpTestingController: HttpTestingController;
    const baseUrl = 'http://localhost:4200/';

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                AboutServiceApi,
                { provide: 'BASE_URL', useValue: baseUrl }
            ]
        });

        service = TestBed.inject(AboutServiceApi);
        httpTestingController = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpTestingController.verify(); // Verifica que no haya solicitudes pendientes.
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should retrieve all team members', () => {
        const mockTeamMembers: TeamMember[] = [
            {
                name: 'John Doe',
                description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla quam velit, vulputate eu pharetra nec, mattis ac neque.',
                nickname: 'John',
                avatar: 'https://via.placeholder.com/150'
            }
        ];

        service.getAllTeamMembers().subscribe(teamMembers => {
            expect(teamMembers).toEqual(mockTeamMembers);
        });

        const req = httpTestingController.expectOne(`${baseUrl}api/about/TeamMembers`);
        expect(req.request.method).toEqual('GET');
        req.flush(mockTeamMembers);
    });
});
