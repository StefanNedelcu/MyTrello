using Microsoft.EntityFrameworkCore;
using MyTrello.Data.Contexts;
using MyTrello.Dto;
using MyTrello.Interfaces;
using Task = System.Threading.Tasks.Task;
using TaskEntity = MyTrello.Data.Entities.Task;

namespace MyTrello.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<TaskService> _logger;

    public TaskService(ApplicationDbContext context, ILogger<TaskService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task AddTask(Guid taskListId, TaskDto newTask)
    {
        try
        {
            var taskList = await _context.TaskLists.FirstOrDefaultAsync(tl => tl.TaskListId == taskListId)
                ?? throw new ArgumentException($"TaskList {taskListId} not found");
            await _context.Tasks.AddAsync(new TaskEntity
            {
                TaskList = taskList,
                Info = newTask.Info,
                Description = newTask.Description,
            });
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException) // ToDo: This probably catches more situations
        {
            throw new ArgumentException($"Task {newTask.Info} already exists");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Add task {taskName} failed", newTask.Info);
            throw;
        }
    }

    public async Task DeleteTask(Guid taskId)
    {
        try
        {
            var taskToDelete = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == taskId)
                ?? throw new ArgumentException($"No task with id {taskId} found");

            _context.Tasks.Remove(taskToDelete);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Delete board {boardId} failed", taskId);
            throw;
        }
    }

    public async Task EditTask(TaskDto editedTask)
    {
        try
        {
            var taskToEdit = await _context.Tasks.FirstOrDefaultAsync(t => t.TaskId == editedTask.TaskId)
                ?? throw new ArgumentException($"No task with id {editedTask.TaskId} found");

            taskToEdit.Info = editedTask.Info;
            taskToEdit.Description = editedTask.Description;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Edit board {taskId} failed", editedTask.TaskId);
            throw;
        }
    }

    public async Task<IEnumerable<TaskDto>> GetTasksFromList(Guid taskListId)
    {
        try
        {
            return await _context.Tasks
                .Include(t => t.TaskList)
                .Where(t => t.TaskList.TaskListId == taskListId)
                .Select(t => new TaskDto
                {
                    TaskId = t.TaskId,
                    Info = t.Info,
                    Description = t.Description,
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get all tasks failed");
            throw;
        }
    }
}
