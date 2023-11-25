using MyTrello.Dto;

namespace MyTrello.Interfaces;

public interface ITaskListsService
{
    Task<IEnumerable<TaskListDto>> GetTaskListsAsync(Guid? boardId);
    Task<TaskListDto> GetTaskListAsync(Guid taskListId);
    Task AddTaskListAsync(TaskListDto newTaskList);
    Task EditTaskListAsync(TaskListDto editedTaskList);
    Task DeleteTaskListAsync(Guid taskListId);
}
