using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Models;
using TaskManagementApi.Repository;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;
        private string _badRequestMsg = "";
        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                var tasks = await _taskRepository.GetAllTasksAsync();

                return Ok(tasks);
            }
            catch (Exception ex) {
                _badRequestMsg= ex.Message;
            }

            return BadRequest(_badRequestMsg);
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            try
            {
                var task = await _taskRepository.GetTaskByIdAsync(id);

                if (task == null)
                {
                    return NotFound("Task not Found.");
                }

                return Ok(task);
            }
            catch (Exception ex)
            {
                _badRequestMsg= ex.Message;
            }
            return BadRequest(_badRequestMsg);
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<IActionResult> PostTask(TaskItem task)
        {
            try
            {
               var newtask= await _taskRepository.AddTaskAsync(task);
               return Ok(newtask);
            }
            catch (Exception ex)
            {
                _badRequestMsg = ex.Message;
            }
            return BadRequest(_badRequestMsg);
        }
        [HttpPut("{id}/toggle")]
        public async Task<IActionResult> ToggleTask(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound("Task not found");
            }
            task.IsCompleted = !task.IsCompleted;
            try
            {
                var updatedTask=await _taskRepository.UpdateTaskAsync(task);
                return Ok(updatedTask);
            }
            catch(Exception ex) {
            
              _badRequestMsg = ex.Message;
            }

            return BadRequest(_badRequestMsg);
        }
        // PUT: api/Tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTask(int id, TaskItem task)
        {
            if (id != task.Id)
            {
                return BadRequest("Invalid Task reference.");
            }

            try
            {
                if (await _taskRepository.GetTaskByIdAsync(id) == null)
                {
                    return NotFound("Task not found.");
                }
                var updatedTask=await _taskRepository.UpdateTaskAsync(task);
                return Ok(updatedTask);
            }
            catch( Exception ex) {
             _badRequestMsg=ex.Message;
            }

            return BadRequest(_badRequestMsg);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
               var isDeleted= await _taskRepository.DeleteTaskAsync(id);
                if(isDeleted == false)
                {
                    return NotFound("Task not found for deletion.");
                }
                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                _badRequestMsg = ex.Message;
            }
            return BadRequest(_badRequestMsg);
        }
    }
}