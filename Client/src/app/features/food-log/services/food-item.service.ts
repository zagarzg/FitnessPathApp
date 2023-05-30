import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { FoodItem } from '../models/FoodItem';

@Injectable({
  providedIn: 'root',
})
export class FoodItemService {
  constructor(private _http: HttpClient) {}

  public createFoodItem(formData: FoodItem): Observable<FoodItem> {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };

    return this._http.post<FoodItem>(
      `${environment.URL}/FoodItem/Create`,
      formData,
      httpOptions
    );
  }

  public updateFoodItem(formData: FoodItem): Observable<FoodItem> {
    return this._http.put<FoodItem>(
      `${environment.URL}/FoodItem/Update`,
      formData
    );
  }

  public deleteFoodItem(id: string): Observable<any> {
    return this._http.delete(`${environment.URL}/FoodItem/Delete/${id}`);
  }
}
