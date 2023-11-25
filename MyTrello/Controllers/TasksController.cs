using Microsoft.AspNetCore.Mvc;
using MyTrello.Dto;
using MyTrello.Interfaces;

namespace MyTrello.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] Guid? taskListId)
        {
            try
            {
                var tasks = await _tasksService.GetTasksAsync(taskListId);
                return Ok(tasks);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTask([FromRoute] Guid taskId)
        {
            try
            {
                var tasks = await _tasksService.GetTaskAsync(taskId);
                return Ok(tasks);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskDto newTask)
        {
            try
            {
                await _tasksService.AddTaskAsync(newTask);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditBoard([FromBody] TaskDto editedTask)
        {
            try
            {
                await _tasksService.EditTaskAsync(editedTask);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask([FromRoute] Guid taskId)
        {
            try
            {
                await _tasksService.DeleteTaskAsync(taskId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }
    }
}