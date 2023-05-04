using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Data;
using TaskManagement.Models;


namespace TaskManagement.Pages
{
    public class CreateTaskModel : PageModel
    {

        [BindProperty]
        public Tasks Task { get; set; }
        public void OnGet()
        {
            Task = new Tasks();
        }

        
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Validation : Check if the task name already exists
            var existingTask = InMemoryTaskCollection.GetTasks()
                .Find(t => t.Name.Equals(Task.Name, StringComparison.OrdinalIgnoreCase));
            if (existingTask != null)
            {
                ModelState.AddModelError("Task.Name", "Task name already exists.");
                return Page();
            }

            //If the status is Completed then no need to add to the list
            if (Task.Status!="Completed")
            {
                var newTask = Task;
                newTask.Id = Guid.NewGuid();
                InMemoryTaskCollection.GetTasks().Add(newTask);
            }

            return RedirectToPage("./Index");
        }
}
}
