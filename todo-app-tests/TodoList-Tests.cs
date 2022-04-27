using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Serilog.Core;
using System;
using System.Threading.Tasks;
using todo_application.Models;
using todo_application.Repository;

namespace todo_app_tests
{
    public class Tests 
    {
        private TodoListRepository repository;
        public static DbContextOptions<ApplicationDbContext> dbContextOptions { get; set; }
        public static string connectionString = @"Server=(localdb)\mssqllocaldb;Database=todo-application;Trusted_Connection=True;MultipleActiveResultSets=true";

        // Add a new task
        // Should return true and add new task
        [TestCase("TestTItle1", "TestDescription1", ExpectedResult = true)]
        [TestCase("TestTItle2", "TestDescription2", ExpectedResult = true)]
        [TestCase("TestTItle3", "TestDescription3", ExpectedResult = true)]
        [Order(0)]

        public bool AddTestsSuccess(string title, string description)
        {
            ILogger<TodoListRepository> logger = Mock.Of<ILogger<TodoListRepository>>();

            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationDbContext(dbContextOptions);

            repository = new TodoListRepository(context, logger);

            Todo body = new Todo
            {
                Title = title,
                Description = description,
                DueDate = DateTime.Now.AddDays(7),
            };

            try
            {
                repository.AddTodo(body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Add a new task error check
        // Should return false and not update
        [TestCase(ExpectedResult = false)]
        public bool AddTestNoSuccess()
        {
            ILogger<TodoListRepository> logger = Mock.Of<ILogger<TodoListRepository>>();

            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationDbContext(dbContextOptions);

            repository = new TodoListRepository(context, logger);

            Todo body = null;

            try
            {
                repository.AddTodo(body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Update a task
        // Should return true and update
        [TestCase("TestTItle1", "TestTItle1updated", "TestDescription1updated", ExpectedResult = true)]
        [TestCase("TestTItle2", "TestTItle2updated", "TestDescription2updated", ExpectedResult = true)]
        [TestCase("TestTItle3", "TestTItle3updated", "TestDescription3updated", ExpectedResult = true)]
        [Order(1)]
        public bool UpdateTestsSuccess(string oldTitle, string newTitle, string newDescription)
        {
            ILogger<TodoListRepository> logger = Mock.Of<ILogger<TodoListRepository>>();

            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationDbContext(dbContextOptions);

            repository = new TodoListRepository(context, logger);

            Todo body = new Todo
            {
                Id = repository.GetByTitle(oldTitle),
                Title = newTitle,
                Description = newDescription,
                DueDate = DateTime.Now.AddDays(7),
            };

            try
            {
                repository.UpdateTodo(body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Update a task error check
        // Should return false and not update
        [TestCase(ExpectedResult = false)]
        public bool UpdateTestsNoSuccess()
        {
            ILogger<TodoListRepository> logger = Mock.Of<ILogger<TodoListRepository>>();

            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationDbContext(dbContextOptions);

            repository = new TodoListRepository(context, logger);

            Todo body = null;

            try
            {
                repository.UpdateTodo(body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Mark a task as finished
        // Should return true and mark a task as finished
        [TestCase("TestTItle1updated", ExpectedResult = true)]
        [TestCase("TestTItle2updated", ExpectedResult = true)]
        [TestCase("TestTItle3updated", ExpectedResult = true)]
        [Order(3)]
        public bool MarkAsFinishedTestsSuccess(string title)
        {
            ILogger<TodoListRepository> logger = Mock.Of<ILogger<TodoListRepository>>();

            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationDbContext(dbContextOptions);

            repository = new TodoListRepository(context, logger);

            int id = repository.GetByTitle(title);

            var res = repository.ChangeStatus(id, 2);

            return res;
        }

        // Mark a task as finished error check
        // Should return false and not mark as finished
        [TestCase("", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        [TestCase("NonExistingTitle", ExpectedResult = false)]
        public bool MarkAsFinishedTestsNoSuccess(string title)
        {
            ILogger<TodoListRepository> logger = Mock.Of<ILogger<TodoListRepository>>();

            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationDbContext(dbContextOptions);

            repository = new TodoListRepository(context, logger);

            var id = repository.GetByTitle(title);

            var res = repository.ChangeStatus(id, 2);

            return res;
        }

        // Remove a task
        // Should return true and remove task
        [TestCase("TestTItle1updated", ExpectedResult = true)]
        [TestCase("TestTItle2updated", ExpectedResult = true)]
        [TestCase("TestTItle3updated", ExpectedResult = true)]
        [Order(4)]
        public bool RemoveTestsSuccess(string title)
        {
            ILogger<TodoListRepository> logger = Mock.Of<ILogger<TodoListRepository>>();

            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationDbContext(dbContextOptions);

            repository = new TodoListRepository(context, logger);

            int id = repository.GetByTitle(title);

            var res = repository.DeleteTodo(id);

            return res;
        }

        [TestCase("", ExpectedResult = false)]
        [TestCase(null, ExpectedResult = false)]
        [TestCase("NONExistingTask", ExpectedResult = false)]
        public bool RemoveTestsNoSuccess(string title)
        {
            ILogger<TodoListRepository> logger = Mock.Of<ILogger<TodoListRepository>>();

            dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            var context = new ApplicationDbContext(dbContextOptions);

            repository = new TodoListRepository(context, logger);

            int id = repository.GetByTitle(title);

            var result = repository.DeleteTodo(id);

            return result;
        }
    }
}