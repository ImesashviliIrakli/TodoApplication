﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace todo_application.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(80, ErrorMessage = "Title must not exceed 80 characters")]
        public string Title { get; set; }
        [StringLength(320, ErrorMessage = "Description must not exceed 320 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Duedate is required")]
        public DateTime DueDate { get; set; }
        public int Status { get; set; } = 0;
    }
}
