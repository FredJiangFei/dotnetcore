import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Todo } from '../_models/todo';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<Todo[]>(`${environment.baseUrl}/todo`);
  }

  add(name: string) {
    return this.http.post<Todo[]>(`${environment.baseUrl}/todo`, {
      name: name
    });
  }

  update(todo: Todo) {
    return this.http.put<Todo[]>(`${environment.baseUrl}/todo`, todo);
  }

  delete(id: number) {
    return this.http.delete<Todo[]>(`${environment.baseUrl}/todo/${id}`);
  }
}
