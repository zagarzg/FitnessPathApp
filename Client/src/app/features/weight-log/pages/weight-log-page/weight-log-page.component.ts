import { Component, OnInit, ViewChild } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { take } from 'rxjs/operators';
import { WeightChartComponent } from '../../components/weight-chart/weight-chart.component';
import { WeightLogListComponent } from '../../components/weight-log-list/weight-log-list.component';
import { WeightLog } from '../../models/WeightLog';
import { WeightLogService } from '../../services/weight-log.service';

@Component({
  selector: 'app-weight-log-page',
  templateUrl: './weight-log-page.component.html',
  styleUrls: ['./weight-log-page.component.scss'],
})
export class WeightLogPageComponent implements OnInit {
  private logs: WeightLog[] = [];
  private weightLogsSubject$: Subject<WeightLog[]> = new Subject<WeightLog[]>();
  public weightLogs$: Observable<WeightLog[]> =
    this.weightLogsSubject$.asObservable();

  public selectedDate!: Date | null;
  public selectedWeightLog!: WeightLog | undefined;

  @ViewChild(WeightLogListComponent) listComponent!: WeightLogListComponent;
  @ViewChild(WeightChartComponent) chartComponent!: WeightChartComponent;

  constructor(private _weightLogService: WeightLogService) {}

  ngOnInit(): void {
    this._weightLogService
      .getAllWeightLogs()
      .pipe(take(1))
      .subscribe((logs) => {
        this.logs = logs;
        this.weightLogsSubject$.next(logs);
      });
  }

  onAdd(log: WeightLog) {
    this._weightLogService
      .createWeightLog(log)
      .pipe(take(1))
      .subscribe((log) => {
        this.logs = [...this.logs, log];
        // this.logs.push(log);
        this.weightLogsSubject$.next(this.logs);
        this.selectedDate = log.date;
        this.selectedWeightLog = log;
        this.chartComponent.monthChange(2);
      });
  }

  onUpdate(log: WeightLog) {
    this._weightLogService
      .updateWeightLog(log)
      .pipe(take(1))
      .subscribe((_) => {
        const logIndex = this.logs.findIndex((obj) => obj.id == log.id);
        this.logs[logIndex] = log;
        this.weightLogsSubject$.next(this.logs);
        this.selectedDate = log.date;
        this.selectedWeightLog = log;
        this.chartComponent.monthChange(2);
      });
  }

  onGet(log: WeightLog | null) {
    this.selectedWeightLog = log ? log : undefined;
    this.selectedDate = log ? log.date : null;
  }

  onDelete(id: string): void {
    this._weightLogService
      .deleteWeightLog(id)
      .pipe(take(1))
      .subscribe((_) => {
        this.logs = this.logs.filter((log) => log.id !== id);
        this.weightLogsSubject$.next(this.logs);
        this.selectedWeightLog = undefined;
        this.chartComponent.monthChange(2);
      });
  }
}
