import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToDoTask } from './to-do-task.model';


@Component({
  selector: 'app-to-do-tasks',
  templateUrl: './to-do-tasks.component.html',
  styleUrls: ['./to-do-tasks.component.css']
})
export class ToDoTasksComponent implements OnInit {

  public toDoTasks: ToDoTask[];

  constructor(http: HttpClient, @Inject('API_URL') apiUrl: string) {
    http.get<ToDoTask[]>(apiUrl + 'toDoTask').subscribe(result => {
      this.toDoTasks = result;
    }, error => console.error(error));
  }

  ngOnInit() {
  }

}
