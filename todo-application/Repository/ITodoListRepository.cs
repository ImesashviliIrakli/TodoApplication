using todo_application.Models;

namespace todo_application.Repository
{
    public interface ITodoListReposority
    {
        public Todo GetTask(int id);
        public int GetByTitle(string title);
        public void AddTodo(Todo body);
        public void UpdateTodo(Todo body);
        public bool DeleteTodo(int id);
        public bool ChangeStatus(int id, int status);
    }
}
