import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ExerciseChoice } from '../models/ExerciseChoice';

@Injectable()
export class ExerciseChoiceService {
  constructor(private _http: HttpClient) {}

  public getAllExerciseChoices(): Observable<ExerciseChoice[]> {
    return this._http.get<ExerciseChoice[]>(
      `${environment.URL}/ExerciseChoice/GetAll`
    );
  }

  public getAllExerciseNames(): Observable<string[]> {
    return this._http.get<string[]>(
      `${environment.URL}/ExerciseChoice/GetAllNames`
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
