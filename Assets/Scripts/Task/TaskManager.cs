using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class TaskManager : MonoBehaviour
{
    [SerializeField] private List<TaskData> taskList = new();

    private readonly List<TaskState> taskStates = new();

    public event Action OnTaskChanged;

    private void Awake()
    {
        taskStates.Clear();

        foreach (TaskData task in taskList)
        {
            if (task != null)
                taskStates.Add(new TaskState(task));
        }
    }

    public void CompleteTask(TaskData data)
    {
        TaskState state = taskStates.Find(task => task.Data == data);
        CompleteTask(state);
    }

    public void CompleteTask(string taskName)
    {
        TaskState state = taskStates.Find(task => task.Data != null && task.Data.TaskName == taskName);
        CompleteTask(state);
    }

    public bool IsCompleted(TaskData data)
    {
        TaskState state = taskStates.Find(task => task.Data == data);
        return state != null && state.IsCompleted;
    }

    public bool IsCompleted(string taskName)
    {
        TaskState state = taskStates.Find(task => task.Data != null && task.Data.TaskName == taskName);
        return state != null && state.IsCompleted;
    }

    public IReadOnlyList<TaskState> GetAllTasks() => taskStates;

    private void CompleteTask(TaskState state)
    {
        if (state != null && state.Complete())
            OnTaskChanged?.Invoke();
    }
}
