import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, EMPTY, Observable, of } from 'rxjs';
import { TrainingLog } from '../models/TrainingLog';
import { environment } from 'src/environments/environment';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable()
export class TrainingLogService {
  private readonly _trainingLogs$ = new BehaviorSubject<TrainingLog[]>([]);
  readonly trainingLogs$ = this._trainingLogs$.asObservable();

  private readonly _selectedTrainingLogId$ = new BehaviorSubject<string | null>(
    null
  );

  readonly selectedTrainingLogId$ = this._selectedTrainingLogId$.asObservable();

  constructor(private _http: HttpClient) {}

  get selectedTrainingLog() {
    return this._selectedTrainingLogId$.getValue();
  }

  public getTrainingLog(id: string): Observable<TrainingLog> {
    return this._http.get<TrainingLog>(
      `${environment.URL}/TrainingLog/Get/${id}`
    );
  }

  getTrainingLogExercises(trainingLogId: string) {
    return this._http.get<TrainingLog>(
      `${environment.URL}/TrainingLog/Get/${trainingLogId}`
    );
  }

  getTrainingLogByDate(date: Date | null) {
    return this._http
      .get<TrainingLog>(
        `${
          environment.URL
        }/TrainingLog/GetByDate?date=${date?.toLocaleDateString()}`
      )
      .pipe(
        catchError(() => {
          this._selectedTrainingLogId$.next(null);
          throw EMPTY;
        }),
        tap((trainingLog) => {
          console.log(trainingLog);
          if (trainingLog) this._selectedTrainingLogId$.next(trainingLog.id);
          else this._selectedTrainingLogId$.next(null);
        })
      );
  }

  public createTrainingLog(formData: TrainingLog): Observable<any> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    formData.userId = environment.USER_ID;
    formData.date.setHours(6);

    return this._http
      .post<any>(`${environment.URL}/TrainingLog/Create`, formData, httpOptions)
      .pipe(
        tap((trainingLog) => {
          this._trainingLogs$.next([...this._trainingLogs$.value, trainingLog]);
          this._selectedTrainingLogId$.next(trainingLog.id);
        })
      );
  }

  public deleteTrainingLog(id: string): Observable<any> {
    return this._http
      .delete<any>(`${environment.URL}/TrainingLog/Delete/${id}`)
      .pipe(
        tap((trainingLogId) => {
          const updatedTrainingLogs = this._trainingLogs$.value.filter(
            (log: TrainingLog) => log.id !== trainingLogId
          );
          this._trainingLogs$.next(updatedTrainingLogs);
          this._selectedTrainingLogId$.next(null);
        })
      );
  }

  public getMonthlyTrainingLogs(month: number): Observable<TrainingLog[]> {
    return this._http
      .get<TrainingLog[]>(
        `${environment.URL}/TrainingLog/GetMonthyTrainingLogs?month=${month}`
      )
      .pipe(
        tap((trainingLogs) => {
          this._trainingLogs$.next(trainingLogs);
        })
      );
  }

  public getAllTrainingLogDates(): Observable<TrainingLog[]> {
    return this._http
      .get<TrainingLog[]>(`${environment.URL}/TrainingLog/GetAll`)
      .pipe(
        tap((trainingLogs) => {
          this._trainingLogs$.next(trainingLogs);
        })
      );
  }
}
