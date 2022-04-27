using System;

namespace todo_domain_entities
{
    public class TodoList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public int Status { get; set; } = 0;
    }
}
