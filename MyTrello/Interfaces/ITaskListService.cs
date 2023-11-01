using MyTrello.Dto;

namespace MyTrello.Interfaces;

public interface ITaskListService
{
    Task<IEnumerable<TaskListDto>> GetTaskListsForBoard(string boardName);
    Task AddTaskList(string boardName, TaskListDto newTaskList);
    Task DeleteTaskList(Guid taskListId);
    Task EditTaskList(TaskListDto editedTaskList);
}
