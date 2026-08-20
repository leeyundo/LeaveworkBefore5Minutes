using UnityEngine;

[CreateAssetMenu(fileName = "Task", menuName = "LeaveWork/Task")]
public sealed class TaskData : ScriptableObject
{
    [SerializeField] private string taskName;
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private bool defaultCompleted;

    public string TaskName => taskName;
    public string Description => description;
    public bool DefaultCompleted => defaultCompleted;
}
