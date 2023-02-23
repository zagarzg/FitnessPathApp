import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { WeightLog } from '../models/WeightLog';

@Injectable({
  providedIn: 'root',
})
export class WeightLogService {
  constructor(private _http: HttpClient) {}

  public getWeightLog(id: string): Observable<WeightLog> {
    return this._http.get<WeightLog>(`${environment.URL}/WeightLog/Get/${id}`);
  }

  public createWeightLog(formData: WeightLog): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    formData.userId = environment.USER_ID;
    formData.date.setHours(6);

    return this._http.post<any>(
      `${environment.URL}/WeightLog/Create`,
      formData,
      httpOptions
    );
  }

  public updateWeightLog(formData: WeightLog): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    formData.userId = environment.USER_ID;

    return this._http.put<any>(
      `${environment.URL}/WeightLog/Update`,
      formData,
      httpOptions
    );
  }

  public deleteWeightLog(id: string): Observable<any> {
    return this._http.delete<any>(`${environment.URL}/WeightLog/Delete/${id}`);
  }

  public getAllWeightLogs(): Observable<WeightLog[]> {
    return this._http.get<WeightLog[]>(`${environment.URL}/WeightLog/GetAll`);
  }
}
