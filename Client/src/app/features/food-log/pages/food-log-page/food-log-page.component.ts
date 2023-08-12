import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { BehaviorSubject, EMPTY, Observable } from 'rxjs';
import { take, switchMap } from 'rxjs/operators';
import { ChartService } from 'src/app/features/training-log/services/chart.service';
import { DeleteConfirmationDialogComponent } from 'src/app/shared/components/delete-confirmation-dialog/delete-confirmation-dialog.component';
import { FoodItem } from '../../models/FoodItem';
import { FoodLog } from '../../models/FoodLog';
import { FoodItemService } from '../../services/food-item.service';
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

  public chartData!: number[];

  constructor(
    private _foodLogService: FoodLogService,
    private _foodItemService: FoodItemService,
    private _chartService: ChartService,
    private _toasterService: ToastrService,
    private _dialog: MatDialog
  ) {}

  ngOnInit(): void {
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
      this.chartData = [];

      return;
    }
    this._foodLogService
      .getFoodLog(selectDayObject!.foodLogId)
      .pipe(take(1))
      .subscribe((res) => {
        this.foodItems = res.foodItems;
        this.chartData = this._chartService.calculateFoodChartData(
          this.foodItems
        );
      });
  }

  onAddFoodItem(foodItem: FoodItem): void {
    if (foodItem.foodLogId === '') {
      const foodLog = {
        id: '00000000-0000-0000-0000-000000000000',
        date: this.selectedDate!,
        foodItems: [],
        userId: '',
      };
      this._foodLogService
        .createFoodLog(foodLog)
        .pipe(
          switchMap((foodLog): any => {
            foodItem.foodLogId = foodLog.id;
            const updatedLogs = [...this.foodLogsSubject$.value, foodLog];
            this.foodLogsSubject$.next(updatedLogs);
            return this._foodItemService.createFoodItem(foodItem).pipe(take(1));
          })
        )
        .subscribe((result: any): any => {
          this.foodItems = [...this.foodItems, result];
          this.chartData = this._chartService.calculateFoodChartData(
            this.foodItems
          );
        });
    } else {
      this._foodItemService
        .createFoodItem(foodItem)
        .pipe(take(1))
        .subscribe((result) => {
          this.foodItems = [...this.foodItems, result];
          this._toasterService.success('Meal successfully added!');
          this.chartData = this._chartService.calculateFoodChartData(
            this.foodItems
          );
        });
    }
  }

  onUpdateFoodItem(foodItem: FoodItem): void {
    this._foodItemService
      .updateFoodItem(foodItem)
      .pipe(take(1))
      .subscribe((result) => {
        const updatedItems = this.foodItems.map((el) =>
          el.id === foodItem.id ? foodItem : el
        );
        this.foodItems = updatedItems;
        this._toasterService.success('Meal successfully updated!');
        this.chartData = this._chartService.calculateFoodChartData(
          this.foodItems
        );
      });
  }

  onDeleteFoodItem(id: string): void {
    this._foodItemService
      .deleteFoodItem(id)
      .pipe(take(1))
      .subscribe(() => {
        this.foodItems = this.foodItems.filter(
          (foodItem) => foodItem.id !== id
        );
        this._toasterService.success('Meal successfully deleted!');
        this.chartData = this._chartService.calculateFoodChartData(
          this.foodItems
        );
      });
  }

  onDeleteFoodLog(id: string): void {
    const dialogRef = this._dialog.open(DeleteConfirmationDialogComponent, {
      data: {
        date: this.selectedDate,
      },
    });

    dialogRef
      .afterClosed()
      .pipe(
        switchMap((confirm: boolean) => {
          if (confirm) return this._foodLogService.deleteFoodLog(id);
          return EMPTY;
        }),
        take(1)
      )
      .subscribe(() => {
        const updatedFoodLogs = this.foodLogsSubject$.value.filter(
          (log: FoodLog) => log.id !== id
        );
        this.foodLogsSubject$.next(updatedFoodLogs);
        this.foodItems = [];
        this._toasterService.success('Food Log successfully deleted!');
        this.chartData = this._chartService.calculateFoodChartData(
          this.foodItems
        );
      });
  }
}
