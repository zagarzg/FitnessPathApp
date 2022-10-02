import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { Exercise } from '../../../models/Exercise';
import { TrainingLog } from '../../../models/TrainingLog';

@Component({
  selector: 'app-recent-logs-list',
  templateUrl: './recent-logs-list.component.html',
  styleUrls: ['./recent-logs-list.component.scss']
})
export class RecentLogsListComponent {

  selected!: Date | null;

  @Input() trainingLogs!: TrainingLog[];
  @Input() exercises!: Exercise[];
  @Output() selectedDayChangeEvent = new EventEmitter<string | null>();
  @Output() deleteTrainingLogEvent = new EventEmitter<string>();
  @Output() deleteExerciseEvent = new EventEmitter<string>();
  @Output() addExerciseEvent = new EventEmitter<Exercise>();

  displayedColumns = ['name', 'reps', 'sets', 'weight','actions'];

  constructor() { }

  selectDay() {
    const selectedTrainingLog = this.trainingLogs.find( log => 
        new Date(log.date).getDate() === this.selected?.getDate() &&
        new Date(log.date).getMonth() === this.selected?.getMonth() &&
        new Date(log.date).getFullYear() === this.selected.getFullYear()
    )

    if(selectedTrainingLog) {
      this.selectedDayChangeEvent.emit(selectedTrainingLog.id);
    }
    else {
      this.selectedDayChangeEvent.emit(null);
    } 
  }

  addExercise() {
    const exercise: Exercise = {
      id: 'ac06878e-2ccf-4aea-a6cf-e6c8314b650c',
      name: 'Deadlift',
      sets: 1,
      reps: 5,
      weight: 180,
      trainingLogId: 'cb31d06e-13da-4ba0-a923-5c062399f3a8'
    }

    this.addExerciseEvent.emit(exercise);
  }

  deleteExercise(id: string) {
    this.deleteExerciseEvent.emit(id);
  }

  dateClass() {
    return (date: Date): MatCalendarCellCssClasses => {
      
      if(this.trainingLogs == undefined) return '';

      const existingTrainingLogs = this.trainingLogs.map((log:TrainingLog) => { 
        return new Date(log.date).getDate()
      });

      const dayNumber: number = date.getDate();

      if (existingTrainingLogs.includes(dayNumber) && date.getMonth() == 9) {
        return 'bg-green-300 rounded';
      } else {
        return '';
      }
    };
  }

}
