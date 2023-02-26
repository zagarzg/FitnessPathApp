import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import {
  MatCalendar,
  MatCalendarCellCssClasses,
} from '@angular/material/datepicker';
import { MatDialog } from '@angular/material/dialog';
import { Exercise } from '../../models/Exercise';
import { TrainingLog } from '../../models/TrainingLog';
import { ExerciseFormComponent } from '../exercise-form/exercise-form.component';

@Component({
  selector: 'app-recent-logs-list',
  templateUrl: './recent-logs-list.component.html',
  styleUrls: ['./recent-logs-list.component.scss'],
})
export class RecentLogsListComponent implements OnChanges {
  selectedTrainingLog: TrainingLog | undefined;
  selectedDate!: Date | null;

  @Input() trainingLogs!: TrainingLog[];
  @Input() exercises!: Exercise[];
  @Output() selectedDayChangeEvent = new EventEmitter<{
    trainingLogId: string | null;
    date: Date | null;
  }>();
  @Output() deleteTrainingLogEvent = new EventEmitter<string>();
  @Output() addExerciseEvent = new EventEmitter<Exercise>();
  @Output() updateExerciseEvent = new EventEmitter<Exercise>();
  @Output() deleteExerciseEvent = new EventEmitter<string>();
  @ViewChild(MatCalendar) calendar!: MatCalendar<Date>;

  displayedColumns = ['name', 'reps', 'sets', 'weight', 'actions'];

  constructor(public dialog: MatDialog) {}

  ngOnChanges(changes: SimpleChanges) {
    let logsChange = changes['trainingLogs'];
    if (logsChange != undefined && !logsChange.firstChange) {
      this.calendar.updateTodaysDate();
    }
  }

  selectDay() {
    this.selectedTrainingLog = this.trainingLogs.find(
      (log) =>
        new Date(log.date).getDate() === this.selectedDate?.getDate() &&
        new Date(log.date).getMonth() === this.selectedDate?.getMonth() &&
        new Date(log.date).getFullYear() === this.selectedDate.getFullYear()
    );

    if (this.selectedTrainingLog) {
      this.selectedDayChangeEvent.emit({
        trainingLogId: this.selectedTrainingLog.id,
        date: this.selectedDate,
      });
    } else {
      this.selectedDayChangeEvent.emit({
        trainingLogId: '',
        date: this.selectedDate,
      });
    }
  }

  addExercise() {
    const dialogRef = this.dialog.open(ExerciseFormComponent, {
      width: '400px',
      data: {
        trainingLogId: this.selectedTrainingLog?.id,
      },
    });

    dialogRef.afterClosed().subscribe((formData: Exercise) => {
      this.addExerciseEvent.emit(formData);
    });
  }

  updateExercise(exercise: Exercise) {
    const dialogRef = this.dialog.open(ExerciseFormComponent, {
      width: '400px',
      data: {
        exercise,
      },
    });

    dialogRef.afterClosed().subscribe((formData: Exercise) => {
      console.log(`Form data before: ${formData.id}`);
      formData.id = exercise.id;
      console.log(`Form data after: ${formData.id}`);
      this.updateExerciseEvent.emit(formData);
    });
  }

  deleteExercise(id: string) {
    this.deleteExerciseEvent.emit(id);
  }

  dateClass() {
    return (date: Date): MatCalendarCellCssClasses => {
      if (this.trainingLogs == undefined) return '';

      let trainingLogsByMonth = this.trainingLogs.filter(
        (log: TrainingLog) =>
          new Date(log.date).getFullYear() == date.getFullYear() &&
          new Date(log.date).getMonth() == date.getMonth()
      );

      let mappedLogs = trainingLogsByMonth.map((log: TrainingLog) => {
        return new Date(log.date).getDate();
      });

      const dayNumber: number = date.getDate();

      if (mappedLogs.includes(dayNumber)) {
        return 'bg-green-300 rounded-full';
      } else {
        return '';
      }
    };
  }
}
