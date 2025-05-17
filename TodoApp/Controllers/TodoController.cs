using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private static List<TodoItem> Items = new List<TodoItem>();

        public IActionResult Index(string category, Priority? priority)
        {
            var filtered = Items.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
                filtered = filtered.Where(x => x.Category == category);

            if (priority.HasValue)
                filtered = filtered.Where(x => x.Priority == priority);

            return View(filtered.ToList());
        }

        [HttpGet]
        public IActionResult Undo(int id)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsCompleted = false;
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Add(string title, string category, Priority priority)
        {
            Items.Add(new TodoItem { Id = Items.Count + 1, Title = title, Category = category, Priority = priority });
            return RedirectToAction("Index");
        }

        public IActionResult ToggleVisibility(int id)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsHidden = !item.IsHidden;
            }
            return RedirectToAction("Index");
        }
        

        public IActionResult Complete(int id)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            if (item != null) item.IsCompleted = true;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var item = Items.FirstOrDefault(x => x.Id == id);
            if (item != null) Items.Remove(item);
            return RedirectToAction("Index");
        }

        
    }
}
