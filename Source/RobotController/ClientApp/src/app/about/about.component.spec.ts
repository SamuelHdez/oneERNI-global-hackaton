import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { AboutComponent } from './about.component';
import { AboutServiceApi } from '../api/about.service';
import { TeamMember } from '../models/TeamMember.model';

describe('AboutComponent', () => {
  let component: AboutComponent;
  let fixture: ComponentFixture<AboutComponent>;
  let aboutService: jasmine.SpyObj<AboutServiceApi>;
  let teamMembers: TeamMember[];
  beforeEach(async () => {
    const aboutServiceSpy = jasmine.createSpyObj('AboutServiceApi', ['getAllTeamMembers']);

    await TestBed.configureTestingModule({
      declarations: [AboutComponent],
      providers: [
        { provide: AboutServiceApi, useValue: aboutServiceSpy }
      ]
    })
      .compileComponents();

    aboutService = TestBed.inject(AboutServiceApi) as jasmine.SpyObj<AboutServiceApi>;
  });

  beforeEach(() => {
    teamMembers = [
      {
        name: 'John Doe',
        description: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla quam velit, vulputate eu pharetra nec, mattis ac neque.',
        nickname: 'John',
        avatar: 'https://via.placeholder.com/150'
      }
    ];

    aboutService.getAllTeamMembers.and.returnValue(of(teamMembers));

    fixture = TestBed.createComponent(AboutComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load team members on init', () => {
    fixture.detectChanges();

    expect(component.teamMembers).toEqual(teamMembers);
  });
});
