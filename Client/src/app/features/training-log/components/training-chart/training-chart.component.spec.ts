import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrainingChartComponent } from './training-chart.component';

describe('TrainingChartComponent', () => {
  let component: TrainingChartComponent;
  let fixture: ComponentFixture<TrainingChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TrainingChartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrainingChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
