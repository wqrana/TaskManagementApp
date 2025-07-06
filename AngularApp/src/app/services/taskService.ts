import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../models/task';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private readonly apiEndpoint:string = `${environment.apiBaseUrl}/Tasks`;;
  constructor(private http: HttpClient) { 
       
  }

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiEndpoint);
  }
  getTask(id:number): Observable<Task> {
    return this.http.get<Task>(`${this.apiEndpoint}/${id}`);
  }
  addTask(task: Task): Observable<Task> {
    return this.http.post<Task>(this.apiEndpoint, task);
  }

  updateTask(task: Task): Observable<Task> {
    return this.http.put<Task>(`${this.apiEndpoint}/${task.id}`, task);
  }

  deleteTask(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiEndpoint}/${id}`);
  }

}