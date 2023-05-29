import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodLogPageComponent } from './food-log-page.component';

describe('FoodLogPageComponent', () => {
  let component: FoodLogPageComponent;
  let fixture: ComponentFixture<FoodLogPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoodLogPageComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoodLogPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
