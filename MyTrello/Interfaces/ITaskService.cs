using MyTrello.Dto;

namespace MyTrello.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetTasksFromList(Guid taskListId);
    Task AddTask(Guid taskListId, TaskDto newTask);
    Task DeleteTask(Guid taskId);
    Task EditTask(TaskDto editedTask);
}
