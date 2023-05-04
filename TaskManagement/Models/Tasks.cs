using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Models
{
    public class Tasks
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, 10)]
        public int Priority { get; set; }
        [Required]
        public string Status { get; set; }


    }


    //public static class InMemoryTaskCollection
    //{
    //    private static List<Tasks> tasks = new List<Tasks>
    //    {
    //        new Tasks { Id=Guid.NewGuid(),Name = "Task 1", Priority = 2, Status = "Not Started" },
    //        new Tasks { Id=Guid.NewGuid(),Name = "Task 2", Priority = 1, Status = "In Progress" },
    //        new Tasks { Id=Guid.NewGuid(), Name = "Task 3", Priority = 3, Status = "Not Started" }
    //    };

    //    public static List<Tasks> GetTasks()
    //    {
    //        return tasks.FindAll(t => t.Status != "Completed");
    //    }

    //    public static Tasks GetTaskById(Guid id)
    //    {
    //        return tasks.FirstOrDefault(t => t.Id == id);
    //    }
    //}
}

