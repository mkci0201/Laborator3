import { TestBed } from '@angular/core/testing';

import { ToDoTasksService } from './to-do-tasks.service';

describe('ToDoTasksService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ToDoTasksService = TestBed.get(ToDoTasksService);
    expect(service).toBeTruthy();
  });
});
