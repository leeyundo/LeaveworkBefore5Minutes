public sealed class TaskState
{
    public TaskData Data { get; }
    public bool IsCompleted { get; private set; }

    public TaskState(TaskData data)
    {
        Data = data;
        IsCompleted = data != null && data.DefaultCompleted;
    }

    public bool Complete()
    {
        if (IsCompleted)
            return false;

        IsCompleted = true;
        return true;
    }
}
