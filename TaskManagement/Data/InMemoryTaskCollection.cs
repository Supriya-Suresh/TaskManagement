using TaskManagement.Models;

namespace TaskManagement.Data
{
    public class InMemoryTaskCollection
    {
        private static List<Tasks> tasks = new List<Tasks>
        {
            new Tasks { Id=Guid.NewGuid(),Name = "Task 1", Priority = 2, Status = "Not Started" },
            new Tasks { Id=Guid.NewGuid(),Name = "Task 2", Priority = 1, Status = "In Progress" },
            new Tasks { Id=Guid.NewGuid(), Name = "Task 3", Priority = 3, Status = "Completed" }
        };

        public static List<Tasks> GetTasks()
        {
            return tasks;
        }

        public static Tasks GetTaskById(Guid id)
        {
            return tasks.FirstOrDefault(t => t.Id == id);
        }
    }
}
