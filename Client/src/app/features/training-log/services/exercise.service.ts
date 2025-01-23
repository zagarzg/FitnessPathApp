import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Exercise } from '../models/Exercise';
import { tap } from 'rxjs/operators';

@Injectable()
export class ExerciseService {
  private readonly _exercises$ = new BehaviorSubject<Exercise[]>([]);
  readonly exercises$ = this._exercises$.asObservable();

  constructor(private _http: HttpClient) {}

  get exercises() {
    return this._exercises$.getValue();
  }

  public createExercise(formData: Exercise): Observable<Exercise> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    return this._http
      .post<Exercise>(
        `${environment.URL}/Exercise/Create`,
        formData,
        httpOptions
      )
      .pipe(
        tap((exercise) => {
          this._exercises$.next([...this._exercises$.value, exercise]);
        })
      );
  }

  public updateExercise(formData: Exercise): Observable<Exercise> {
    return this._http
      .put<Exercise>(`${environment.URL}/Exercise/Update`, formData)
      .pipe(
        tap((exercise) => {
          const updatedExercises = this._exercises$.value.map((el) =>
            el.id === exercise.id ? exercise : el
          );
          this._exercises$.next(updatedExercises);
        })
      );
  }

  public deleteExercise(id: string): Observable<any> {
    return this._http.delete(`${environment.URL}/Exercise/Delete/${id}`).pipe(
      tap((exerciseId) => {
        const updatedExercises = this._exercises$.value.filter(
          (exercise) => exercise.id !== exerciseId
        );
        this._exercises$.next(updatedExercises);
      })
    );
  }

  setExercises(exercises: Exercise[]) {
    this._exercises$.next(exercises);
  }
}
