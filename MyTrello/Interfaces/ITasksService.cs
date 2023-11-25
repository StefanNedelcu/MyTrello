using MyTrello.Dto;

namespace MyTrello.Interfaces;

public interface ITasksService
{
    Task<IEnumerable<TaskDto>> GetTasksAsync(Guid? taskListId);
    Task<TaskDto> GetTaskAsync(Guid taskId);
    Task AddTaskAsync(TaskDto newTask);
    Task DeleteTaskAsync(Guid taskId);
    Task EditTaskAsync(TaskDto editedTask);
}
