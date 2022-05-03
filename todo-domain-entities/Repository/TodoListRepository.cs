using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using todo_application.Models;
using todo_domain_entities;

namespace todo_application.Repository
{
    public class TodoListRepository : ITodoListReposority
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TodoListRepository> _logger;
        public TodoListRepository(ApplicationDbContext context, ILogger<TodoListRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int GetByTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return 0;
            }

            var todo = _context.Todos.Where(x => x.Title == title).FirstOrDefault();

            if (todo == null)
            {
                return 0;
            }

            return todo.Id;
        }

        public void AddTodo(TodoList body)
        { 
            string methodName = nameof(AddTodo);

            if(body == null)
            { 
                throw new ArgumentNullException(nameof(body));
            }

            try
            {
                _context.Todos.Add(body);

                _context.SaveChanges();

                _logger.LogInformation($"{methodName} => Success");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{methodName} => Exception: {ex.Message}");
            }
        }

        public bool ChangeStatus(int id, int status)
        {
            if (id == 0)
            {
                return false;
            }

            try
            {
                TodoList todo = _context.Todos.Where(x => x.Id == id).FirstOrDefault();

                if (todo != null)
                {
                    todo.Status = status;

                    _context.Todos.Update(todo);

                    _context.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteTodo(int id)
        {
            if (id == 0)
            {
                return false;
            }

            try
            {
                TodoList todo = _context.Todos.Where(x => x.Id == id).FirstOrDefault();

                if (todo != null)
                {
                    _context.Todos.Remove(todo);

                    _context.SaveChanges();

                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TodoList GetTask(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("id must not be 0");
            }

            try
            {
                TodoList todo = _context.Todos.Where(x => x.Id == id).FirstOrDefault();
                return todo;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void UpdateTodo(TodoList body)
        {
            string methodName = nameof(UpdateTodo);

            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            try
            {
                TodoList todo = _context.Todos.Where(x => x.Id == body.Id).FirstOrDefault();
                
                if (todo != null)
                {
                    todo.Title = body.Title;
                    todo.Description = body.Description;
                    todo.DueDate = body.DueDate;

                    _context.Todos.Update(todo);

                    _context.SaveChanges();

                    _logger.LogInformation($"{methodName} => Success");
                }

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{methodName} => Exception: {ex.Message}");

            }
        }
    }
}
