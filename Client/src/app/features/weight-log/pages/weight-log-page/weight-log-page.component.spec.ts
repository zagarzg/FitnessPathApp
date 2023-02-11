import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeightLogPageComponent } from './weight-log-page.component';

describe('WeightLogPageComponent', () => {
  let component: WeightLogPageComponent;
  let fixture: ComponentFixture<WeightLogPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WeightLogPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WeightLogPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
