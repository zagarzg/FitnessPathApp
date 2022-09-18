import { Component, OnInit } from '@angular/core';
import { MatCalendarCellCssClasses } from '@angular/material/datepicker';

@Component({
  selector: 'app-recent-logs-list',
  templateUrl: './recent-logs-list.component.html',
  styleUrls: ['./recent-logs-list.component.scss']
})
export class RecentLogsListComponent implements OnInit {

  selected!: Date | null;

  trainingDays: number[] = [1,5,7,9,13];

  displayedColumns = ['name', 'reps', 'sets', 'weight','actions'];

  mockLogs = [
    {
      name:'Bench',
      reps: 5,
      sets: 5,
      weight: 100
    },
    {
      name:'Squat',
      reps: 3,
      sets: 8,
      weight: 140
    },
    {
      name:'Deadlift',
      reps: 1,
      sets: 5,
      weight: 180
    },
    {
      name:'Barbell Row',
      reps: 8,
      sets: 3,
      weight: 80
    },
    {
      name:'OHP',
      reps: 8,
      sets: 4,
      weight: 60
    }
  ]

  constructor() { }

  ngOnInit(): void {
  }

  dateClass() {
    return (date: Date): MatCalendarCellCssClasses => {

      const dayNumber: number = date.getDate();

      if (this.trainingDays.includes(dayNumber)) {
        return 'bg-green-300 rounded';
      } else {
        return '';
      }
    };
  }

}
