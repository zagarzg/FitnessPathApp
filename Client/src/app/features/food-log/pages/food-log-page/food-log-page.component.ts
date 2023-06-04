import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { FoodItem } from '../../models/FoodItem';
import { FoodLog } from '../../models/FoodLog';
import { FoodLogService } from '../../services/food-log.service';

@Component({
  selector: 'app-food-log-page',
  templateUrl: './food-log-page.component.html',
  styleUrls: ['./food-log-page.component.scss'],
})
export class FoodLogPageComponent implements OnInit {
  private foodLogsSubject$: BehaviorSubject<FoodLog[]> = new BehaviorSubject<
    FoodLog[]
  >([]);
  public foodLogs$: Observable<FoodLog[]> =
    this.foodLogsSubject$.asObservable();

  public foodItems!: FoodItem[];
  public selectedDate!: Date | null;

  constructor(private _foodLogService: FoodLogService) {}

  ngOnInit(): void {
    console.log('NgOnInit of page');
    this._foodLogService
      .getAllFoodLogs()
      .pipe(take(1))
      .subscribe((logs) => {
        this.foodLogsSubject$.next(logs);
      });
  }

  fetchFoodItems(
    selectDayObject: { foodLogId: string | null; date: Date | null } | null
  ): void {
    this.selectedDate = selectDayObject!.date;

    if (!selectDayObject!.foodLogId) {
      this.foodItems = [];
      return;
    }
    this._foodLogService
      .getFoodLog(selectDayObject!.foodLogId)
      .pipe(take(1))
      .subscribe((res) => (this.foodItems = res.foodItems));
  }
}
