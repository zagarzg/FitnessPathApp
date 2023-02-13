import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { WeightLog } from '../../models/WeightLog';
import { WeightLogService } from '../../services/weight-log.service';

@Component({
  selector: 'app-weight-log-page',
  templateUrl: './weight-log-page.component.html',
  styleUrls: ['./weight-log-page.component.scss'],
})
export class WeightLogPageComponent implements OnInit {
  public weightLogs$: Observable<WeightLog[]> =
    this._weightLogService.getAllWeightLogs();

  constructor(private _weightLogService: WeightLogService) {}

  ngOnInit(): void {}

  onAdd(log: WeightLog) {
    console.log(`Adding log: ${{ log }}`);
  }

  onGet(id: string | null) {
    console.log(`Getting log by id ${{ id }}`);
  }

  onDelete(id: string): void {
    console.log('Deleting weight log!!');
    this._weightLogService.deleteWeightLog(id).pipe(take(1)).subscribe();
  }
}
