using Microsoft.AspNetCore.Mvc;
using MyTrello.Dto;
using MyTrello.Interfaces;

namespace MyTrello.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardsController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBoards()
        {
            try
            {
                var allBoards = await _boardService.GetAllBoards();
                return Ok(allBoards);
            }
            catch
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBoard([FromBody]BoardDto newBoard)
        {
            try
            {
                await _boardService.AddBoard(newBoard);
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
        public async Task<IActionResult> EditBoard([FromBody]BoardDto editedBoard)
        {
            try
            {
                await _boardService.EditBoard(editedBoard);
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
        public async Task<IActionResult> DeleteBoard([FromRoute]Guid boardId)
        {
            try
            {
                await _boardService.DeleteBoard(boardId);
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