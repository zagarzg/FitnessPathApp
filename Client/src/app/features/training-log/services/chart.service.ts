import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChartService {

  constructor(private _http: HttpClient) { }

  public getChartDataByExerciseName(exerciseName: string, month: number): Observable<{y: number, x: number}[]> {
    return this._http.get<{y: number, x: number}[]>(`${environment.URL}/Chart/GetChartDataByExerciseName?monthSelected=${month}&exerciseName=${exerciseName}`);
  }
}
