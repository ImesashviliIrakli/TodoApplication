using todo_application.Models;
using todo_domain_entities;

namespace todo_application.Repository
{
    public interface ITodoListReposority
    {
        public TodoList GetTask(int id);
        public int GetByTitle(string title);
        public void AddTodo(TodoList body);
        public void UpdateTodo(TodoList body);
        public bool DeleteTodo(int id);
        public bool ChangeStatus(int id, int status);
    }
}
