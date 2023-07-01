import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExerciseTileComponent } from './exercise-tile.component';

describe('ExerciseTileComponent', () => {
  let component: ExerciseTileComponent;
  let fixture: ComponentFixture<ExerciseTileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExerciseTileComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ExerciseTileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
