import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ToDoTask } from './to-do-task.model';

@Injectable({
  providedIn: 'root'
})
export class ToDoTasksService
{

  private apiUrl: string;

  constructor(private httpClient: HttpClient, @Inject('API_URL') apiUrl: string)
  {
    this.apiUrl = apiUrl;
  }

  getToDoTasks(): Observable<ToDoTask[]> {
    return this.httpClient.get<ToDoTask[]>(this.apiUrl + 'toDoTasks');
  }

  getToDoTask(Id): Observable<ToDoTask>{

    return this.httpClient.get<ToDoTask>(this.apiUrl + 'toDoTasks/' + Id);

  }
}
