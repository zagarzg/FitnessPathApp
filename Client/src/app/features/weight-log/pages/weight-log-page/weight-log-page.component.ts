import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { EMPTY } from 'rxjs';
import { switchMap, take } from 'rxjs/operators';
import {
  ChartService,
  WeightChartFilters,
} from 'src/app/features/training-log/services/chart.service';
import { DeleteConfirmationDialogComponent } from 'src/app/shared/components/delete-confirmation-dialog/delete-confirmation-dialog.component';
import { WeightLog } from '../../models/WeightLog';
import { WeightLogService } from '../../services/weight-log.service';

@Component({
  selector: 'app-weight-log-page',
  templateUrl: './weight-log-page.component.html',
  styleUrls: ['./weight-log-page.component.scss'],
})
export class WeightLogPageComponent implements OnInit {
  readonly weightLogs$ = this._weightLogService.weightLogs$;
  readonly selectedWeightLogId$ = this._weightLogService.selectedWeightLogId$;
  readonly weightChartData$ = this._chartService.weightChartData$;

  public selectedDate!: Date | null;
  public selectedWeightLog!: WeightLog | undefined;

  constructor(
    private _weightLogService: WeightLogService,
    private _chartService: ChartService,
    private _toasterService: ToastrService,
    private _dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this._weightLogService.getAllWeightLogs().pipe(take(1)).subscribe();
    this.weightLogs$
      .pipe(switchMap(() => this._chartService.fetchWeightChartData()))
      .subscribe();
  }

  onAdd(log: WeightLog) {
    this._weightLogService
      .createWeightLog(log)
      .pipe(take(1))
      .subscribe((log) => {
        this.selectedDate = log.date;
        this._toasterService.success('Weight log successfully added!');
      });
  }

  onUpdate(log: WeightLog) {
    this._weightLogService
      .updateWeightLog(log)
      .pipe(take(1))
      .subscribe((updatedLog: any) => {
        this.selectedDate = log.date;
        this.selectedWeightLog = updatedLog;
        this._toasterService.success('Weight log successfully updated!');
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
        this.selectedWeightLog = undefined;
        this._toasterService.success('Weight log successfully deleted!');
      });
  }

  onChartFiltersChange(weightChartFilters: WeightChartFilters) {
    this._chartService
      .fetchWeightChartData(weightChartFilters)
      .pipe(take(1))
      .subscribe();
  }
}
