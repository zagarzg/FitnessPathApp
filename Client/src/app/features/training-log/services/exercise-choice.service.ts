import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ExerciseChoice } from '../models/ExerciseChoice';
import { tap } from 'rxjs/operators';

@Injectable()
export class ExerciseChoiceService {
  private readonly _exerciseChoices$ = new BehaviorSubject<string[]>([]);
  readonly exerciseChoices$ = this._exerciseChoices$.asObservable();

  constructor(private _http: HttpClient) {}

  public getAllExerciseChoices(): Observable<ExerciseChoice[]> {
    return this._http.get<ExerciseChoice[]>(
      `${environment.URL}/ExerciseChoice/GetAll`
    );
  }

  public getAllExerciseNames(): Observable<string[]> {
    return this._http
      .get<string[]>(`${environment.URL}/ExerciseChoice/GetAllNames`)
      .pipe(
        tap((exerciseChoices) => {
          this._exerciseChoices$.next(exerciseChoices);
        })
      );
  }

  public createExerciseChoice(
    formData: ExerciseChoice
  ): Observable<ExerciseChoice> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    return this._http.post<ExerciseChoice>(
      `${environment.URL}/ExerciseChoice/Create`,
      formData,
      httpOptions
    );
  }

  public updateExerciseChoice(
    formData: ExerciseChoice
  ): Observable<ExerciseChoice> {
    return this._http.put<ExerciseChoice>(
      `${environment.URL}/ExerciseChoice/Update`,
      formData
    );
  }

  public deleteExerciseChoice(id: string): Observable<any> {
    return this._http.delete(`${environment.URL}/ExerciseChoice/Delete/${id}`);
  }
}
