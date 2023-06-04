import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import {
  MatCalendar,
  MatCalendarCellCssClasses,
} from '@angular/material/datepicker';
import { MatDialog } from '@angular/material/dialog';
import { FoodItem } from '../../models/FoodItem';
import { FoodLog } from '../../models/FoodLog';

@Component({
  selector: 'app-food-log-list',
  templateUrl: './food-log-list.component.html',
  styleUrls: ['./food-log-list.component.scss'],
})
export class FoodLogListComponent implements OnChanges {
  selectedFoodLog: FoodLog | undefined;
  selectedDate!: Date | null;

  @Input() foodLogs!: FoodLog[];
  @Input() foodItems!: FoodItem[];
  @Output() selectedDayChangeEvent = new EventEmitter<{
    foodLogId: string | null;
    date: Date | null;
  }>();
  @Output() deleteTrainingLogEvent = new EventEmitter<string>();
  @Output() addFoodItemEvent = new EventEmitter<FoodItem>();
  @Output() updateFoodItemEvent = new EventEmitter<FoodItem>();
  @Output() deleteFoodItemEvent = new EventEmitter<string>();
  @ViewChild(MatCalendar) calendar!: MatCalendar<Date>;

  displayedColumns = ['name', 'carbs', 'protein', 'fat', 'calories', 'actions'];

  constructor(public dialog: MatDialog) {}

  ngOnChanges(changes: SimpleChanges) {
    let logsChange = changes['foodLogs'];
    if (logsChange != undefined && !logsChange.firstChange) {
      this.calendar.updateTodaysDate();
    }
  }

  selectDay() {
    this.selectedFoodLog = this.foodLogs.find(
      (log) =>
        new Date(log.date).getDate() === this.selectedDate?.getDate() &&
        new Date(log.date).getMonth() === this.selectedDate?.getMonth() &&
        new Date(log.date).getFullYear() === this.selectedDate.getFullYear()
    );

    if (this.selectedFoodLog) {
      this.selectedDayChangeEvent.emit({
        foodLogId: this.selectedFoodLog.id,
        date: this.selectedDate,
      });
    } else {
      this.selectedDayChangeEvent.emit({
        foodLogId: '',
        date: this.selectedDate,
      });
    }
  }

  // addFoodLog() {
  //   const dialogRef = this.dialog.open(ExerciseFormComponent, {
  //     width: '400px',
  //     data: {
  //       trainingLogId: this.selectedTrainingLog?.id,
  //     },
  //   });

  //   dialogRef.afterClosed().subscribe((formData: Exercise) => {
  //     this.addExerciseEvent.emit(formData);
  //   });
  // }

  // updateExercise(exercise: Exercise) {
  //   const dialogRef = this.dialog.open(ExerciseFormComponent, {
  //     width: '400px',
  //     data: {
  //       exercise,
  //     },
  //   });

  //   dialogRef.afterClosed().subscribe((formData: Exercise) => {
  //     console.log(`Form data before: ${formData.id}`);
  //     formData.id = exercise.id;
  //     console.log(`Form data after: ${formData.id}`);
  //     this.updateExerciseEvent.emit(formData);
  //   });
  // }

  // deleteExercise(id: string) {
  //   this.deleteExerciseEvent.emit(id);
  // }

  dateClass() {
    return (date: Date): MatCalendarCellCssClasses => {
      if (this.foodLogs == undefined) return '';

      let foodLogsByMonth = this.foodLogs.filter(
        (log: FoodLog) =>
          new Date(log.date).getFullYear() == date.getFullYear() &&
          new Date(log.date).getMonth() == date.getMonth()
      );

      let mappedLogs = foodLogsByMonth.map((log: FoodLog) => {
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
