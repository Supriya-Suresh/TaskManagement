using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Pages
{
    public class EditTaskModel : PageModel
    {
        [BindProperty]
        public Tasks Task { get; set; }

        public IActionResult OnGet(Guid id)
        {
            Task = InMemoryTaskCollection.GetTaskById(id);

            if (Task == null)
            {
                return NotFound();
            }

            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oldTask = InMemoryTaskCollection.GetTaskById(Task.Id);

            if (oldTask == null)
            {
                return NotFound();
            }
            // Validation : Check if the task name already exists
            var existingTask = InMemoryTaskCollection.GetTasks()
                .Any(t => t.Id != oldTask.Id && t.Name.Equals(oldTask.Name, StringComparison.OrdinalIgnoreCase));


            if (existingTask)
            {
                ModelState.AddModelError("Task.Name", "Task name already exists.");
                return Page();
            }

            //If the status is Completed then remove the task from the list
            if (Task.Status == "Completed")
            {
                InMemoryTaskCollection.GetTasks().Remove(oldTask);
                return RedirectToPage("./Index");
            }
            else
            {
                oldTask.Status = Task.Status;
                oldTask.Name = Task.Name;
                oldTask.Priority = Task.Priority;
            }

            return RedirectToPage("./Index");
        }
    }
}
