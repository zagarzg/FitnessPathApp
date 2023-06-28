import { Component, OnInit, ViewChild } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { take, switchMap } from 'rxjs/operators';
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

  @ViewChild(TrainingChartComponent) chartComponent!: TrainingChartComponent;

  constructor(
    private _trainingLogService: TrainingLogService,
    private _exerciseService: ExerciseService,
    private _exerciseChoiceService: ExerciseChoiceService
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
      .subscribe((res) => (this.exercises = res.exercises));
  }

  onAdd(exercise: Exercise) {
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
            return this._exerciseService.createExercise(exercise).pipe(take(1));
          })
        )
        .subscribe((result: any): any => {
          this.exercises = [...this.exercises, result];
          this.chartComponent.monthChange(2);
        });
    } else {
      this._exerciseService
        .createExercise(exercise)
        .pipe(take(1))
        .subscribe((result) => {
          this.exercises = [...this.exercises, result];
        });
    }
  }

  onUpdate(exercise: Exercise) {
    this._exerciseService
      .updateExercise(exercise)
      .pipe(take(1))
      .subscribe((result) => {
        const updatedItems = this.exercises.map((el) =>
          el.id === exercise.id ? exercise : el
        );
        this.exercises = updatedItems;
      });
  }

  onDelete(id: string): void {
    this._exerciseService
      .deleteExercise(id)
      .pipe(take(1))
      .subscribe(() => {
        this.exercises = this.exercises.filter(
          (exercise) => exercise.id !== id
        );
      });
  }
}
