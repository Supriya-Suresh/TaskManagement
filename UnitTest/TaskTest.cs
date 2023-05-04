using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Pages;
namespace UnitTest
{
    [TestClass]
    public class TaskTest
    {
        [TestMethod]
        public void IndexModel_GetAllTests_ReturnsTotalTasks()
        {
            var indexModel = new IndexModel(null);

            // Act
            indexModel.OnGet();

            // Assert
            Assert.AreEqual(3, indexModel.Tasks.Count()); // Ensure all tasks are returned
        }
        [TestMethod]
        public void GetTaskById_ReturnsTaskById()
        {
            // Arrange
            var taskId = InMemoryTaskCollection.GetTasks().First().Id;

            // Act
            var task = InMemoryTaskCollection.GetTaskById(taskId);

            // Assert
            Assert.IsNotNull(task); // Ensure a task is returned
            Assert.AreEqual(taskId, task.Id); // Ensure the returned task has the correct ID
        }

        [TestMethod]
        public void UpdateTaskById_UpdatesTask()
        {
            // Arrange
            var taskId = InMemoryTaskCollection.GetTasks().First().Id;
            var updatedTask = new Tasks { Id = taskId, Name = "Task 10", Priority = 3, Status = "In Progress" };

            // Act
            var originalTaskStatus = InMemoryTaskCollection.GetTaskById(taskId).Status;
            InMemoryTaskCollection.GetTasks()[0] = updatedTask;
            var VerifyupdatedTaskStatus = InMemoryTaskCollection.GetTaskById(taskId);

            // Assert
            Assert.AreEqual("Task 10", VerifyupdatedTaskStatus.Name); // Ensure the task name was updated to the correct value
            Assert.AreEqual(3, VerifyupdatedTaskStatus.Priority); // Ensure the task priority was updated to the correct value
            Assert.AreEqual("In Progress", VerifyupdatedTaskStatus.Status); // Ensure the task status was updated to the correct value
        }

        [TestMethod]
        public void CreateTaskModel_OnPost_AddsNewTaskIfStatusNotCompleted()
        {
            // Arrange
            var createTaskModel = new CreateTaskModel();
            createTaskModel.Task = new Tasks
            {
                Name = "Task 4",
                Priority = 1,
                Status = "In Progress"
            };

            // Act
            var result = createTaskModel.OnPost();

            // Assert
            var tasks = InMemoryTaskCollection.GetTasks();
            var newTask = tasks.Last();

            Assert.AreEqual(4, tasks.Count);
            Assert.AreEqual(createTaskModel.Task.Name, newTask.Name);
            Assert.AreEqual(createTaskModel.Task.Priority, newTask.Priority);
            Assert.AreEqual(createTaskModel.Task.Status, newTask.Status);
        }

        [TestMethod]
        public void CreateTaskModel_OnPost_SameTaskName_DoesNotAddTasksAndReturnsSameNumberOfTaskItems()
        {
            // Arrange
            var createTaskModel = new CreateTaskModel();
            createTaskModel.Task = new Tasks
            {
                Name = "Task 1",
                Priority = 5,
                Status = "In Progress"
            };

            // Act
            var result = createTaskModel.OnPost();

            // Assert
            var tasks = InMemoryTaskCollection.GetTasks();
            Assert.AreEqual(3, tasks.Count);
        }

        [TestMethod]
        public void EditTaskModel_OnPost_RemovesTaskIfStatusCompleted()
        {
            var taskId = InMemoryTaskCollection.GetTasks().First().Id;
            var originalTask = InMemoryTaskCollection.GetTaskById(taskId);
            var updatedTask = new Tasks { Id = taskId, Name = originalTask.Name, Priority = originalTask.Priority, Status = "Completed" };
            var editTaskModel = new EditTaskModel()
            {
                Task = updatedTask
            };

            // Act
            var result = editTaskModel.OnPost();

            // Assert
            Assert.IsNull(InMemoryTaskCollection.GetTaskById(taskId));
            Assert.AreEqual(2, InMemoryTaskCollection.GetTasks().Count);
        }
    }
}