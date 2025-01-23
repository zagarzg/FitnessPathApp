import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, EMPTY, forkJoin, Observable, of } from 'rxjs';
import {
  take,
  switchMap,
  distinctUntilChanged,
  catchError,
} from 'rxjs/operators';
import { DeleteConfirmationDialogComponent } from 'src/app/shared/components/delete-confirmation-dialog/delete-confirmation-dialog.component';
import { Exercise } from '../../models/Exercise';
import {
  ChartService,
  TrainingChartFilters,
} from '../../services/chart.service';
import { ExerciseChoiceService } from '../../services/exercise-choice.service';
import { ExerciseService } from '../../services/exercise.service';
import { TrainingLogService } from '../../services/training-log.service';

@Component({
  selector: 'app-training-log-page',
  templateUrl: './training-log-page.component.html',
  styleUrls: ['./training-log-page.component.scss'],
})
export class TrainingLogPageComponent implements OnInit {
  readonly trainingLogs$ = this._trainingLogService.trainingLogs$;
  readonly selectedTrainingLogId$ =
    this._trainingLogService.selectedTrainingLogId$;
  readonly exercises$ = this._exerciseService.exercises$;
  readonly trainingChartData$ = this._chartService.trainingChartData$;
  readonly exerciseChoices$ = this._exerciseChoiceService.exerciseChoices$;

  private readonly _selectedDate$ = new BehaviorSubject<Date | null>(
    new Date()
  );

  constructor(
    private _trainingLogService: TrainingLogService,
    private _chartService: ChartService,
    private _exerciseService: ExerciseService,
    private _exerciseChoiceService: ExerciseChoiceService,
    private _toasterService: ToastrService,
    private _dialog: MatDialog
  ) {}

  ngOnInit(): void {
    forkJoin([
      this._exerciseChoiceService.getAllExerciseNames(),
      this._trainingLogService.getAllTrainingLogDates(),
    ])
      .pipe(take(1), distinctUntilChanged())
      .subscribe(([exerciseChoices, trainingLogs]) => {
        console.log(trainingLogs);
      });
  }

  fetchExercises(date: Date | null) {
    this._selectedDate$.next(date);
    this._trainingLogService
      .getTrainingLogByDate(date)
      .pipe(
        catchError(() => {
          this._exerciseService.setExercises([]);
          return EMPTY;
        })
      )
      .subscribe((trainingLog) => {
        if (trainingLog)
          this._exerciseService.setExercises(trainingLog.exercises);
      });
  }

  onAddExercise(exercise: Exercise) {
    console.log('Exercise: ', exercise);
    if (exercise.trainingLogId === '') {
      const trainingLog = {
        id: '00000000-0000-0000-0000-000000000000',
        date: this._selectedDate$.value!,
        exercises: [],
        userId: '',
      };
      this._trainingLogService
        .createTrainingLog(trainingLog)
        .pipe(
          take(1),
          switchMap((trainingLog) => {
            return this._exerciseService.createExercise({
              ...exercise,
              trainingLogId: trainingLog.id,
            });
          }),
          switchMap((exercise) => {
            return this._chartService.fetchTrainingChartData({
              monthSelected: trainingLog.date.getMonth() + 1,
              yearSelected: trainingLog.date.getFullYear(),
              exerciseSelected: exercise.exerciseChoice?.name!,
              isMonthly: true,
            });
          })
        )
        .subscribe();
    } else {
      this._exerciseService
        .createExercise(exercise)
        .pipe(
          take(1),
          switchMap((exercise) => {
            return this._chartService.fetchTrainingChartData({
              monthSelected: this._selectedDate$.value!.getMonth() + 1,
              yearSelected: this._selectedDate$.value!.getFullYear(),
              exerciseSelected: exercise.exerciseChoice?.name!,
              isMonthly: true,
            });
          })
        )
        .subscribe();
    }
  }

  onUpdateExercise(exercise: Exercise) {
    this._exerciseService
      .updateExercise(exercise)
      .pipe(take(1))
      .subscribe(() => {
        this._toasterService.success(`Exercise successfully updated!`);
        this._chartService.fetchTrainingChartData({
          monthSelected: this._selectedDate$.value!.getMonth() + 1,
          yearSelected: this._selectedDate$.value!.getFullYear(),
          exerciseSelected: exercise.exerciseChoice?.name!,
          isMonthly: true,
        });
      });
  }

  onDeleteExercise(id: string): void {
    this._exerciseService
      .deleteExercise(id)
      .pipe(
        switchMap(() => {
          this._toasterService.success('Exercise successfully deleted!');

          if (this._exerciseService.exercises.length === 0) {
            return this._trainingLogService.deleteTrainingLog(
              this._trainingLogService.selectedTrainingLog!
            );
          } else {
            return EMPTY;
          }
        })
      )
      .subscribe(() => {
        // this._chartService.fetchTrainingChartData({
        //   monthSelected: this._selectedDate$.value!.getMonth() + 1,
        //   yearSelected: this._selectedDate$.value!.getFullYear(),
        //   exerciseSelected: exercise.exerciseChoice?.name!,
        //   isMonthly: true,
        // });
      });
  }

  onDeleteTrainingLog(id: string | null): void {
    const dialogRef = this._dialog.open(DeleteConfirmationDialogComponent, {
      data: {
        date: this._selectedDate$.value,
      },
    });

    dialogRef
      .afterClosed()
      .pipe(
        take(1),
        switchMap((confirm: boolean) => {
          if (confirm && id)
            return this._trainingLogService.deleteTrainingLog(id);
          return EMPTY;
        })
      )
      .subscribe(() => {
        this._toasterService.success('Training Log successfully deleted!');
      });
  }

  onChartFiltersChange(trainingChartFilters: TrainingChartFilters) {
    this._chartService
      .fetchTrainingChartData(trainingChartFilters)
      .pipe(take(1))
      .subscribe();
  }
}
