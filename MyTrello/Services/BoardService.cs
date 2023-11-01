using Microsoft.EntityFrameworkCore;
using MyTrello.Data.Contexts;
using MyTrello.Data.Entities;
using MyTrello.Dto;
using MyTrello.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace MyTrello.Services;

public class BoardService : IBoardService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<BoardService> _logger;

    public BoardService(ApplicationDbContext context, ILogger<BoardService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddBoard(BoardDto newBoard)
    {
        try
        {
            await _context.Boards.AddAsync(new Board
            {
                Name = newBoard.Name,
            });
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException) // ToDo: This probably catches more situations
        {         
            throw new ArgumentException($"Board {newBoard.Name} already exists");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add board {board} failed", newBoard.Name);
            throw;
        }
    }

    public async Task DeleteBoard(Guid boardId)
    {
        try
        {
            var boardToDelete = await _context.Boards.FirstOrDefaultAsync(b =>  b.BoardId == boardId) 
                ?? throw new ArgumentException($"No board with id {boardId} found");

            _context.Boards.Remove(boardToDelete);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete board {boardId} failed", boardId);
            throw;
        }
    }

    public async Task EditBoard(BoardDto editedBoard)
    {
        try
        {
            var boardToEdit = await _context.Boards.FirstOrDefaultAsync(b => b.BoardId == editedBoard.BoardId)
                ?? throw new ArgumentException($"No board with id {editedBoard.BoardId} found");

            boardToEdit.Name = editedBoard.Name;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Edit board {boardId} failed", editedBoard.BoardId);
            throw;
        }
    }

    public async Task<IEnumerable<BoardDto>> GetAllBoards()
    {
        try
        {
            return await _context.Boards
                .Select(b => new BoardDto
                {
                    BoardId = b.BoardId,
                    Name = b.Name,
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get all boards failed");
            throw;
        }
    }
}
