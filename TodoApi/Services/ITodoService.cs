using System.Collections.Generic;
using TodoApi.Models;

public interface ITodoService
{
    List<TodoItem> GetAll();
    TodoItem GetById(long id);
    long Add(TodoItem item);
    void Update(long id, TodoItem item);
    void Delete(long id);
}