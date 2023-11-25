using Microsoft.AspNetCore.Mvc;
using MyTrello.Dto;
using MyTrello.Interfaces;

namespace MyTrello.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskListsController : ControllerBase
    {
        private readonly ITaskListsService _taskListsService;

        public TaskListsController(ITaskListsService taskListsService)
        {
            _taskListsService = taskListsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskLists([FromQuery] Guid? boardId = null)
        {
            try
            {
                var taskLists = await _taskListsService.GetTaskListsAsync(boardId);
                return Ok(taskLists);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpGet("{taskListId}")]
        public async Task<IActionResult> GetTaskList([FromRoute] Guid taskListId)
        {
            try
            {
                var taskList = await _taskListsService.GetTaskListAsync(taskListId);
                return Ok(taskList);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTaskList([FromBody] TaskListDto newTaskList)
        {
            try
            {
                await _taskListsService.AddTaskListAsync(newTaskList);
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
        public async Task<IActionResult> EditTaskList([FromBody] TaskListDto editedTaskList)
        {
            try
            {
                await _taskListsService.EditTaskListAsync(editedTaskList);
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

        [HttpDelete("{taskListId}")]
        public async Task<IActionResult> DeleteTaskList([FromRoute]Guid taskListId)
        {
            try
            {
                await _taskListsService.DeleteTaskListAsync(taskListId);
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