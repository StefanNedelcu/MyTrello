using MyTrello.Dto;

namespace MyTrello.Interfaces;

public interface IBoardsService
{
    Task<BoardDto> GetBoardAsync(Guid boardId);
    Task<IEnumerable<BoardDto>> GetAllBoardsAsync();
    Task AddBoardAsync(BoardDto newBoard);
    Task EditBoardAsync(BoardDto editedBoard);
    Task DeleteBoardAsync(Guid boardId);
}
