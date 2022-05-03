using System;
using System.ComponentModel.DataAnnotations;

namespace todo_domain_entities
{
    public class TodoList
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(80, ErrorMessage = "Title must not exceed 80 characters")]
        public string Title { get; set; }
        [StringLength(320, ErrorMessage = "Description must not exceed 320 characters")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Duedate is required")]
        public DateTime DueDate { get; set; }
        public int Status { get; set; } = 0;
    }
}
