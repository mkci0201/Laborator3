import { Component, Inject, } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ToDoTask } from './to-do-task.model';
import { ToDoTasksService } from './to-do-tasks.service';


@Component({
  selector: 'app-to-do-tasks',
  templateUrl: './to-do-tasks.component.html',
  styleUrls: ['./to-do-tasks.component.css']
})
export class ToDoTasksComponent {


}
