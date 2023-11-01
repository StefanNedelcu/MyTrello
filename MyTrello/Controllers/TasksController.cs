using Microsoft.AspNetCore.Mvc;
using MyTrello.Dto;
using MyTrello.Interfaces;

namespace MyTrello.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] Guid taskListId)
        {
            try
            {
                var allTaskLists = await _taskService.GetTasksFromList(taskListId);
                return Ok(allTaskLists);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromQuery]Guid taskListId, [FromBody]TaskDto newTask)
        {
            try
            {
                await _taskService.AddTask(taskListId, newTask);
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
                await _taskService.EditTask(editedTask);
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
        public async Task<IActionResult> DeleteTask([FromRoute]Guid taskId)
        {
            try
            {
                await _taskService.DeleteTask(taskId);
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