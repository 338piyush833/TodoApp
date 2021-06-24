using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.IRepository;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly IDatabaseRepository _todoDb;
        private readonly IMapper _mapper;



        public TaskController(IDatabaseRepository todoDb,
            ILogger<TaskController> logger,
            IMapper mapper)
        {
            _todoDb = todoDb;
            _logger = logger;
            _mapper = mapper;
        }

        //get method responses all tasks from database table
        [HttpGet]
        public async Task<IActionResult> GetAllTask()
        {
            var tasks = await _todoDb.TodoTasks.GetAllItems();
            if (tasks == null)
            {
                return BadRequest("No task Available");
            }
            return Ok(tasks);
        }

        [HttpGet("{id:int}", Name = "GetTaskById")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _todoDb.TodoTasks.GetOneItem(item => item.Id == id);
            if (task == null)
            {
                return BadRequest("Id is Invalid");
            }
            return Ok(task);
        }

        [HttpPost]
        //[Route("single")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddOneNewTask([FromBody] CreateTodoTaskDTO taskDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var task = _mapper.Map<TodoTask>(taskDTO);
            await _todoDb.TodoTasks.InsertItem(task);
            await _todoDb.Save();

            return CreatedAtRoute("GetTaskById", new { id = task.Id }, task);
        }

        [HttpPost]
        [Route("multiple")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddMultipleNewTask([FromBody] List<CreateTodoTaskDTO> taskDTOs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<TodoTask> todoTasks = new List<TodoTask>();
            foreach (var taskDTO in taskDTOs)
            {
                var task = _mapper.Map<TodoTask>(taskDTO);
                todoTasks.Add(task);
            }
            await _todoDb.TodoTasks.InsertMultipleItems(todoTasks);
            await _todoDb.Save();
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] CreateTodoTaskDTO taskDTO)
        {
            try
            {
                if (!ModelState.IsValid || id < 1)
                {
                    return BadRequest(ModelState);
                }
                var task = await _todoDb.TodoTasks.GetOneItem(item => item.Id == id);
                if (task == null)
                {
                    return BadRequest("Submitted data is invalid");
                }

                _mapper.Map(taskDTO, task);
                _todoDb.TodoTasks.UpdateItem(task);
                await _todoDb.Save();

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _todoDb.TodoTasks.GetOneItem(item => item.Id == id);
            if (task == null)
            {
                return BadRequest("Nothing to Delete");
            }
            await _todoDb.TodoTasks.DeleteItem(id);
            await _todoDb.Save();

            return NoContent();
        }

        [HttpDelete]
        [Route("multiple")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMultipleTask([FromBody] List<int> ids)
        {
            List<TodoTask> todoTasks = new List<TodoTask>();
            foreach (var id in ids)
            {
                TodoTask task = await _todoDb.TodoTasks.GetOneItem(entity => entity.Id == id);
                todoTasks.Add(task);
            }
            _todoDb.TodoTasks.DeleteMultipleItems(todoTasks);
            await _todoDb.Save();

            return NoContent();
        }

        [HttpDelete]
        [Route("all")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAllTask()
        {
            var tasks = await _todoDb.TodoTasks.GetAllItems();
            if (tasks == null)
            {
                return BadRequest("Nothing to Delete");
            }
            _todoDb.TodoTasks.DeleteMultipleItems(tasks);
            await _todoDb.Save();

            return NoContent();
        }

    }
}
