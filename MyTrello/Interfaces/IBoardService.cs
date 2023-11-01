using MyTrello.Dto;

namespace MyTrello.Interfaces;

public interface IBoardService
{
    Task<IEnumerable<BoardDto>> GetAllBoards();
    Task AddBoard(BoardDto newBoard);
    Task DeleteBoard(Guid boardId);
    Task EditBoard(BoardDto editedBoard);
}
