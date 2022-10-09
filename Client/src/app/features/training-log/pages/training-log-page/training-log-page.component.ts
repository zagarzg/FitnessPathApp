import { Component, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { faHandshakeSimple } from '@fortawesome/free-solid-svg-icons';
import { element } from 'protractor';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Exercise } from '../../models/Exercise';
import { TrainingLog } from '../../models/TrainingLog';
import { ExerciseService } from '../../services/exercise.service';
import { TrainingLogService } from '../../services/training-log.service';

@Component({
  selector: 'app-training-log-page',
  templateUrl: './training-log-page.component.html',
  styleUrls: ['./training-log-page.component.scss']
})
export class TrainingLogPageComponent implements OnInit {

  public trainingLogs$: Observable<TrainingLog[]> = this._trainingLogService.getMonthlyTrainingLogs(new Date(Date.now()).getMonth() + 1);

  public exercises!: Exercise[];

  constructor(
    private _trainingLogService: TrainingLogService,
    private _exerciseService: ExerciseService
  ) { }

  ngOnInit(): void {
  }

  fetchExercises(id: string | null): void {
    if(!id) {
      this.exercises = [];
      return;
    } 
    this._trainingLogService.getTrainingLog(id).pipe(
      take(1))
      .subscribe( res => this.exercises = res.exercises);
  }

  onAdd(exercise: Exercise) {
    this._exerciseService.createExercise(exercise).pipe(
      take(1))
      .subscribe((result) => {
        this.exercises = [...this.exercises, result];
      });
  }

  onUpdate(exercise: Exercise) {
    this._exerciseService.updateExercise(exercise).pipe(
      take(1))
      .subscribe((result) => {
        const updatedItems = this.exercises.map(el => el.id === exercise.id ? exercise : el);
        this.exercises = updatedItems;
      });
  }

  onDelete(id: string): void {
    this._exerciseService.deleteExercise(id).pipe(
      take(1))
      .subscribe(() => {
        this.exercises = this.exercises.filter(exercise => exercise.id !== id)});
  }

}
