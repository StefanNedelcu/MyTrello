using Microsoft.AspNetCore.Mvc;
using MyTrello.Dto;
using MyTrello.Interfaces;

namespace MyTrello.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardsService _boardsService;

        public BoardsController(IBoardsService boardsService)
        {
            _boardsService = boardsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBoards()
        {
            try
            {
                var allBoards = await _boardsService.GetAllBoardsAsync();
                return Ok(allBoards);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpGet("{boardId}")]
        public async Task<IActionResult> GetBoard([FromRoute] Guid boardId)
        {
            try
            {
                var board = await _boardsService.GetBoardAsync(boardId);
                return Ok(board);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBoard([FromBody] BoardDto newBoard)
        {
            try
            {
                await _boardsService.AddBoardAsync(newBoard);
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
        public async Task<IActionResult> EditBoard([FromBody] BoardDto editedBoard)
        {
            try
            {
                await _boardsService.EditBoardAsync(editedBoard);
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

        [HttpDelete("{boardId}")]
        public async Task<IActionResult> DeleteBoard([FromRoute] Guid boardId)
        {
            try
            {
                await _boardsService.DeleteBoardAsync(boardId);
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