import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TrainingLog } from '../models/TrainingLog';
import { environment } from 'src/environments/environment';

@Injectable()
export class TrainingLogService {
  constructor(private _http: HttpClient) {}

  public getTrainingLog(id: string): Observable<TrainingLog> {
    return this._http.get<TrainingLog>(
      `${environment.URL}/TrainingLog/Get/${id}`
    );
  }

  public createTrainingLog(formData: TrainingLog): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    formData.userId = environment.USER_ID;
    formData.date.setHours(6);

    return this._http.post<any>(
      `${environment.URL}/TrainingLog/Create`,
      formData,
      httpOptions
    );
  }

  public deleteTrainingLog(id: string): Observable<any> {
    return this._http.delete<any>(
      `${environment.URL}/TrainingLog/Delete/${id}`
    );
  }

  public getMonthlyTrainingLogs(month: number): Observable<TrainingLog[]> {
    return this._http.get<TrainingLog[]>(
      `${environment.URL}/TrainingLog/GetMonthyTrainingLogs?month=${month}`
    );
  }

  public getAllTrainingLogs(): Observable<TrainingLog[]> {
    return this._http.get<TrainingLog[]>(
      `${environment.URL}/TrainingLog/GetAll`
    );
  }
}
