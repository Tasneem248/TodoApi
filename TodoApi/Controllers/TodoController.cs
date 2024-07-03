using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using System.Collections.Generic;
using System.Linq;


namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private static List<Todo> todos = new List<Todo>();
        private static int nextId = 1;

        // Todo Oluşturma
        [HttpPost]
        public ActionResult<Todo> CreateTodo([FromBody] Todo todo)
        {
            todo.Id = nextId++;
            todos.Add(todo);
            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        // Todo'ları Listele
        [HttpGet]
        public ActionResult<IEnumerable<Todo>> GetTodos()
        {
            return todos;
        }

        // Belirli Bir Todo'yu Getir
        [HttpGet("{id}")]
        public ActionResult<Todo> GetTodoById(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            return todo;
        }

        // Todo Adını Güncelleme
        [HttpPut("{id}/name")]
        public IActionResult UpdateTodoName(int id, [FromBody] string newName)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            todo.Name = newName;
            return NoContent();
        }

        // Todo'yu Tamamlama
        [HttpPut("{id}/complete")]
        public IActionResult CompleteTodo(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            todo.Completed = true;
            return NoContent();
        }

        // Todo'yu Tamamlanmamış Yapma
        [HttpPut("{id}/incomplete")]
        public IActionResult IncompleteTodo(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            todo.Completed = false;
            return NoContent();
        }

        // Todo Silme
        [HttpDelete("{id}")]
        public IActionResult DeleteTodo(int id)
        {
            var todo = todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            todos.Remove(todo);
            return NoContent();
        }
    }
}
