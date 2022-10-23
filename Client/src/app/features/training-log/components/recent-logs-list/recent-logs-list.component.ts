import { Component, EventEmitter, Input, Output, ViewChild } from '@angular/core';
import { MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { MatDialog } from '@angular/material/dialog';
import { MatTable } from '@angular/material/table';
import { Observable } from 'rxjs';
import { Exercise } from '../../models/Exercise';
import { TrainingLog } from '../../models/TrainingLog';
import { ExerciseFormComponent } from '../exercise-form/exercise-form.component';

@Component({
  selector: 'app-recent-logs-list',
  templateUrl: './recent-logs-list.component.html',
  styleUrls: ['./recent-logs-list.component.scss']
})
export class RecentLogsListComponent {

  selected!: Date | null;
  selectedTrainingLog: TrainingLog | undefined;

  @Input() trainingLogs!: TrainingLog[];
  @Input() exercises!: Exercise[];
  @Output() selectedDayChangeEvent = new EventEmitter<string | null>();
  @Output() deleteTrainingLogEvent = new EventEmitter<string>();
  @Output() addExerciseEvent = new EventEmitter<Exercise>();
  @Output() updateExerciseEvent = new EventEmitter<Exercise>();
  @Output() deleteExerciseEvent = new EventEmitter<string>();

  displayedColumns = ['name', 'reps', 'sets', 'weight','actions'];

  // @ViewChild(MatTable, {static: false}) table!: MatTable<Exercise>

  constructor(public dialog: MatDialog) { }

  selectDay() {
    this.selectedTrainingLog = this.trainingLogs.find( log => 
        new Date(log.date).getDate() === this.selected?.getDate() &&
        new Date(log.date).getMonth() === this.selected?.getMonth() &&
        new Date(log.date).getFullYear() === this.selected.getFullYear()
    )

    if(this.selectedTrainingLog) {
      this.selectedDayChangeEvent.emit(this.selectedTrainingLog.id);
    }
    else {
      this.selectedDayChangeEvent.emit(null);
    } 
  }

  addExercise() {
    const dialogRef = this.dialog.open(ExerciseFormComponent, {
      width: '400px',
      data: {
        trainingLogId: this.selectedTrainingLog?.id
      }
    });

    dialogRef.afterClosed().subscribe((formData: Exercise) => {
      this.addExerciseEvent.emit(formData);
    });
  }

  updateExercise(exercise: Exercise) {
    const dialogRef = this.dialog.open(ExerciseFormComponent, {
      width: '400px',
      data: {
        exercise
      }
    });

    dialogRef.afterClosed().subscribe((formData: Exercise) => {
      console.log(`Form data before: ${formData.id}`)
      formData.id = exercise.id;
      console.log(`Form data after: ${formData.id}`)
      this.updateExerciseEvent.emit(formData);
    });
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
