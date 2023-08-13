import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodTileComponent } from './food-tile.component';

describe('FoodTileComponent', () => {
  let component: FoodTileComponent;
  let fixture: ComponentFixture<FoodTileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoodTileComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoodTileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
