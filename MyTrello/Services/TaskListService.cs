using Microsoft.EntityFrameworkCore;
using MyTrello.Data.Contexts;
using MyTrello.Data.Entities;
using MyTrello.Dto;
using MyTrello.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace MyTrello.Services;

public class TaskListService : ITaskListService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TaskListService> _logger;

    public TaskListService(ApplicationDbContext context, ILogger<TaskListService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddTaskList(string boardName, TaskListDto newTaskList)
    {
        try
        {
            var board = await _context.Boards.Where(b => b.Name == boardName).FirstOrDefaultAsync()
                ?? throw new ArgumentException($"Board {boardName} not found");
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

    public async Task DeleteTaskList(Guid taskListId)
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

    public async Task EditTaskList(TaskListDto editedTaskList)
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

    public async Task<IEnumerable<TaskListDto>> GetTaskListsForBoard(string boardName)
    {
        try
        {
            return await _context.TaskLists
                .Include(tl => tl.Tasks)
                .Include(tl => tl.Board)
                .Where(tl => tl.Board.Name == boardName)
                .Select(tl => new TaskListDto
                {
                    TaskListId = tl.TaskListId,
                    Name = tl.Name,
                    Tasks = tl.Tasks.Select(t => new TaskDto
                    {
                        TaskId = t.TaskId,
                        Info = t.Info,
                        Description = t.Description,
                    })
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get taskLists for board {boardName} failed", boardName);
            throw;
        }
    }
}
