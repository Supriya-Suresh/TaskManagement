using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public IEnumerable<Tasks> Tasks { get; private set; }

        public void OnGet()
        {
            var CompletedTasks = InMemoryTaskCollection.GetTasks().Where(t => t.Status == "Completed").ToList();
            if (CompletedTasks.Any())
            {
                InMemoryTaskCollection.GetTasks().RemoveAll(t => t.Status == "Completed");
            }
            Tasks = InMemoryTaskCollection.GetTasks();
        }

        public IActionResult Create()
        {
            return RedirectToPage("/CreateTask");
        }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

    }
}