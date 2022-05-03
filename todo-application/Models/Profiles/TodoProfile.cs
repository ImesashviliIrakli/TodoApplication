using AutoMapper;
using todo_domain_entities;

namespace todo_application.Models.Profiles
{
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoList, Todo>();
            CreateMap<Todo, TodoList>();
        }
    }
}
