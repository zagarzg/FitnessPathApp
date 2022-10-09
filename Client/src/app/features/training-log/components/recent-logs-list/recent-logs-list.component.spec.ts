import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecentLogsListComponent } from './recent-logs-list.component';

describe('RecentLogsListComponent', () => {
  let component: RecentLogsListComponent;
  let fixture: ComponentFixture<RecentLogsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecentLogsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecentLogsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
