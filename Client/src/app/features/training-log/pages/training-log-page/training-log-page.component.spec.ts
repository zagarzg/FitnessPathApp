import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingLogPageComponent } from './training-log-page.component';

describe('TrainingLogPageComponent', () => {
  let component: TrainingLogPageComponent;
  let fixture: ComponentFixture<TrainingLogPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingLogPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainingLogPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
