import { HttpClient } from '@angular/common/http';
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

  public deleteWeightLog(id: string): Observable<any> {
    return this._http.delete<any>(`${environment.URL}/WeightLog/Delete/${id}`);
  }

  public getAllWeightLogs(): Observable<WeightLog[]> {
    return this._http.get<WeightLog[]>(`${environment.URL}/WeightLog/GetAll`);
  }
}
