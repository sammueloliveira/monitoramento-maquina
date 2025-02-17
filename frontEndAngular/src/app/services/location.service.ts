import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LocationService {
  private apiUrl = 'https://nominatim.openstreetmap.org/search?format=json&q=';

  constructor(private http: HttpClient) {}

  getCoordinates(location: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}${encodeURIComponent(location)}`);
  }
}
