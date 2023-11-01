using Microsoft.AspNetCore.Mvc;
using MyTrello.Dto;
using MyTrello.Interfaces;

namespace MyTrello.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskListsController : ControllerBase
    {
        private readonly ITaskListService _taskListService;

        public TaskListsController(ITaskListService taskListService)
        {
            _taskListService = taskListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskLists([FromQuery]string boardName)
        {
            try
            {
                var allTaskLists = await _taskListService.GetTaskListsForBoard(boardName);
                return Ok(allTaskLists);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTaskList([FromQuery]string boardName, [FromBody]TaskListDto newTaskList)
        {
            try
            {
                await _taskListService.AddTaskList(boardName, newTaskList);
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
                await _taskListService.EditTaskList(editedTaskList);
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
                await _taskListService.DeleteTaskList(taskListId);
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