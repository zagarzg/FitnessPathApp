import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { FoodItem } from '../../food-log/models/FoodItem';

export interface TrainingChartFilters {
  monthSelected?: number;
  yearSelected: number;
  exerciseSelected: string;
  isMonthly: boolean;
}

export interface WeightChartFilters {
  monthSelected?: number;
  yearSelected: number;
  isMonthly: boolean;
}

@Injectable({
  providedIn: 'root',
})
export class ChartService {
  private readonly _traningChartData$ = new BehaviorSubject<
    { y: number; x: number }[]
  >([]);
  readonly trainingChartData$ = this._traningChartData$.asObservable();

  private readonly _weightChartData$ = new BehaviorSubject<
    { y: number; x: number }[]
  >([]);
  readonly weightChartData$ = this._weightChartData$.asObservable();

  readonly todaysDate = new Date();
  readonly defaultWeightChartFilters: WeightChartFilters = {
    monthSelected: this.todaysDate.getMonth() + 1,
    yearSelected: this.todaysDate.getFullYear(),
    isMonthly: true,
  };

  constructor(private _http: HttpClient) {}

  public getMonthlyWeightChangeData(
    monthSelected: number,
    yearSelected: number
  ): Observable<{ y: number; x: number }[]> {
    return this._http.get<{ y: number; x: number }[]>(
      `${environment.URL}/Chart/GetMonthlyWeightChangeData?monthSelected=${monthSelected}&yearSelected=${yearSelected}`
    );
  }

  public getYearlyWeightChangeData(
    yearSelected: number
  ): Observable<{ y: number; x: number }[]> {
    return this._http.get<{ y: number; x: number }[]>(
      `${environment.URL}/Chart/GetYearlyWeightChangeData?yearSelected=${yearSelected}`
    );
  }

  public calculateFoodChartData(foodItems: FoodItem[]) {
    let carbs = 0;
    let protein = 0;
    let fat = 0;
    let calories = 0;

    foodItems.forEach((item) => {
      carbs += item.carbs;
      protein += item.protein;
      fat += item.fat;
      calories += item.calories;
    });

    return [carbs, protein, fat, calories];
  }

  public fetchTrainingChartData(trainingChartFilters: TrainingChartFilters) {
    const params = new HttpParams({
      fromObject: {
        monthSelected: trainingChartFilters.monthSelected!,
        exerciseSelected: trainingChartFilters.exerciseSelected,
        yearSelected: trainingChartFilters.yearSelected,
        isMonthly: trainingChartFilters.isMonthly,
      },
    });

    return this._http
      .get<{ y: number; x: number }[]>(
        `${environment.URL}/Chart/GetTrainingChartData`,
        { params }
      )
      .pipe(
        tap((chartData) => {
          this._traningChartData$.next(chartData);
        })
      );
  }

  public fetchWeightChartData(
    weightChartFilters: WeightChartFilters = this.defaultWeightChartFilters
  ) {
    const params = new HttpParams({
      fromObject: {
        monthSelected: weightChartFilters.monthSelected!,
        yearSelected: weightChartFilters.yearSelected,
        isMonthly: weightChartFilters.isMonthly,
      },
    });

    return this._http
      .get<{ y: number; x: number }[]>(
        `${environment.URL}/Chart/GetWeightChartData`,
        { params }
      )
      .pipe(
        tap((chartData) => {
          this._weightChartData$.next(chartData);
        })
      );
  }
}
