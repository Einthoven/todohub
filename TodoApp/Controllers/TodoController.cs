using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using System.Linq;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoDbContext _context;

        public TodoController(TodoDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string category, Priority? priority)
        {
            var query = _context.TodoItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(x => x.Category == category);

            if (priority.HasValue)
                query = query.Where(x => x.Priority == priority);

            return View(query.ToList());
        }

        [HttpPost]
        public IActionResult Add(string title, string category, Priority priority)
        {
            var item = new TodoItem
            {
                Title = title,
                Category = category,
                Priority = priority,
                IsCompleted = false,
                IsHidden = false
            };

            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Undo(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsCompleted = false;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult ToggleVisibility(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsHidden = !item.IsHidden;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Complete(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsCompleted = true;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var item = _context.TodoItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
