import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { WeightLog } from '../models/WeightLog';
import { map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class WeightLogService {
  private readonly _weightLogs$ = new BehaviorSubject<WeightLog[]>([]);
  readonly weightLogs$ = this._weightLogs$.asObservable();

  private readonly _selectedWeightLogId$ = new BehaviorSubject<string | null>(
    null
  );
  readonly selectedWeightLogId$ = this._selectedWeightLogId$.asObservable();

  constructor(private _http: HttpClient) {}

  get selectedWeightLog() {
    return this._selectedWeightLogId$.getValue();
  }

  public getWeightLog(id: string): Observable<WeightLog> {
    return this._http.get<WeightLog>(`${environment.URL}/WeightLog/Get/${id}`);
  }

  public createWeightLog(formData: WeightLog): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    formData.userId = environment.USER_ID;
    formData.date.setHours(6);

    return this._http
      .post<any>(`${environment.URL}/WeightLog/Create`, formData, httpOptions)
      .pipe(
        tap((weightLog: WeightLog) => {
          this._weightLogs$.next([...this._weightLogs$.value, weightLog]);
          this._selectedWeightLogId$.next(weightLog.id);
        })
      );
  }

  public updateWeightLog(formData: WeightLog): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    formData.userId = environment.USER_ID;

    return this._http
      .put<any>(`${environment.URL}/WeightLog/Update`, formData, httpOptions)
      .pipe(
        tap((weightLog: WeightLog) => {
          const updatedWeightLogs = this._weightLogs$.value.map((el) => {
            return el.id === weightLog.id ? weightLog : el;
          });
          this._weightLogs$.next(updatedWeightLogs);
        })
      );
  }

  public deleteWeightLog(id: string): Observable<any> {
    return this._http
      .delete<any>(`${environment.URL}/WeightLog/Delete/${id}`)
      .pipe(
        tap(() => {
          const updatedWeightLogs = this._weightLogs$.value.filter(
            (log: WeightLog) => log.id !== id
          );
          this._weightLogs$.next(updatedWeightLogs);
          this._selectedWeightLogId$.next(null);
        })
      );
  }

  public getAllWeightLogs(): Observable<WeightLog[]> {
    return this._http
      .get<WeightLog[]>(`${environment.URL}/WeightLog/GetAll`)
      .pipe(
        tap((weightLogs: WeightLog[]) => {
          this._weightLogs$.next(weightLogs);
        })
      );
  }
}
