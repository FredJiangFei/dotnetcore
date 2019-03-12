using System;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

public class TodoService : ITodoService
{
    private readonly TodoContext _context;

    public TodoService(TodoContext context)
    {
        _context = context;
    }

    public long Add(TodoItem item)
    {
        _context.TodoItems.Add(item);
        _context.SaveChanges();
        return item.Id;
    }

    public void Delete(long id)
    {
        var todo = _context.TodoItems.Find(id);
        if (todo == null)
        {
            throw new Exception();
        }

        _context.TodoItems.Remove(todo);
        _context.SaveChanges();
    }

    public List<TodoItem> GetAll()
    {
        return _context.TodoItems.ToList();
    }

    public TodoItem GetById(long id)
    {
        var item = _context.TodoItems.Find(id);
        if (item == null)
        {
            throw new Exception();
        }

        return item;
    }

    public void Update(long id, TodoItem item)
    {
        var todo = _context.TodoItems.Find(id);
        if (todo == null)
        {
            throw new Exception();
        }

        todo.IsComplete = item.IsComplete;
        todo.Name = item.Name;

        _context.TodoItems.Update(todo);
        _context.SaveChanges();
    }
}