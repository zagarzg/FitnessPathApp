import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeightLogListComponent } from './weight-log-list.component';

describe('WeightLogListComponent', () => {
  let component: WeightLogListComponent;
  let fixture: ComponentFixture<WeightLogListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WeightLogListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WeightLogListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
