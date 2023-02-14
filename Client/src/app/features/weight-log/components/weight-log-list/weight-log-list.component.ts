import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatCalendarCellCssClasses } from '@angular/material/datepicker';
import { MatDialog } from '@angular/material/dialog';
import { WeightLog } from '../../models/WeightLog';
import { WeightLogFormComponent } from '../weight-log-form/weight-log-form.component';

@Component({
  selector: 'app-weight-log-list',
  templateUrl: './weight-log-list.component.html',
  styleUrls: ['./weight-log-list.component.scss'],
})
export class WeightLogListComponent implements OnInit {
  selected!: Date | null;
  selectedWeightLog: WeightLog | undefined;

  @Input() weightLogs!: WeightLog[];
  @Output() selectedDayChangeEvent = new EventEmitter<string | null>();
  @Output() deleteWeightLogEvent = new EventEmitter<string>();
  @Output() addWeightLogEvent = new EventEmitter<WeightLog>();
  @Output() updateWeightLogEvent = new EventEmitter<WeightLog>();

  constructor(public dialog: MatDialog) {}

  ngOnInit(): void {
    console.log(this.weightLogs);
  }

  selectDay() {
    this.selectedWeightLog = this.weightLogs.find(
      (log) =>
        new Date(log.date).getDate() === this.selected?.getDate() &&
        new Date(log.date).getMonth() === this.selected?.getMonth() &&
        new Date(log.date).getFullYear() === this.selected.getFullYear()
    );

    if (this.selectedWeightLog) {
      this.selectedDayChangeEvent.emit(this.selectedWeightLog.id);
    } else {
      this.selectedDayChangeEvent.emit(null);
    }
  }

  addWeightLog() {
    const dialogRef = this.dialog.open(WeightLogFormComponent, {
      width: '400px',
      data: {
        trainingLogId: this.selectedWeightLog?.id,
      },
    });

    dialogRef.afterClosed().subscribe((formData: WeightLog) => {
      this.addWeightLogEvent.emit(formData);
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
        return 'bg-green-300 rounded-full';
      } else {
        return '';
      }
    };
  }
}
