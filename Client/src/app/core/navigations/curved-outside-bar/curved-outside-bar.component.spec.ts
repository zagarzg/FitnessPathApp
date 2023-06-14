import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurvedOutsideBarComponent } from './curved-outside-bar.component';

describe('CurvedOutsideBarComponent', () => {
  let component: CurvedOutsideBarComponent;
  let fixture: ComponentFixture<CurvedOutsideBarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CurvedOutsideBarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CurvedOutsideBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
