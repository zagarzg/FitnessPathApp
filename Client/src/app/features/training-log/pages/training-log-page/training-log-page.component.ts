import { Component, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { faHandshakeSimple } from '@fortawesome/free-solid-svg-icons';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Exercise } from '../../models/Exercise';
import { TrainingLog } from '../../models/TrainingLog';
import { TrainingLogService } from '../../services/training-log.service';

@Component({
  selector: 'app-training-log-page',
  templateUrl: './training-log-page.component.html',
  styleUrls: ['./training-log-page.component.scss']
})
export class TrainingLogPageComponent implements OnInit {

  public trainingLogs$: Observable<TrainingLog[]> = this._trainingLogService.getMonthlyTrainingLogs(new Date(Date.now()).getMonth() + 1);

  public exercises!: Exercise[];

  constructor(private _trainingLogService: TrainingLogService) { }

  ngOnInit(): void {
  }

  fetchExercises(id: string): void {
    this._trainingLogService.getTrainingLog(id).pipe(
      take(1)
      ).subscribe( res => this.exercises = res.exercises);
  }

}
