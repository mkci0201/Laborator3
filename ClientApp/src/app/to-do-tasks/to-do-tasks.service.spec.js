"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var to_do_tasks_service_1 = require("./to-do-tasks.service");
describe('ToDoTasksService', function () {
    beforeEach(function () { return testing_1.TestBed.configureTestingModule({}); });
    it('should be created', function () {
        var service = testing_1.TestBed.get(to_do_tasks_service_1.ToDoTasksService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=to-do-tasks.service.spec.js.map