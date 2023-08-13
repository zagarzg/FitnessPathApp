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
import { WeightLog } from '../../models/WeightLog';
import { WeightLogFormComponent } from '../weight-log-form/weight-log-form.component';

@Component({
  selector: 'app-weight-log-list',
  templateUrl: './weight-log-list.component.html',
  styleUrls: ['./weight-log-list.component.scss'],
})
export class WeightLogListComponent implements OnInit, OnChanges {
  @Input() selectedDate!: Date | null;
  @Input() selectedWeightLog: WeightLog | undefined;
  @Input() weightLogs!: WeightLog[];

  @Output() selectedDayChangeEvent = new EventEmitter<WeightLog | null>();
  @Output() deleteWeightLogEvent = new EventEmitter<string>();
  @Output() addWeightLogEvent = new EventEmitter<WeightLog>();
  @Output() updateWeightLogEvent = new EventEmitter<WeightLog>();
  @ViewChild(MatCalendar) calendar!: MatCalendar<Date>;

  constructor(public dialog: MatDialog) {}

  ngOnChanges(changes: SimpleChanges) {
    let logsChange = changes['weightLogs'];
    if (logsChange != undefined && !logsChange.firstChange) {
      this.calendar.updateTodaysDate();
    }
  }

  ngOnInit(): void {}

  selectDay() {
    this.selectedWeightLog = this.weightLogs.find(
      (log) =>
        new Date(log.date).getDate() === this.selectedDate?.getDate() &&
        new Date(log.date).getMonth() === this.selectedDate?.getMonth() &&
        new Date(log.date).getFullYear() === this.selectedDate.getFullYear()
    );

    if (this.selectedWeightLog) {
      this.selectedDayChangeEvent.emit(this.selectedWeightLog);
    } else {
      this.selectedDayChangeEvent.emit(null);
    }
  }

  addWeightLog() {
    const dialogRef = this.dialog.open(WeightLogFormComponent, {
      width: '400px',
      data: {
        date: this.selectedDate,
        weightLogId: this.selectedWeightLog?.id,
      },
    });

    dialogRef.afterClosed().subscribe((formData: WeightLog) => {
      this.addWeightLogEvent.emit(formData);
    });
  }

  updateWeightLog() {
    const dialogRef = this.dialog.open(WeightLogFormComponent, {
      width: '400px',
      data: {
        date: this.selectedDate,
        weightLogId: this.selectedWeightLog?.id,
        value: this.selectedWeightLog?.value,
      },
    });

    dialogRef.afterClosed().subscribe((formData: WeightLog) => {
      this.updateWeightLogEvent.emit(formData);
    });
  }

  deleteWeightLog(id: string) {
    this.deleteWeightLogEvent.emit(id);
  }

  dateClass() {
    return (date: Date): MatCalendarCellCssClasses => {
      if (this.weightLogs == undefined) return '';

      let weightLogsByMonth = this.weightLogs.filter(
        (log: WeightLog) =>
          new Date(log.date).getFullYear() == date.getFullYear() &&
          new Date(log.date).getMonth() == date.getMonth()
      );

      let mappedLogs = weightLogsByMonth.map((log: WeightLog) => {
        return new Date(log.date).getDate();
      });

      const dayNumber: number = date.getDate();

      if (mappedLogs.includes(dayNumber)) {
        return 'logged-day rounded-full';
      } else {
        return '';
      }
    };
  }
}
