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
  @Output() selectedDayChangeEvent = new EventEmitter<string>();

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
  }

  dateClass() {
    return (date: Date): MatCalendarCellCssClasses => {
      
      if(this.trainingLogs == undefined) return '';

      const existingTrainingLogs = this.trainingLogs.map((log:TrainingLog) => { 
        return new Date(log.date).getDate()
      });

      const dayNumber: number = date.getDate();

      if (existingTrainingLogs.includes(dayNumber) && date.getMonth() == 8) {
        return 'bg-green-300 rounded';
      } else {
        return '';
      }
    };
  }

}
