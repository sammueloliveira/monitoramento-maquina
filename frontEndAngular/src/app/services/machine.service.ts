import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, EMPTY } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Machine } from '../interfaces/machine.module';

@Injectable({
  providedIn: 'root',
})

export class MachineService {
  baseUrl = 'http://localhost:5056/api/machine';
 

  constructor(private snackBar: MatSnackBar, private http: HttpClient) {}

  showMessage(msg: string): void {
    this.snackBar.open(msg, 'x', {
      duration: 3000,
      horizontalPosition: 'right',
      verticalPosition: 'top',
    });
  }

  create(machine: Machine): Observable<Machine> {
    return this.http.post<Machine>(this.baseUrl, machine).pipe(
      map((obj) => obj),
      catchError((e) => this.errorHandler(e))
    );
  }
  

  read(): Observable<Machine[]> {
    return this.http.get<Machine[]>(this.baseUrl).pipe(
      map((obj) => obj),
      catchError((e) => this.errorHandler(e))
    );
  }

  readById(id: string): Observable<Machine> {
    const url = `${this.baseUrl}/${id}`;
    return this.http
      .get<Machine>(url)
      .pipe(catchError((e) => this.errorHandler(e)));
  }

  update(machine: Machine): Observable<Machine> {
    const url = `${this.baseUrl}/${machine.id}`;
    return this.http
      .put<Machine>(url, machine)
      .pipe(catchError((e) => this.errorHandler(e)));
  }

  delete(id: string): Observable<void> {
    const url = `${this.baseUrl}/${id}`;
    return this.http
      .delete<void>(url)
      .pipe(catchError((e) => this.errorHandler(e)));
  }

  errorHandler(e: any): Observable<any> {
    this.showMessage('Ocorreu um erro!');
    return EMPTY;
  }

 
}