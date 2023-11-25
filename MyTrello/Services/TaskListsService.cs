using Microsoft.EntityFrameworkCore;
using MyTrello.Data.Contexts;
using MyTrello.Data.Entities;
using MyTrello.Dto;
using MyTrello.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace MyTrello.Services;

public class TaskListsService : ITaskListsService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TaskListsService> _logger;

    public TaskListsService(ApplicationDbContext context, ILogger<TaskListsService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddTaskListAsync(TaskListDto newTaskList)
    {
        try
        {
            var board = await _context.Boards.Where(b => b.BoardId == newTaskList.BoardId).FirstOrDefaultAsync()
                ?? throw new ArgumentException($"Board {newTaskList.BoardId} not found");
            await _context.TaskLists.AddAsync(new TaskList
            {
                Board = board,
                Name = newTaskList.Name,
            });
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException) // ToDo: This probably catches more situations
        {
            throw new ArgumentException($"TaskList {newTaskList.Name} already exists");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add TaskList {taskList} failed", newTaskList.Name);
            throw;
        }
    }

    public async Task DeleteTaskListAsync(Guid taskListId)
    {
        try
        {
            var taskListToDelete = await _context.TaskLists.FirstOrDefaultAsync(t => t.TaskListId == taskListId)
                ?? throw new ArgumentException($"No TaskList with id {taskListId} found");

            _context.TaskLists.Remove(taskListToDelete);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete TaskList {taskListId} failed", taskListId);
            throw;
        }
    }

    public async Task EditTaskListAsync(TaskListDto editedTaskList)
    {
        try
        {
            var taskListToEdit = await _context.TaskLists.FirstOrDefaultAsync(t => t.TaskListId == editedTaskList.TaskListId)
                ?? throw new ArgumentException($"No taskList with id {editedTaskList.TaskListId} found");

            taskListToEdit.Name = editedTaskList.Name;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Edit taskList {taskListId} failed", editedTaskList.TaskListId);
            throw;
        }
    }

    public async Task<TaskListDto> GetTaskListAsync(Guid taskListId)
    {
        try
        {
            return await _context.TaskLists
                .Where(tl => tl.TaskListId == taskListId)
                .Select(tl => new TaskListDto
                {
                    TaskListId = tl.TaskListId,
                    BoardId = tl.BoardId,
                    Name = tl.Name,
                })
                .FirstAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get taskList {taskListId} failed", taskListId);
            throw;
        }
    }

    public async Task<IEnumerable<TaskListDto>> GetTaskListsAsync(Guid? boardId)
    {
        try
        {
            return await _context.TaskLists
                .Where(tl => boardId == null || tl.BoardId == boardId)
                .Select(tl => new TaskListDto
                {
                    TaskListId = tl.TaskListId,
                    BoardId = tl.BoardId,
                    Name = tl.Name,
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get taskLists for board {boardId} failed", boardId);
            throw;
        }
    }
}
