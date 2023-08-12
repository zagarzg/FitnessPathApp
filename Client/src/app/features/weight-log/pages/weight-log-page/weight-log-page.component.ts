import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, EMPTY, Observable } from 'rxjs';
import { take, switchMap } from 'rxjs/operators';
import { DeleteConfirmationDialogComponent } from 'src/app/shared/components/delete-confirmation-dialog/delete-confirmation-dialog.component';
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
  private weightLogsSubject$: BehaviorSubject<WeightLog[]> =
    new BehaviorSubject<WeightLog[]>([]);
  public weightLogs$: Observable<WeightLog[]> =
    this.weightLogsSubject$.asObservable();

  public selectedDate!: Date | null;
  public selectedWeightLog!: WeightLog | undefined;

  @ViewChild(WeightLogListComponent) listComponent!: WeightLogListComponent;
  @ViewChild(WeightChartComponent) chartComponent!: WeightChartComponent;

  constructor(
    private _weightLogService: WeightLogService,
    private _toasterService: ToastrService,
    private _dialog: MatDialog
  ) {}

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
        const updatedLogs = [...this.weightLogsSubject$.value, log];
        this.weightLogsSubject$.next(updatedLogs);
        this.selectedDate = log.date;
        this.selectedWeightLog = log;
        this._toasterService.success('Weight log successfully added!');
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
        this._toasterService.success('Weight log successfully updated!');
        this.chartComponent.monthChange(2);
      });
  }

  onGet(log: WeightLog | null) {
    this.selectedWeightLog = log ? log : undefined;
    this.selectedDate = log ? log.date : null;
  }

  onDelete(id: string): void {
    const dialogRef = this._dialog.open(DeleteConfirmationDialogComponent, {
      data: {
        date: this.selectedDate,
      },
    });

    dialogRef
      .afterClosed()
      .pipe(
        switchMap((confirm: boolean) => {
          if (confirm) return this._weightLogService.deleteWeightLog(id);
          return EMPTY;
        }),
        take(1)
      )
      .subscribe((_) => {
        const updatedLogs = this.weightLogsSubject$.value.filter(
          (obj) => obj.id !== id
        );
        this.weightLogsSubject$.next(updatedLogs);
        this.selectedWeightLog = undefined;
        this._toasterService.success('Weight log successfully deleted!');
        this.chartComponent.monthChange(2);
      });
  }
}
