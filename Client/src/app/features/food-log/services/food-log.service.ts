import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FoodLog } from '../models/FoodLog';

@Injectable({
  providedIn: 'root',
})
export class FoodLogService {
  constructor(private _http: HttpClient) {}

  public getFoodLog(id: string): Observable<FoodLog> {
    return this._http.get<FoodLog>(`${environment.URL}/FoodLog/Get/${id}`);
  }

  public createFoodLog(formData: FoodLog): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    formData.userId = environment.USER_ID;
    formData.date.setHours(6);

    return this._http.post<any>(
      `${environment.URL}/FoodLog/Create`,
      formData,
      httpOptions
    );
  }

  public deleteFoodLog(id: string): Observable<any> {
    return this._http.delete<any>(`${environment.URL}/FoodLog/Delete/${id}`);
  }

  public getAllFoodLogs(): Observable<FoodLog[]> {
    return this._http.get<FoodLog[]>(`${environment.URL}/FoodLog/GetAll`);
  }
}
