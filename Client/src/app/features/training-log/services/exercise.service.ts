import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Exercise } from '../models/Exercise';

@Injectable()

export class ExerciseService {

  constructor(private _http: HttpClient) { }

  public createExercise(formData: Exercise): Observable<Exercise> {
    return this._http.post<Exercise>(`${environment.URL}/Exercise/Create`, formData);
  }

  public updateExercise(formData: Exercise): Observable<Exercise> {
    return this._http.put<Exercise>(`${environment.URL}/Exercise/Update`, formData);
  }

  public deleteExercise(id: string): Observable<any> {
    return this._http.delete(`${environment.URL}/Exercise/Delete/${id}`);
  }
}
