import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NavMenuComponent } from './nav-menu.component';

describe('NavMenuComponent', () => {
  let component: NavMenuComponent;
  let fixture: ComponentFixture<NavMenuComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NavMenuComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NavMenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set isExpanded to false when collapse is called', () => {
    component.isExpanded = true; // Asegurarse de que isExpanded es true antes de llamar a collapse.
    component.collapse();
    expect(component.isExpanded).toBeFalse();
  });

  it('should toggle isExpanded when toggle is called', () => {
    component.isExpanded = true;
    component.toggle();
    expect(component.isExpanded).toBeFalse();
    component.toggle();
    expect(component.isExpanded).toBeTrue();
  });
});
