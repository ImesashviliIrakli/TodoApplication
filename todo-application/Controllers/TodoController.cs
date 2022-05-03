using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using todo_application.Models;
using todo_application.Repository;
using todo_domain_entities;

namespace todo_application.Controllers
{
    public class TodoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly TodoListRepository _repository;
        private readonly IMapper _mapper;
        public TodoController(ApplicationDbContext context, TodoListRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<TodoList> model = _context.Todos.Where(x => x.Status != 2).ToList();
            return View(_mapper.Map<List<Todo>>(model));
        }

        public IActionResult AddPage()
        {
            return View();
        }

        [HttpGet("{id:int}")]
        public IActionResult UpdatePage(int id)
        {
            TodoList model = _context.Todos.Where(x => x.Id == id).FirstOrDefault();
            if(model == null)
            {
                return View("Error");
            }
            return View(_mapper.Map<Todo>(model));
        }

        [HttpGet("details/{id}")]
        public IActionResult DetailPage(int id)
        {
            TodoList details = _context.Todos.Where(x => x.Id == id).FirstOrDefault();

            if(details == null)
            {
                return View("Error");
            }

            return View(_mapper.Map<Todo>(details));
        }

        [HttpPost]
        public IActionResult AddTodo(Todo body)
        {
            TodoList todoEntity = _mapper.Map<TodoList>(body);

            if (!ModelState.IsValid)
            {
                return View("AddPage");
            }

            _repository.AddTodo(todoEntity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateTodo(Todo body)
        {
            TodoList todoEntity = _mapper.Map<TodoList>(body);

            if (!ModelState.IsValid)
            {
                return View("UpdatePage", body);
            }

             _repository.UpdateTodo(todoEntity);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MarkAsFinished(int id)
        {
            var finish = _repository.ChangeStatus(id, 2);

            if (!finish)
            {
                return NotFound("Error");
            }

            return Json(new { success = "Success" });
        }

        [HttpPost]
        public IActionResult ChangeTodoStatus(int id, int status)
        {
            var setDoing = _repository.ChangeStatus(id, status);

            if (!setDoing)
            {
                return NotFound("Error");
            }

            return Json(new { success = "Success" });
        }

        [HttpPost]
        public IActionResult DeleteTodo(int id)
        {
            var delete = _repository.DeleteTodo(id);

            if (!delete)
            {
                return NotFound("Error");
            }

            return Json(new { success = "Success" });
        }

        [HttpGet]
        public IActionResult FinishedTodos()
        {
            List<TodoList> model = _context.Todos.Where(x => x.Status == 2).ToList();

            return View(_mapper.Map<List<Todo>>(model));
        }

        [HttpPost]
        public IActionResult UnMarkFinished(int id)
        {
            var finish = _repository.ChangeStatus(id, 0);

            if (!finish)
            {
                return NotFound("Error");
            }

            return Json(new { success = "Success" });
        }
        
        [HttpGet("Error404")]
        public IActionResult Error()
        {
            return View();
        }
    
    }
}
