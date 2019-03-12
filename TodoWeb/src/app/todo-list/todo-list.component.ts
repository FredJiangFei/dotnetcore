import { Component, OnInit } from '@angular/core';
import { TodoService } from '../_services/todo.service';
import { Observable } from 'rxjs';
import { Todo } from '../_models/todo';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.css']
})
export class TodoListComponent implements OnInit {
  todos$: Observable<Todo[]>;

  constructor(private todoService: TodoService) {}

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.todos$ = this.todoService.getAll();
  }

  add(name: string) {
    this.todoService.add(name).subscribe(() => this.getAll());
  }
  delete(item: Todo) {
    this.todoService.delete(item.id).subscribe(() => this.getAll());
  }
}
