using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return _todoService.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id)
        {
            return _todoService.GetById(id);
        }

        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            var itemId = _todoService.Add(item);
            return CreatedAtRoute("GetTodo", new { id = itemId }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            _todoService.Update(id, item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            _todoService.Delete(id);
            return NoContent();
        }
    }
}