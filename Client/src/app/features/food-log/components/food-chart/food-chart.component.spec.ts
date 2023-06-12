import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodChartComponent } from './food-chart.component';

describe('FoodChartComponent', () => {
  let component: FoodChartComponent;
  let fixture: ComponentFixture<FoodChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoodChartComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoodChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
