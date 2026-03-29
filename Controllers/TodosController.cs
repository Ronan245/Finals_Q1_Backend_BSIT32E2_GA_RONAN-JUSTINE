using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private static List<Todo> _todos = new();

       
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_todos);
        }

       
        [HttpPost]
        public IActionResult Post([FromBody] Todo todo)
        {
            if (string.IsNullOrWhiteSpace(todo.Title))
                return BadRequest("Title is required.");

            var newTodo = new Todo
            {
                Id = Guid.NewGuid(),
                Title = todo.Title.Trim(),
                Completed = todo.Completed
            };

            _todos.Add(newTodo);

            return CreatedAtAction(nameof(Get), new { id = newTodo.Id }, newTodo);
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Todo updated)
        {
            if (string.IsNullOrWhiteSpace(updated.Title))
                return BadRequest("Title is required.");

            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return NotFound();

            todo.Title = updated.Title.Trim();
            todo.Completed = updated.Completed;

            return Ok(todo);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null) return NotFound();

            _todos.Remove(todo);
            return NoContent();
        }
    }
}