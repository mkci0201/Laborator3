import { Component, Inject, } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToDoTask } from '../to-do-task.model';
import { ToDoTasksService } from '../to-do-tasks.service';


@Component({
  selector: 'app-list-to-do-tasks',
  templateUrl: './to-do-tasks-list.component.html',
  styleUrls: ['./to-do-tasks-list.component.css']
})
export class ToDoTasksListComponent {

  public toDoTasks: ToDoTask[];
  public toDoTask: ToDoTask;

  constructor(private toDoTasksService : ToDoTasksService ) { }

  getToDoTasks()
  {
    this.toDoTasksService.getToDoTasks().subscribe(t => this.toDoTasks = t);
  }

  ngOnInit() {
    this.getToDoTasks();
  }

}
