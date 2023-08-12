import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, EMPTY, forkJoin, Observable, of } from 'rxjs';
import { take, switchMap } from 'rxjs/operators';
import { DeleteConfirmationDialogComponent } from 'src/app/shared/components/delete-confirmation-dialog/delete-confirmation-dialog.component';
import { TrainingChartComponent } from '../../components/training-chart/training-chart.component';
import { Exercise } from '../../models/Exercise';
import { TrainingLog } from '../../models/TrainingLog';
import { ExerciseChoiceService } from '../../services/exercise-choice.service';
import { ExerciseService } from '../../services/exercise.service';
import { TrainingLogService } from '../../services/training-log.service';

@Component({
  selector: 'app-training-log-page',
  templateUrl: './training-log-page.component.html',
  styleUrls: ['./training-log-page.component.scss'],
})
export class TrainingLogPageComponent implements OnInit {
  private trainingLogsSubject$: BehaviorSubject<TrainingLog[]> =
    new BehaviorSubject<TrainingLog[]>([]);
  public trainingLogs$: Observable<TrainingLog[]> =
    this.trainingLogsSubject$.asObservable();

  public exercises!: Exercise[];
  public exerciseNames!: string[];
  public selectedDate!: Date | null;
  public selectedTrainingLogId!: string | null;

  @ViewChild(TrainingChartComponent) chartComponent!: TrainingChartComponent;

  constructor(
    private _trainingLogService: TrainingLogService,
    private _exerciseService: ExerciseService,
    private _exerciseChoiceService: ExerciseChoiceService,
    private _toasterService: ToastrService,
    private _dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this._exerciseChoiceService
      .getAllExerciseNames()
      .pipe(take(1))
      .subscribe((exerciseNames) => {
        this.exerciseNames = exerciseNames;
      });
    this._trainingLogService
      .getAllTrainingLogs()
      .pipe(take(1))
      .subscribe((logs) => {
        this.trainingLogsSubject$.next(logs);
      });
  }

  fetchExercises(
    selectDayObject: { trainingLogId: string | null; date: Date | null } | null
  ): void {
    this.selectedDate = selectDayObject!.date;

    if (!selectDayObject!.trainingLogId) {
      this.exercises = [];
      return;
    }
    this._trainingLogService
      .getTrainingLog(selectDayObject!.trainingLogId)
      .pipe(take(1))
      .subscribe((res) => {
        this.selectedTrainingLogId = res.id;
        this.exercises = res.exercises;
      });
  }

  onAddExercise(exercise: Exercise) {
    if (exercise.trainingLogId === '') {
      const trainingLog = {
        id: '00000000-0000-0000-0000-000000000000',
        date: this.selectedDate!,
        exercises: [],
        userId: '',
      };
      this._trainingLogService
        .createTrainingLog(trainingLog)
        .pipe(
          switchMap((trainingLog): any => {
            exercise.trainingLogId = trainingLog.id;
            const updatedLogs = [
              ...this.trainingLogsSubject$.value,
              trainingLog,
            ];
            this.trainingLogsSubject$.next(updatedLogs);
            this.selectedTrainingLogId = trainingLog.id;
            return this._exerciseService.createExercise(exercise).pipe(take(1));
          })
        )
        .subscribe((result: any): any => {
          this.exercises = [...this.exercises, result];
          this.chartComponent.monthChange(trainingLog.date.getMonth() + 1);
        });
    } else {
      this._exerciseService
        .createExercise(exercise)
        .pipe(take(1))
        .subscribe((result) => {
          this.exercises = [...this.exercises, result];
          this._toasterService.success('Exercise successfully created!');
          this.chartComponent.monthChange(this.selectedDate!.getMonth() + 1);
        });
    }
  }

  onUpdateExercise(exercise: Exercise) {
    this._exerciseService
      .updateExercise(exercise)
      .pipe(take(1))
      .subscribe((result) => {
        const updatedItems = this.exercises.map((el) =>
          el.id === exercise.id ? exercise : el
        );
        this.exercises = updatedItems;
        this._toasterService.success(`Exercise successfully updated!`);
        this.chartComponent.monthChange(this.selectedDate!.getMonth() + 1);
      });
  }

  onDeleteExercise(id: string): void {
    this._exerciseService
      .deleteExercise(id)
      .pipe(
        switchMap(() => {
          this.exercises = this.exercises.filter(
            (exercise) => exercise.id !== id
          );
          this._toasterService.success('Exercise successfully deleted!');
          this.chartComponent.monthChange(this.selectedDate!.getMonth() + 1);
          if (this.exercises.length === 0) {
            return this._trainingLogService.deleteTrainingLog(
              this.selectedTrainingLogId!
            );
          } else {
            return EMPTY;
          }
        })
      )
      .subscribe(() => {
        const updatedTrainingLogs = this.trainingLogsSubject$.value.filter(
          (log: TrainingLog) => log.id !== this.selectedTrainingLogId
        );
        this.selectedTrainingLogId = '';
        this.trainingLogsSubject$.next(updatedTrainingLogs);
      });
  }

  onDeleteTrainingLog(id: string): void {
    const dialogRef = this._dialog.open(DeleteConfirmationDialogComponent, {
      data: {
        date: this.selectedDate,
      },
    });

    dialogRef
      .afterClosed()
      .pipe(
        switchMap((confirm: boolean) => {
          if (confirm) return this._trainingLogService.deleteTrainingLog(id);
          return EMPTY;
        }),
        take(1)
      )
      .subscribe(() => {
        const updatedTrainingLogs = this.trainingLogsSubject$.value.filter(
          (log: TrainingLog) => log.id !== id
        );
        this.selectedTrainingLogId = '';
        this.trainingLogsSubject$.next(updatedTrainingLogs);
        this.exercises = [];
        this._toasterService.success('Training Log successfully deleted!');
        this.chartComponent.monthChange(this.selectedDate!.getMonth() + 1);
      });
  }
}
