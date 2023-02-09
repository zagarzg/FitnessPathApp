import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class ChartService {
  constructor(private _http: HttpClient) {}

  public getMonthlyChartDataByExerciseName(
    exerciseName: string,
    month: number,
    yearSelected: number
  ): Observable<{ y: number; x: number }[]> {
    return this._http.get<{ y: number; x: number }[]>(
      `${environment.URL}/Chart/GetMonthlyChartDataByExerciseName?monthSelected=${month}&exerciseName=${exerciseName}&yearSelected=${yearSelected}`
    );
  }

  public getYearlyChartDataByExerciseName(
    exerciseName: string,
    yearSelected: number
  ): Observable<{ y: number; x: number }[]> {
    return this._http.get<{ y: number; x: number }[]>(
      `${environment.URL}/Chart/GetYearlyChartDataByExerciseName?exerciseName=${exerciseName}&yearSelected=${yearSelected}`
    );
  }
}
