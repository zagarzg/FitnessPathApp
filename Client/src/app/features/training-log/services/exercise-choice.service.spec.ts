import { TestBed } from '@angular/core/testing';

import { ExerciseChoiceService } from './exercise-choice.service';

describe('ExerciseChoiceService', () => {
  let service: ExerciseChoiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExerciseChoiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
