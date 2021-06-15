using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.IRepository;
using TodoApp.Repository;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IDatabaseRepository _database;

       
        public TaskController(IDatabaseRepository database, ILogger<TaskController> logger)
        {
            _database = database;
            _logger = logger;
        }

        [HttpGet]
        //get method response all tasks from database table
        public async Task<IActionResult> GetAllTask()
        {
            var tasks = await _database.TodoTasks.GetAllItems();
            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var tasks = await _database.TodoTasks.GetOneItem(item => item.Id == id);
            return Ok(tasks);
        }
    }
}
