import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WeightTileComponent } from './weight-tile.component';

describe('WeightTileComponent', () => {
  let component: WeightTileComponent;
  let fixture: ComponentFixture<WeightTileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WeightTileComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WeightTileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
