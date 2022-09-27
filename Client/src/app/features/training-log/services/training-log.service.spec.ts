import { TestBed } from '@angular/core/testing';

import { TrainingLogService } from './training-log.service';

describe('TrainingLogService', () => {
  let service: TrainingLogService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TrainingLogService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
